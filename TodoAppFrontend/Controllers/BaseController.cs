using System.Web.Mvc;
using TodoAppFrontend.Services;

namespace TodoAppFrontend.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IAuthService AuthService;

        public BaseController(IAuthService authService)
        {
            AuthService = authService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthService.IsLoggedIn())
            {
                ViewBag.CurrentUser = AuthService.GetCurrentUser();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
