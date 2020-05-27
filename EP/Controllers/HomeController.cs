using OneC.BusinessLogic.Managers;
using OneC.BusinessLogic.Models;
using OneC.EntityData.Helpers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace OneC.Controllers
{
    public class HomeController : EPController
    {
        private readonly IUserProfileManager _userProfileManager;

        public HomeController(IUserProfileManager userProfileManager)
        {
            _userProfileManager = userProfileManager;
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Feed");
            }

            return View();
        }

        public ActionResult LogOff()
        {
            Session.Clear();

            HttpCookie cookie = HttpContext.Request.Cookies.Get("isActive");

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult Register(RegisterModel user)
        {
            if (!ModelState.IsValid || !_userProfileManager.CheckName(user.FirstName) || !_userProfileManager.CheckName(user.LastName) || !_userProfileManager.CheckEmail(user.UserName))
            {
                return Json(new { success = false, errorMessage = "Datu formāts bija nekorekts" });
            }

            if (ModelState.IsValid && !WebSecurity.UserExists(user.UserName))
            {
                var firstName = TextHelper.ToUpperCaseFirstLetter(user.FirstName);
                var lastName = TextHelper.ToUpperCaseFirstLetter(user.LastName);

                WebSecurity.CreateUserAndAccount(user.UserName.Trim(), user.RePassword.Trim(), new { firstName, lastName, CreateDate = DateTime.Now, LastActivityDate = DateTime.Now, Photo = "/Content/img/no-logo.jpg", IsActive = false }, false);
                WebSecurity.Login(user.UserName, user.Password);
                FormsAuthentication.SetAuthCookie(user.UserName, false);

                _userProfileManager.AddMainRole(user.UserName.Trim(), UserRoles.SportsMan);

                return Json(new { success = true });
            }

            return Json(new { success = false, errorMessage = "Lietotāja vārds jau ir aizņēmts" });
        }

        [HttpPost]
        public JsonResult Login(LoginModel user)
        {
            if (!ModelState.IsValid || !_userProfileManager.CheckEmail(user.UserName))
            {
                return Json(new { success = false, errorMessage = "Datu formāts bija nekorekts" });
            }

            if (ModelState.IsValid && WebSecurity.Login(user.UserName, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.UserName, true);

                HttpCookie existingCookie = HttpContext.Request.Cookies.Get("isActive");

                if (existingCookie == null)
                {
                    if (_userProfileManager.IsActived(user.UserName))
                    {
                        HttpCookie cookie = new HttpCookie("isActive");
                        cookie.Expires.AddDays(30);
                        HttpContext.Response.SetCookie(cookie);
                    }
                }

                _userProfileManager.UpdateLastActivtyDate(user.UserName);                

                return Json(new { success = true });
            }

            return Json(new { success = false, errorMessage = "Lietotāja vārds vai parole nav pareiza" });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string token, string password)
        {
            if (_userProfileManager.ResetPasswordFinal(token, password))
            {
                return RedirectToAction("Index", "Feed");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}