using EP.BusinessLogic.Managers;
using EP.BusinessLogic.Models;
using System.Web.Mvc;

namespace EP.Controllers
{
    public class MatchController : EPController
    {
        private readonly IMatchManager _matchManager;

        public MatchController(IMatchManager matchManager)
        {
            _matchManager = matchManager;
        }

        [Authorize]
        public ActionResult Index()
        {          
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult Get(int id)
        {
            return Json(new { match = _matchManager.GetMatchById(id) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetList(MatchFilters filters)
        {
            return Json(new { result = _matchManager.GetList(filters) }, JsonRequestBehavior.AllowGet);
        }
    }
}