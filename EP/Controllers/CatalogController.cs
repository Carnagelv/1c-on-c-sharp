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
        
        [HttpGet]
        public JsonResult DeleteRow(int id)
        {
            return Json(new { success = _tableRowManager.DeleteRow(id) }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult SaveColumn(string name, int tableId, int parentId)
        {
            return Json(new { success = _tableColumnManager.SaveColumn(name, tableId, parentId) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveValue(string value, int rowId, int columnId)
        {
            return Json(new { success = _tableColumnManager.SaveValue(value, rowId, columnId) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditValue(string value, int rowId, int columnId, int valueId)
        {
            return Json(new { success = _tableColumnManager.EditValue(value, rowId, columnId, valueId) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteValue(int valueId)
        {
            return Json(new { success = _tableColumnManager.DeleteValue(valueId) }, JsonRequestBehavior.AllowGet);
        }
    }
}