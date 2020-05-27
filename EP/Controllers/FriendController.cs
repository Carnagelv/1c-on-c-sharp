using EP.BusinessLogic.Managers;
using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EP.Controllers
{
    public class FriendController : EPController
    {
        private readonly IFriendManager _friendManager;
        private readonly IRequestManager _requestManager;
        private readonly IUserProfileManager _userProfileManager;
        
        public FriendController(IFriendManager friendManager, IRequestManager requestManager, IUserProfileManager userProfileManager)
        {
            _friendManager = friendManager;
            _requestManager = requestManager;
            _userProfileManager = userProfileManager;
        }

        [Authorize]
        public ActionResult Index(int? Id)
        {
            if (!Id.HasValue)
            {
                Id = GetCurrentUserId();
            }

            return View(new PageModel { ID = Id.Value, IsOwner = Id == GetCurrentUserId() });
        }

        public JsonResult Get(int Id)
        {
            var friends = mapper.Map<List<UserProfile>, List<FriendsModel>>(_friendManager.GetFriends(Id));
            var requestsFrom = mapper.Map<List<UserProfile>, List<FriendsModel>>(_requestManager.GetRequestFromUser(Id));
            var requestsTo = mapper.Map<List<UserProfile>, List<FriendsModel>>(_requestManager.GetRequestForUser(Id));
            var otherUsers = mapper.Map<List<UserProfile>, List<FriendsModel>>(_userProfileManager.GetOtherUserForFriendShip(GetCurrentUserId(), friends, requestsFrom, requestsTo));

            return Json(new { friends, requestsFrom, requestsTo, otherUsers }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RequestToFriend(int Id)
        {
            var result = _friendManager.RequestToFriend(GetCurrentUserId(), Id);

            return Json(new { success = result.IsSuccess, errorMessage = result.ErrorMessage });
        }

        [HttpPost]
        public JsonResult AbortRequest(int Id)
        {
            var result = _friendManager.AbortRequest(GetCurrentUserId(), Id);

            return Json(new { success = result.IsSuccess, errorMessage = result.ErrorMessage });
        }

        [HttpPost]
        public JsonResult AcceptRequest(int Id)
        {
            var result = _friendManager.AcceptRequest(GetCurrentUserId(), Id);

            return Json(new { success = result.IsSuccess, errorMessage = result.ErrorMessage });
        }

        [HttpPost]
        public JsonResult DeclineRequest(int Id)
        {
            var result = _friendManager.DeclineRequest(GetCurrentUserId(), Id);

            return Json(new { success = result.IsSuccess, errorMessage = result.ErrorMessage });
        }

        [HttpPost]
        public JsonResult DeleteFromFriend(int Id)
        {
            var result = _friendManager.DeleteFromFriend(GetCurrentUserId(), Id);

            return Json(new { success = result.IsSuccess, errorMessage = result.ErrorMessage });
        }        
    }
}