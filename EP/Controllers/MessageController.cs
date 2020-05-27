using EP.BusinessLogic.Managers;
using EP.BusinessLogic.Models;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EP.Controllers
{
    public class MessageController : EPController
    {
        private readonly IMessageManager _messageManager;

        public MessageController(IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetDialogs()
        {
            return Json(new { dialogs = _messageManager.GetDialogs(GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetDialog(int dialogId)
        {
            return Json(new { dialog = _messageManager.GetDialog(GetCurrentUserId(), dialogId) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult IsExistNewMessage(int dialogId)
        {
            return Json(new { success = _messageManager.IsExistNewMessage(GetCurrentUserId(), dialogId) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult SentMessage(int dialogId, string message)
        {
            return Json(new { dialog = _messageManager.SaveMessage(GetCurrentUserId(), dialogId, message) });
        }

        [Authorize]
        [HttpPost]
        public JsonResult CreateDialog(int recipientId)
        {
            return Json(new { success = true, id = _messageManager.CreateDialog(GetCurrentUserId(), recipientId) });
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetUsersForDialog()
        {
            return Json(new { users = _messageManager.GetUsersForDialog(GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }
    }
}