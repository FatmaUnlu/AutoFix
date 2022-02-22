using AutoFix.Extensions;
using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using AutoFix.Repository;
using AutoFix.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFix.Controllers
{
    public class CustomerManageController : CustomerBaseController
    {
        private readonly FailureRepo _failureRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CustomerManageController(FailureRepo failureRepo, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _failureRepo = failureRepo;
            _userManager = userManager;
            _mapper = mapper;
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
        public async Task<IActionResult> FailureLogging(FailureLoggingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            }
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            if (user == null)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı için bir sorun oluştu"
                });

            }
            model.CreatedUser = user.Id;
            model.FailureStatus = FailureStatus.Alındı.ToString();
            var data = _mapper.Map<FailureLogging>(model);
            var result = _failureRepo.Insert(data);
            _failureRepo.Save();
            return View(result);
            //return RedirectToAction("Detail","CustomerManager",new {id = result});
        }

        public async Task<IActionResult> FailureGet()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            var data = _failureRepo.Get(x => x.CreatedUser == user.Id).ToArray().ToList();
            return View(data);
        }
        public IActionResult FailureDelete(Guid id)
        {
            _failureRepo.Delete(id);
            return View();
        }
        [HttpGet]
        public IActionResult FailureUpdate(Guid id)
        {

            var data = _failureRepo.GetById(id);
            if (data == null) return NotFound();// 404 sayfası yapılacak

            //var model = new FailureLoggingViewModel()
            //{
            //    AddressDetail = data.AddressDetail,
            //    FailureDescription = data.FailureDescription,
            //    FailureName = data.FailureName,
            //    FailureSatus = data.FailureStatus,
            //    Latitude = data.Latitude,
            //    Longitude = data.Longitude
            //};
            //Mapper yapılacak
           var model = _mapper.Map<FailureLoggingViewModel>(data);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> FailureUpdate(FailureLogging model)
        {



            //_failureRepo.Update(model);
            //return View();
            return View();

        }

    }
}
