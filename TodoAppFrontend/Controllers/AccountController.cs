using System.Threading.Tasks;
using System.Web.Mvc;
using TodoAppFrontend.Models;
using TodoAppFrontend.Services;
using TodoAppFrontend.Source;

namespace TodoAppFrontend.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IAuthService authService) : base(authService)
        {
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await AuthService.LoginAsync(model.Username, model.Password);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Result result = await AuthService.RegisterAsync(model.Username, model.Password);

            if (result)
            {
                ModelState.AddModelError("", "Successfully registered");
            }
            else
            {
                ModelState.AddModelError("", result.ErrorMessage);
            }
                
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await AuthService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
