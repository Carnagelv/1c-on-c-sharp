using EP.BusinessLogic.Managers;
using System.Web;
using System.Web.Mvc;

namespace EP.Controllers
{
    public class FeedController : EPController
    {
        private readonly IFeedManager _feedManager;
        private readonly IUserProfileManager _userProfileManager;

        public FeedController(IFeedManager feedManager, IUserProfileManager userProfileManager)
        {
            _feedManager = feedManager;
            _userProfileManager = userProfileManager;
        }

        [Authorize]
        public ActionResult Index()
        {
            var activeCookie = HttpContext.Request.Cookies.Get("isActive");

            if (activeCookie == null)
            {
                var inActiveCookie = HttpContext.Request.Cookies.Get("inActive");

                if (inActiveCookie == null)
                {
                    HttpCookie cookie = _userProfileManager.IsActived(User.Identity.Name)
                        ? new HttpCookie("isActive")
                        : new HttpCookie("inActive");

                    cookie.Expires.AddDays(30);
                    HttpContext.Response.SetCookie(cookie);
                }
            }

            _userProfileManager.UpdateLastActivtyDate(GetCurrentUserName());

            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new { feed = _feedManager.GetFeeds(GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }
    }
}