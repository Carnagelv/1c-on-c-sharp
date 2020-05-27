using EP.BusinessLogic.Managers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EP.Controllers
{
    public class MovieController : EPController
    {
        private readonly IMovieManager _movieManager;

        public MovieController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }       

        [Authorize]
        [HttpGet]
        public JsonResult GetList(List<int> seasons, bool firstEnter)
        {
            return Json(new
            {
                movies = _movieManager.GetMovies(seasons, firstEnter),
                movieSeasons = _movieManager.GetMovieSeasons()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}