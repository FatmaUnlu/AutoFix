using AutoFix.Inject;
using AutoFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AutoFix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMyDependency _myDependency;

        public HomeController(ILogger<HomeController> logger, IMyDependency myDependency)
        {
            _logger = logger;
            _myDependency = myDependency;
        }

        public IActionResult Index()
        {
            _myDependency.Log("Home/Index'e girildi");
            return View();
        }

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
