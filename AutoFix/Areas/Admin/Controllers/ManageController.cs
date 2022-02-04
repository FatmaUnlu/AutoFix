using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult User()
        {
            return View();
        }
    }
}
