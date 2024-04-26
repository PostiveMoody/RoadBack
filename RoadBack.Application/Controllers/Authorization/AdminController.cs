using Denunciation.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoadBack.DAL;

namespace RoadBack.Application.Controllers.Authorization
{
    [Authorize]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet]
        [Route("[action]")]
        public IActionResult Administrator()
        {
            return View();
        }

        [Authorize(Policy = "Manager")]
        [HttpGet]
        [Route("[action]")]
        public IActionResult Manager()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public IActionResult LogIn(string returnUrl)
        {
            return View();
        }

        
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
