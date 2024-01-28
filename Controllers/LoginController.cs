using Forum_Management_System.Models.View;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authService;
        public LoginController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_Login", new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel form)
        {
            var token = await this._authService.RequestToken(form.Email, form.Password);
            Response.Cookies.Append("X-Access",
                                        token.Token,
                                        new CookieOptions
                                        {
                                            HttpOnly = true,
                                            SameSite = SameSiteMode.Strict
                                        });

            return RedirectToAction("Index", "Home");
        }
    }
}
