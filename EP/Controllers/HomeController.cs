using OneC.BusinessLogic.Managers;
using System.Web.Mvc;

namespace OneC.Controllers
{
    public class HomeController : EPController
    {
        private readonly ITableColumnManager _tableColumnManager;
        private readonly ITableItemManager _tableItemManager;

        public HomeController
        (
            ITableColumnManager tableColumnManager,
            ITableItemManager tableItemManager
        )
        {
            _tableColumnManager = tableColumnManager;
            _tableItemManager = tableItemManager;
        }

        public ActionResult Index()
        {
            return View();
        }       
    }
}