using OneC.BusinessLogic.Managers;
using System.Web.Mvc;

namespace OneC.Controllers
{
    public class CatalogController : EPController
    {
        private readonly ITableColumnManager _tableColumnManager;
        private readonly ITableItemManager _tableItemManager;

        public CatalogController
        (
            ITableColumnManager tableColumnManager,
            ITableItemManager tableItemManager
        )
        {
            _tableColumnManager = tableColumnManager;
            _tableItemManager = tableItemManager;
        }

        [HttpGet]
        public JsonResult LoadTable()
        {
            return Json(new { tables = _tableColumnManager.GetTable() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCatalog(string name)
        {
            return Json(new { success = _tableColumnManager.SaveCatalog(name) }, JsonRequestBehavior.AllowGet);
        }
    }
}