using Kolisetka.MVC.Contracts;
using Kolisetka.MVC.Models.User;
using Kolisetka.MVC.Properties;
using Microsoft.AspNetCore.Mvc;

namespace Kolisetka.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            if (!string.IsNullOrEmpty(login.Email) && !string.IsNullOrEmpty(login.Password))
            {
                var isLoggenIn = await _authService.Authenticate(login);
                if (isLoggenIn)
                    return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError("", Resources.UserController_LoginFailed);

            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM userRegister)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Content("~/");
                var isCreated = await _authService.Register(userRegister);
                if (isCreated)
                    return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("", Resources.UserController_RegisterFailed);

            return View(userRegister);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await _authService.Logout();

            return LocalRedirect(returnUrl);
        }
    }
}
