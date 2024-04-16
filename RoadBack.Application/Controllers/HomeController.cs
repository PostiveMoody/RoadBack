using Denunciation.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Denunciation.Application.Controllers
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
            return View();
        }
    }
}
