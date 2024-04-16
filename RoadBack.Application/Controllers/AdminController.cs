using Denunciation.Application.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Denunciation.Application.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var claims = new List<Claim>()
            {
                new Claim("Demo","Value")
            };

            var claimIdentity = new ClaimsIdentity(claims,"Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            HttpContext.SignInAsync("Cookie", claimPrincipal);

            return Redirect(model.ReturnUrl);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Index");
        }
    }
}
