using AutoFix.Models;
using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using AutoFix.Repository;
using AutoFix.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace AutoFix.Controllers
{
    public class OperatorManageController : Controller
    {

        private readonly FailureRepo _failureRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<AplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public OperatorManageController(FailureRepo failureRepo, UserManager<ApplicationUser> userManager, RoleManager<AplicationRole> _roleManager, IMapper mapper)
        {
            _failureRepo = failureRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFailureLogging()
        {
            var failures = _failureRepo.Get(x => x.FailureStatus == FailureStatus.Alındı.ToString()).ToList();
            var x = _userManager.GetUsersInRoleAsync("Teknisyen").Result;
            var tech = x.OfType<ApplicationUser>();
            ViewBag.Technicians = tech;
            return View(failures);
        }
        public IActionResult GetFailureStatus(string id)
        {

            var data = _failureRepo.GetStatus(id).ToList().Select(x => _mapper.Map<FailureLoggingViewModel>(x)).ToList();
            var x =  _userManager.GetUsersInRoleAsync("Teknisyen").Result;
            var tech = x.OfType<ApplicationUser>();
            ViewBag.Technicians = tech;
            return View(data);
        }
       [HttpPost]
        public IActionResult TechnicianRoute(string technicianId,string failureId)
        {
            var data = _failureRepo.GetById(Guid.Parse(failureId));
            data.TechnicianId = technicianId;
            var result = _failureRepo.IsTech(technicianId).ToList();
            if (result.Count>0)
            {
                data.FailureStatus = FailureStatus.Beklemede.ToString(); 
            }
            else
            {
                data.FailureStatus = FailureStatus.Yönlendirildi.ToString();
            }
            _failureRepo.Update(data);
            return RedirectToAction("Index", "Home");
        }
    }
}
