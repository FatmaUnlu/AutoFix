using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Controllers
{
    public class CustomerManageController : CustomerBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
