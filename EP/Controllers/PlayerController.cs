using EP.BusinessLogic.Managers;
using System.Web.Mvc;

namespace EP.Controllers
{
    public class PlayerController : EPController
    {
        private readonly IPlayerManager _playerManager;

        public PlayerController(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
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
            return Json(new { player = _playerManager.GetPlayer(id, GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetList()
        {
            return Json(new
            {
                players = _playerManager.GetPlayersList(),
                userPlayer = _playerManager.GetUserPlayer(GetCurrentUserId())
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult Assign(int id)
        {
            return Json(new { success = _playerManager.Assign(id, GetCurrentUserId()) });
        }
    }
}