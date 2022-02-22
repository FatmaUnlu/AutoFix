using AutoFix.Extensions;
using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using AutoFix.Repository;
using AutoFix.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;

namespace AutoFix.Controllers
{
    [Authorize(Roles = "Müşteri")]
    public class CustomerManageController : BaseController
    {
        private readonly FailureRepo _failureRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CartRepo _cartRepo;
        private readonly ServiceProductRepo _serviceProductRepo;

        public CustomerManageController(FailureRepo failureRepo, IMapper mapper, UserManager<ApplicationUser> userManager, CartRepo cartRepo, ServiceProductRepo serviceProductRepo)
        {
            _failureRepo = failureRepo;
            _mapper = mapper;
            _userManager = userManager;
            _cartRepo = cartRepo;
            _serviceProductRepo = serviceProductRepo;
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
        public async Task<IActionResult> Basket()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());

            var shopcart = _cartRepo.Get(x => x.CustomerId == user.Id && x.OrderStatus == OrderStatus.Odeme_Bekliyor.ToString()).Select(x => _mapper.Map<CartItemViewModel>(x)).ToList();
            if(shopcart.Count==0)
            {
                return View();
            }
            foreach (var item in shopcart)
            {
                var failure = _failureRepo.GetById(item.FailureId);
                item.Failure = failure;
                var product = _serviceProductRepo.GetById(item.ServiceProductId);
                item.ServiceProduct = product;
            }
            
            

            return View(shopcart);
        }

    }
}
