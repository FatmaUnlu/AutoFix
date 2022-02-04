using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Areas.Admin.Controllers
{
    public class UserApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
