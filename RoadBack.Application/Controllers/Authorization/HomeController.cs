using Microsoft.AspNetCore.Mvc;

namespace RoadBack.Application.Controllers.Authorization
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult SecuredZone()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
