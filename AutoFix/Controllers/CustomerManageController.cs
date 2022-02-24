using AutoFix.Extensions;
using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using AutoFix.Models.Payment;
using AutoFix.Repository;
using AutoFix.Services;
using AutoFix.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IPaymentService _paymentService;

        public CustomerManageController(FailureRepo failureRepo, IMapper mapper, UserManager<ApplicationUser> userManager, CartRepo cartRepo, ServiceProductRepo serviceProductRepo, IPaymentService paymentService)
        {
            _failureRepo = failureRepo;
            _mapper = mapper;
            _userManager = userManager;
            _cartRepo = cartRepo;
            _serviceProductRepo = serviceProductRepo;
            _paymentService = paymentService;
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
        [HttpGet]
        public async Task<IActionResult> Basket()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());

            var shopcart = _cartRepo.Get(x => x.CustomerId == user.Id && x.OrderStatus == OrderStatus.Odeme_Bekliyor.ToString()).ToList();
                //.Select(x => _mapper.Map<CartItemViewModel>(x)).ToList();
            if(shopcart.Count==0)
            {
                return View();
            }
            decimal total;
            foreach (var item in shopcart)
            {
                var failure = _failureRepo.GetById(item.FailureId);
                item.Failure = failure;
                var product = _serviceProductRepo.GetById(item.ServiceProductId);
                item.ServiceProduct = product;
                

            }
            var modelPayment = new PaymentViewModel();
            modelPayment.CartItem = shopcart;

            //TempData["model"] = "emre";
            //ViewData["model2"] = modelPayment;
            return View(modelPayment);
        }

        [HttpPost]
        public IActionResult Basket(PaymentViewModel model)
        {
            //decimal amount=model.Amount;
            TempData["Amount"]=model.Amount.ToString();
            return RedirectToAction("Purchase", "CustomerManage");
        }

        [HttpGet]
        public IActionResult Purchase()
        {
            var amount =TempData["Amount"];
            ViewBag.Total = amount;
            return View();

            
        }
        [HttpPost]
        public async Task<IActionResult> Purchase(PaymentViewModel model)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            var basketModel = new List<BasketModel>();
            var shopcart = _cartRepo.Get(x => x.CustomerId == user.Id && x.OrderStatus == OrderStatus.Odeme_Bekliyor.ToString()).ToList();
            //.Select(x => _mapper.Map<CartItemViewModel>(x)).ToList();
            if (shopcart.Count == 0)
            {
                return View();
            }

            foreach (var item in shopcart)
            {
                var failure = _failureRepo.GetById(item.FailureId);
                item.Failure = failure;
                var product = _serviceProductRepo.GetById(item.ServiceProductId);
                item.ServiceProduct = product;
                basketModel.Add(_mapper.Map <BasketModel>(product));

            }
            var addressModel = new AddressModel()
            {
                City = "Sivas",
                ContactName = "Bernaaaa",
                Country = "Turkiye",
                Description = "Efsane",
                ZipCode = "58"
            };

            var customerModel = new CustomerModel()
            {
                City = "İstanbul",
                Country = "Turkiye",
                Email = user.Email,
                GsmNumber = user.PhoneNumber,
                Id = user.Id,
                IdentityNumber = user.Id,
                Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                ZipCode = addressModel.ZipCode,
                LastLoginDate = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                RegistrationAddress = "hdthty",
                //RegistrationDate = $"{user.CreateDate:yyyy-MM-dd HH:mm:ss}"

            };
         

            var modelPayment = new PaymentModel()
            {
                AddressModel=addressModel,
                Installment = model.Installment,
                CartItem =shopcart,
                BasketModel = basketModel, //new List<BasketModel>(){ basketModel },
                CardModel=model.CardModel,
                Price=model.Amount,
                UserId = HttpContext.GetUserId(),
                CustomerModel=customerModel,
                Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString()
            };
           

            var installmentInfo = _paymentService.CheckInstallment(modelPayment.CardModel.CardNumber.Substring(0, 6), modelPayment.Price);

            var installmentNumber = installmentInfo.InstallmentPrices.FirstOrDefault(x => x.InstallmentNumber == model.Installment);

            //modelPayment.PaidPrice = decimal.Parse(installmentNumber != null ? installmentNumber.TotalPrice : installmentInfo.InstallmentPrices[0].TotalPrice);
            modelPayment.PaidPrice =modelPayment.Price;

            //legacy code
            var result = _paymentService.Pay(modelPayment);
            return View();

        }


        [HttpPost]
        public IActionResult CheckInstallment(string binNumber, decimal price)
        {
            if (binNumber.Length < 6 || binNumber.Length > 16) return BadRequest(new
            {
                Message = "Bad Request"
            });

            var result = _paymentService.CheckInstallment(binNumber, price);
            return Ok(result);
        }

    }
}
