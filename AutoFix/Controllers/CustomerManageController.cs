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
        public async Task<IActionResult> FailureLogging(string lat, string lng, FailureLogging model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
                return View(model);
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
            model.Latitude = lat;
            model.Longitude = lng;
            model.UserId = user.Id;
            model.FailureStatus = FailureStatus.Alındı.ToString();
            var result = _failureRepo.Insert(model);
            _failureRepo.Save();

            return View();
        }

        public async Task<IActionResult> FailureGet()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            var data = _failureRepo.Get(x => x.UserId == user.Id).ToArray().ToList();


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

            var model = new FailureLoggingViewModel()
            {
                AddressDetail = data.AddressDetail,
                FailureDescription = data.FailureDescription,
                FailureName = data.FailureName,
                FailureSatus = data.FailureStatus,
                Latitude = data.Latitude,
                Longitude = data.Longitude
            };
            //Mapper yapılacak
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
