using EP.BusinessLogic.Managers;
using EP.BusinessLogic.Models;
using EP.EntityData.Helpers;
using System.Web.Mvc;

namespace EP.Controllers
{
    public class NewsController : EPController
    {
        private readonly INewsManager _newsManager;

        public NewsController(INewsManager newsManager)
        {
            _newsManager = newsManager;
        }

        [Authorize]
        public ActionResult Index(int? Id = null)
        {           
            return View(new PageModel { ID = GetCurrentUserId() });
        }

        public JsonResult GetNews(NewsDiscipline discipline = NewsDiscipline.All)
        {
            return Json(new { news = _newsManager.GetNewsByDiscipline(discipline, GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNewsById(int Id)
        {
            return Json(new { selectedNews = _newsManager.GetNewsById(Id, GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ToggleLike(int Id)
        {
            _newsManager.ToogleLikeNews(Id, GetCurrentUserId());

            return Json(new { success = true });
        }

        public JsonResult AddComment(string text, int Id)
        {
            var cookie = HttpContext.Request.Cookies.Get("isActive");

            if (text.Trim().Length == 0 || cookie == null)
                return Json(new { success = false });

            var comment = _newsManager.AddComment(text, Id, GetCurrentUserId());

            return Json(new { success = comment.Id != 0, comment });
        }

        [HttpPost]
        public JsonResult DeleteComment(int Id)
        {
            _newsManager.DeleteComment(Id, GetCurrentUserId());

            return Json(new { success = true });
        }
    }
}