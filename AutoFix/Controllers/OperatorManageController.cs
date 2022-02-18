using AutoFix.Models;
using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using AutoFix.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages.Html;

namespace AutoFix.Controllers
{
    public class OperatorManageController : Controller
    {

        private readonly FailureRepo _failureRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<AplicationRole> _roleManager;

        public OperatorManageController(FailureRepo failureRepo, UserManager<ApplicationUser> userManager, RoleManager<AplicationRole> _roleManager)
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
            var failures = _failureRepo.Get(x => x.FailureStatus == FailureStatus.Alındı.ToString()).ToList();

            //var Technicians = new List<SelectListItem>();

            var x = _userManager.GetUsersInRoleAsync("Teknisyen").Result;
            var tech = x.OfType<ApplicationUser>();

            //foreach (var item in tech)
            //{
            //    Technicians.Add(new SelectListItem
            //    {
            //        Text = $"{item.Name} {item.Surname}",
            //        Value = item.Id.ToString()
            //    });
            //}

            ViewBag.Technicians = tech;
            return View(failures);
        }
        [HttpPost]
        public IActionResult GetFailureLogging(string[] Technician)
        {
            var failures = _failureRepo.Get(x => x.FailureStatus == FailureStatus.Alındı.ToString()).ToList();

            var x = _userManager.GetUsersInRoleAsync("Teknisyen").Result;

            var techId = _userManager.GetUserId(x => x.RollNames == "Teknisyen");
            var tech = x.OfType<ApplicationUser>();



            //foreach (var item in tech)
            //{
            //    Technicians.Add(new SelectListItem
            //    {
            //        Text = $"{item.Name} {item.Surname}",
            //        Value = item.Id.ToString()
            //    });
            //}

            ViewBag.Technicians = tech;
            return View(failures);
        }
        [HttpPost]
        public IActionResult GetFailureLogging(string[] Technician)
        {
            //var failures = _failureRepo.GetById(id)
          
          



            return View();
        }


    }
}
