using EP.BusinessLogic.Managers;
using EP.BusinessLogic.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EP.Controllers
{
    public class UserController : EPController
    {
        private readonly IUserProfileManager _userProfileManager;

        public UserController(IUserProfileManager userProfileManager)
        {
            _userProfileManager = userProfileManager;
        }

        [Authorize]
        public ActionResult Index(int? Id)
        {
            if (Id == null)
            {                
                return Redirect($"User?Id={ WebSecurity.GetUserId(User.Identity.Name) }");
            }

            return View(new PageModel { ID = Id.Value, IsOwner = Id == GetCurrentUserId(), IsActive = _userProfileManager.IsActived(GetCurrentUserId()) });
        }

        [Authorize]
        public ActionResult Activation()
        {
            if (!_userProfileManager.IsActived(GetCurrentUserId()))
            {
                _userProfileManager.SendActivationEmail(GetCurrentUserId());
            }
            else
                ChangeCookieState();

            return View(new Result { IsSuccess = _userProfileManager.IsActived(GetCurrentUserId()) });
        }

        [Authorize]
        [HttpPost]
        public JsonResult Activate(string code)
        {
            var success = _userProfileManager.ActivateUser(GetCurrentUserId(), code);

            if (success)
                ChangeCookieState();

            return Json(new { success });
        }       

        [Authorize]
        [HttpGet]
        public JsonResult Get(int Id)
        {           
            return Json(new
            {
                user = _userProfileManager.GetUserProfile(Id, mapper, GetCurrentUserId()), 
                other = _userProfileManager.GetOtherUserProfileData(Id, GetCurrentUserId())
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult Save(UserViewModel user)
        {
            if (!_userProfileManager.CheckName(user.FirstName) || !_userProfileManager.CheckName(user.LastName))
                return Json(new { success = false, errorMessage = "Datiem ir nepareiz formāts!" });

            _userProfileManager.Update(user, GetCurrentUserId());

            return Json(new { success = true });
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetInformers()
        {
            var informerData = _userProfileManager.GetInformerData(GetCurrentUserId());

            return Json(new
            {
                lastTeams = informerData.LastTeams,
                requestsToFriend = informerData.Requests,
                tournament = informerData.Tournament
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult CheckNewEmail(string email)
        {
            return Json(new { success = _userProfileManager.CheckEmail(email) && !_userProfileManager.CheckEmailExisting(email) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult ChangeEmail(string email, string password)
        {
            return Json(new { success = _userProfileManager.ChangeEmail(email, password, GetCurrentUserId()) });
        }

        [Authorize]
        [HttpPost]
        public JsonResult ChangePassword(string password, string oldPassword)
        {
            return Json(new { success = _userProfileManager.ChangePassword(password, oldPassword, GetCurrentUserId()) });
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult ResetPassword(string email)
        {
            return Json(new { success = _userProfileManager.ResetPassword(email) });
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult UploadPhoto(HttpPostedFileBase photo)
        {
            return Json(new
            {
                success = _userProfileManager.UploadPhoto(photo, GetCurrentUserId()),
                photo = $"/Uploaded/Users/{GetCurrentUserId()}{Path.GetExtension(photo.FileName)}"
            });
        }

        private void ChangeCookieState()
        {
            var oldCookie = HttpContext.Request.Cookies.Get("inActive");

            if (oldCookie != null)
            {
                oldCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(oldCookie);
            }

            var cookie = new HttpCookie("isActive");
            cookie.Expires.AddDays(30);
            HttpContext.Response.SetCookie(cookie);
        }
    }
}