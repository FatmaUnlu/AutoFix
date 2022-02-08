using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Controllers
{
    public class TechnicianManageController : TechnicianBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
