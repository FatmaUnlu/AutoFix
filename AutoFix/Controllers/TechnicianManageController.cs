using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Controllers
{
    public class TechnicianManageController : TechnicianBaseController
    {
        public IActionResult ServiceProduct()
        {
            return View();
        }
    }
}
