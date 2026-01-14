using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoAppFrontend.Services;

namespace TodoAppFrontend.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IAuthService authService) : base(authService)
        {
        }

        public ActionResult Index()
        {
            if (!AuthService.IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult About()
        {
            if (!AuthService.IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            if (!AuthService.IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}