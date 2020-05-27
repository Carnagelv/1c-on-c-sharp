using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using EP.EntityData.Context;
using System.Collections.Generic;

namespace EP.BusinessLogic.Managers
{
    public interface IFriendManager
    {
        Result RequestToFriend(int userId, int friendId);
        Result AbortRequest(int userId, int friendId);
        Result AcceptRequest(int userId, int friendId);
        Result DeclineRequest(int userId, int friendId);
        Result DeleteFromFriend(int userId, int friendId);
        List<UserProfile> GetFriends(int id);       
    }

    public class FriendManager : IFriendManager
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IFriendService _friendService;
        private readonly IRequestToFriendService _requestToFriendService;

        public FriendManager(IUserProfileService userProfileService, IFriendService friendService, IRequestToFriendService requestToFriendService)
        {
            _userProfileService = userProfileService;
            _friendService = friendService;
            _requestToFriendService = requestToFriendService;
        }

        public Result RequestToFriend(int userId, int friendId)
        {
            if (_userProfileService.CheckUsersEntity(new int[] { userId, friendId }))
            {
                if (!_friendService.IsAlreadyFriend(userId, friendId))
                {
                    if (_requestToFriendService.RequestToFriend(userId, friendId))
                    { 
                        return new Result { IsSuccess = true };
                    }

                    return new Result { IsSuccess = false, ErrorMessage = "Pieteikums jau tiek nosūtīts" };
                }

                return new Result { IsSuccess = false, ErrorMessage = "Lietotājs jau ir jūsu draugs" };
            }

            return new Result { IsSuccess = false, ErrorMessage = "Lietotājs neeksiste" };
        }

        public Result AbortRequest(int userId, int friendId)
        {           
            if (_userProfileService.CheckUsersEntity(new int[] { userId, friendId }))
            {
                _requestToFriendService.AbortRequest(userId, friendId);

                return new Result { IsSuccess = true };               
            }

            return new Result { IsSuccess = false, ErrorMessage = "Lietotājs neeksiste" };
        }

        public Result AcceptRequest(int userId, int friendId)
        {
            if (_userProfileService.CheckUsersEntity(new int[] { userId, friendId }))
            {
                _requestToFriendService.CleanRequest(userId, friendId);
                _friendService.AcceptFriendShip(userId, friendId);

                return new Result { IsSuccess = true };
            }

            return new Result { IsSuccess = false, ErrorMessage = "Lietotājs neeksiste" };
        }

        public Result DeclineRequest(int userId, int friendId)
        {
            if (_userProfileService.CheckUsersEntity(new int[] { userId, friendId }))
            {
                _requestToFriendService.CleanRequest(userId, friendId);

                return new Result { IsSuccess = true };
            }

            return new Result { IsSuccess = false, ErrorMessage = "Lietotājs neeksiste" };
        }

        public Result DeleteFromFriend(int userId, int friendId)
        {
            if (_userProfileService.CheckUsersEntity(new int[] { userId, friendId }))
            {
                _friendService.RemoveFriendShip(userId, friendId);

                return new Result { IsSuccess = true };
            }

            return new Result { IsSuccess = false, ErrorMessage = "Lietotājs neeksiste" };
        }

        public List<UserProfile> GetFriends(int Id)
        {
            return _friendService.GetFriends(Id);
        }        
    }
}
