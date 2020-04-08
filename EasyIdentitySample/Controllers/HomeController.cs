using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyIdentitySample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace EasyIdentitySample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _accessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }

        [Authorize]
        public IActionResult Index()
        {
            var isInRole = _accessor.HttpContext.User.IsInRole("Admin");
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}