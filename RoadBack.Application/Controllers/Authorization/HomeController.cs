using Denunciation.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace RoadBack.Application.Controllers.Authorization
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }

        public IActionResult SecuredZone()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
