using AutoMapper;
using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace EP.BusinessLogic.Managers
{  
    public class UserProfileManager : IUserProfileManager
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IFriendService _friendService;
        private readonly IRequestToFriendService _requestToFriendService;
        private readonly IMessageService _messageService;

        public UserProfileManager
        (
            IUserProfileService userProfileService, 
            IFriendService friendService, 
            IRequestToFriendService requestToFriendService,
            IMessageService messageService
        )
        {
            _userProfileService = userProfileService;
            _friendService = friendService;
            _requestToFriendService = requestToFriendService;
            _messageService = messageService;
        }

        private string GeneratePassword(int length)
        {

            const string str = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIPASDFGHJKLZXCVBNM";
            Random rand = new Random();
            string result = string.Empty;

            for (int i = 0; i < length; i++)
            {
                int index = rand.Next(0, str.Length);
                result += str[index];
            }

            return result;
        }

        public void AddMainRole(string userName, string roleName)
        {
            var roles = (SimpleRoleProvider)Roles.Provider;

            roles.AddUsersToRoles(new[] { userName }, new[] { roleName });
        }

        public void UpdateLastActivtyDate(string userName)
        {
            var user = _userProfileService.Get(g => g.UserName == userName);

            if (user != null)
            {
                user.LastActivityDate = DateTime.Now;
                _userProfileService.Update(user);
            }
        }

        public void Update(UserViewModel model, int userId)
        {
            var user = _userProfileService.Get(g => g.UserId == userId);

            user.FirstName = TextHelper.ToUpperCaseFirstLetter(model.FirstName);
            user.LastName = TextHelper.ToUpperCaseFirstLetter(model.LastName);
            user.LastActivityDate = DateTime.Now;
            user.Position = model.Position;
            user.Discipline = model.Discipline;          
            user.DateOfBirth = model.DateOfBirth;

            _userProfileService.Update(user);
        }

        public UserViewModel GetUserProfile(int Id, IMapper mapper, int userId)
        {
            var userEntity = _userProfileService.Get(g => g.UserId == Id);
            var user = new UserViewModel();

            if (userEntity != null)
            {
                user = mapper.Map<UserProfile, UserViewModel>(userEntity);
                user.IsFriend = userEntity.Friends.Any(a => a.WithID == userId);
                user.IsRequested = _requestToFriendService.IsRequested(userId, Id);
                user.IsAsked = userEntity.RequestToFriends.Any(a => a.WithID == userId);
            }

            return user;
        }

        public List<UserProfile> GetOtherUserForFriendShip(int Id, List<FriendsModel> friends, List<FriendsModel> requestsFrom, List<FriendsModel> requestsTo)
        {
            var Ids = new List<int> { Id };

            Ids.AddRange(friends.Select(s => s.UserId).ToList());
            Ids.AddRange(requestsFrom.Select(s => s.UserId).ToList());
            Ids.AddRange(requestsTo.Select(s => s.UserId).ToList());

            return _userProfileService.GetOtherUserForFriendShip(Ids);
        }

        public InformerData GetInformerData(int userId)
        {
            return _userProfileService.GetInformerData(userId);
        }

        public bool CheckUserModel(UserViewModel user)
        {
            return !string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName) && user.DateOfBirth.HasValue;
        }

        public bool CheckName(string name)
        {
            Regex regex = new Regex(@"\p{L}");

            return regex.Matches(name).Count == name.Length && name.Length <= Constants.DEFAULT_NAME_LENGHT;
        }

        public bool CheckEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsActived(int userId)
        {
            return _userProfileService.Get(g => g.UserId == userId)?.IsActive ?? false;
        }

        public bool IsActived(string userName)
        {
            return _userProfileService.Get(g => g.UserName == userName)?.IsActive ?? false;
        }

        public bool SendActivationEmail(int userId)
        {
            var code = GeneratePassword(Constants.ACTIVATION_CODE);
            var userAdress = _userProfileService.GetById(userId)?.UserName ?? string.Empty;

            if (!string.IsNullOrEmpty(userAdress) && !_userProfileService.CodeWasSent(userId))
            {
                try
                {
                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("esipirmais.activation@gmail.com", "mama17121966"),
                        EnableSsl = true
                    };

                    client.Send("esipirmais.activation@gmail.com", userAdress, "Activation Code", $"Jūsu kods: {code}");
                    _userProfileService.AddCode(userId, code);

                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;
        }

        public bool ActivateUser(int userId, string code)
        {
            if (code.Length == Constants.ACTIVATION_CODE)
            {
                return _userProfileService.ActivateUser(userId, code);
            }

            return false;
        }

        public bool CheckEmailExisting(string email)
        {
            return WebSecurity.UserExists(email);
        }

        public bool ChangeEmail(string email, string password, int userId)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var user =_userProfileService.Get(g => g.UserId == userId);

                if (WebSecurity.Login(user.UserName, password))
                {
                    user.UserName = email;
                    _userProfileService.Update(user);

                    WebSecurity.Logout();
                    WebSecurity.Login(email, password);

                    return true;
                }
            }

            return false;
        }

        public bool ChangePassword(string password, string oldPassword, int userId)
        {
            if (!string.IsNullOrEmpty(password))
            {
                var user = _userProfileService.Get(g => g.UserId == userId);

                if (WebSecurity.ChangePassword(user.UserName, oldPassword, password))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ResetPassword(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (WebSecurity.UserExists(email))
                {
                    var token = WebSecurity.GeneratePasswordResetToken(email);

                    try
                    {
                        var client = new SmtpClient("smtp.gmail.com", 587)
                        {
                            Credentials = new NetworkCredential("esipirmais.activation@gmail.com", "mama17121966"),
                            EnableSsl = true
                        };

                        client.Send("esipirmais.activation@gmail.com", email, "Pieejas atgūšana", "Lai atgūt pieeju, spiediet uz norādi: https://esipirmais.com/Home/ResetPassword?token=" + token);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool ResetPasswordFinal(string token, string password)
        {
            var userId = WebSecurity.GetUserIdFromPasswordResetToken(token);

            if (WebSecurity.ResetPassword(token, password))
            {
                var email = _userProfileService.Get(g => g.UserId == userId)?.UserName ?? string.Empty;

                if (WebSecurity.Login(email, password))
                {
                    return true;
                }
            }

            return false;
        }

        public OtherUserViewModel GetOtherUserProfileData(int id, int userId)
        {
            var result = new OtherUserViewModel();
            var currentUser = _userProfileService.Get(g => g.UserId == id);

            if (currentUser != null)
            {
                if (id != userId)
                {
                    result.LastLogIn = $"{currentUser.LastActivityDate.ToShortDateString()} {currentUser.LastActivityDate.ToShortTimeString()}";
                    result.UserDialogId = _messageService.Get(g => g.AuthorId == id && g.RecipientId == userId || g.AuthorId == userId && g.RecipientId == id)?.Id ?? 0;
                }

                var mapper = Mappings.GetMapper();

                result.UserFriends = mapper.Map<List<UserRandomFriend>>(currentUser.Friends.Take(6).ToList());
                result.FriendsCount = currentUser.Friends.Count;

            }

            return result;
        }

        public bool UploadPhoto(HttpPostedFileBase photo, int userId)
        {
            if (photo.ContentLength > 0 && (photo.ContentType.Contains("image")))
            {
               if (photo.ContentLength < 1100000)
               {
                    var fileName = $"{userId}{Path.GetExtension(photo.FileName)}";
                    var path = AppDomain.CurrentDomain.BaseDirectory + "Uploaded\\Users";

                    photo.SaveAs(Path.Combine(path, fileName));

                    var user = _userProfileService.Get(g => g.UserId == userId);
                    user.Photo = "/Uploaded/Users/" + fileName;
                    _userProfileService.Update(user);

                    return true;
               }
            }

            return false;
        }
    }

    public interface IUserProfileManager
    {
        void AddMainRole(string userName, string roleName);
        void UpdateLastActivtyDate(string userName);
        void Update(UserViewModel user, int userId);
        UserViewModel GetUserProfile(int Id, IMapper mapper, int userId);
        List<UserProfile> GetOtherUserForFriendShip(int Id, List<FriendsModel> friends, List<FriendsModel> requestsFrom, List<FriendsModel> requestsTo);
        InformerData GetInformerData(int userId);
        bool CheckUserModel(UserViewModel user);
        bool CheckName(string name);
        bool CheckEmail(string email);
        bool IsActived(int userId);
        bool IsActived(string userName);
        bool SendActivationEmail(int userId);
        bool ActivateUser(int userId, string code);
        bool CheckEmailExisting(string email);
        bool ChangeEmail(string email, string password, int userId);
        bool ChangePassword(string password, string oldPassword, int userId);
        bool ResetPassword(string email);
        bool ResetPasswordFinal(string token, string password);
        OtherUserViewModel GetOtherUserProfileData(int id, int userId);
        bool UploadPhoto(HttpPostedFileBase photo, int userId);
    }
}
