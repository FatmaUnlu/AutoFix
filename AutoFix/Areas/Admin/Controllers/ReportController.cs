using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Areas.Admin.Controllers
{
    public class ReportController : AdminBaseController
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
