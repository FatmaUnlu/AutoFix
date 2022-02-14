using AutoFix.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoFix.Controllers
{
    public class CustomerManageController : CustomerBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FailureLogging()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FailureLogging(string lat, string lng, FailureLogging model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(model);
            }






            return Ok(model);
        }
    }
}
