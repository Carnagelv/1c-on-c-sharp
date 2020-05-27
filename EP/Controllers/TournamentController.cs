using EP.BusinessLogic.Managers;
using System.Web.Mvc;

namespace EP.Controllers
{
    public class TournamentController : EPController
    {
        private readonly ITournamentManager _tournamentManager;

        public TournamentController(ITournamentManager tournamentManager)
        {
            _tournamentManager = tournamentManager;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetTournaments()
        {
            return Json(new { tournaments = _tournamentManager.GetTournaments() }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetTournament(int Id)
        {
            return Json(new { tournament = _tournamentManager.GetTournamentById(Id, GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult TakePartIn(int Id)
        {
            return Json(new { result = _tournamentManager.TakePartIn(GetCurrentUserId(), Id) }, JsonRequestBehavior.AllowGet);
        }
    }
}