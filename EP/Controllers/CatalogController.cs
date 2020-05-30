using OneC.BusinessLogic.Managers;
using OneC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OneC.Controllers
{
    public class CatalogController : EPController
    {
        private readonly ITableColumnManager _tableColumnManager;
        private readonly ITableRowManager _tableRowManager;

        public CatalogController
        (
            ITableColumnManager tableColumnManager,
            ITableRowManager tableRowManager
        )
        {
            _tableColumnManager = tableColumnManager;
            _tableRowManager = tableRowManager;
        }

        [HttpGet]
        public JsonResult LoadTable()
        {
            return Json(new { tables = _tableColumnManager.GetTable() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetColumns(int id)
        {
            return Json(new { columns = _tableColumnManager.GetColumns(id) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCatalog(string name)
        {
            return Json(new { success = _tableColumnManager.SaveCatalog(name) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRow(List<TableRowViewModel> rows, int tableId)
        {
            return Json(new { success = _tableRowManager.SaveRow(rows, tableId) }, JsonRequestBehavior.AllowGet);
        }
    }
}