using AutoFix.Models.Identity;
using AutoFix.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Controllers
{
    public class OperatorManageController : Controller
    {

        private readonly FailureRepo _failureRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public OperatorManageController(FailureRepo failureRepo, UserManager<ApplicationUser> userManager)
        {
            _failureRepo = failureRepo; 
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFailureLogging()
        {
            var failures = _failureRepo.Get(x => x.FailureStatus == x.FailureStatus.ToString());


            return View();
        }
    }
}
