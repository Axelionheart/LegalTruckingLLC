using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Web.Ui.Membership;
using LegalTrucking.IntakePlus.Web.Ui.Models.account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LegalTrucking.IntakePlus.Web.Ui.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private ICustomMembership _membership;

        public AccountController(ICustomMembership membership)
        {
            _membership = membership;
        }

        [HttpGet("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            await HttpContext.SignOutAsync("Cookies");

            TempData["returnUrl"] = returnUrl;
            return View("Register");
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPostAsync(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            var result = await _membership.RegisterAsync(model.UserName, model.Email, model.Password);
            if (result.Failed)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View("Register", model);
            }

            return LocalRedirect(_membership.Options.DefaultPathAfterLogin);
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            await HttpContext.SignOutAsync("Cookies");

            TempData["returnUrl"] = returnUrl;
            return View("Login");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginPostAsync(LoginModel user, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View("Login", user);
            }

            var result = await _membership.LoginAsync(user.UserName, user.Password);

            if (result.Failed)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View("Login", user);
            }

            return LocalRedirect(returnUrl ?? _membership.Options.DefaultPathAfterLogin);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Return()
        {
           return LocalRedirect(_membership.Options.DefaultPathAfterLogout);
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await _membership.LogoutAsync();

            if (_membership.Options.DefaultPathAfterLogout != null)
            {
                return Redirect(_membership.Options.DefaultPathAfterLogout);
            }
            else
            {
                return RedirectToAction("login");
            }
        }

    }
}
