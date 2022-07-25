using Kolisetka.MVC.Contracts;
using Kolisetka.MVC.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Kolisetka.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authService;

        public UserController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult Login(string? returnUrl = null)
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

            ModelState.AddModelError("", "Log attempt failed. Please try again.");

            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
