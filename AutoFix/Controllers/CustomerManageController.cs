using AutoFix.Models.Entities;
using AutoFix.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoFix.Controllers
{
    public class CustomerManageController : CustomerBaseController
    {
        private readonly FailureRepo _failureRepo;

        public CustomerManageController(FailureRepo failureRepo)
        {
            _failureRepo = failureRepo;
        }

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
                return View(model);
            }








            return View(model);
        }
    }
}
