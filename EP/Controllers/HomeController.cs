using OneC.BusinessLogic.Managers;
using System.Web.Mvc;

namespace OneC.Controllers
{
    public class HomeController : EPController
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }       
    }
}