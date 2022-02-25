using AutoFix.Models;
using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using AutoFix.Repository;
using AutoFix.Services;
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
    //[Authorize(Roles = "Operator")]
    public class OperatorManageController : Controller
    {

        private readonly FailureRepo _failureRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public OperatorManageController(FailureRepo failureRepo, UserManager<ApplicationUser> userManager, IMapper mapper, IEmailSender emailSender)
        {
            _failureRepo = failureRepo;
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
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
        public async Task<IActionResult> TechnicianRoute(string technicianId,string failureId)
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
            var technician =  await _userManager.FindByIdAsync(technicianId);
            var emailMesage = new EmailMessage()
            {
                Contacts = new string[] { technician.Email },
                Body =  data.FailureName+" arıza işlemi tarafınıza tanımlanmıştır.",
                Subject = "Tarafınıza arıza tanımlandı"
            };

            await _emailSender.SendAsyc(emailMesage);




            return RedirectToAction("Index", "Home");
        }
    }
}
