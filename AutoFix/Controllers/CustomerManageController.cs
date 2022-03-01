using AutoFix.Extensions;
using AutoFix.Models;
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
using System.Threading;
using System.Threading.Tasks;
//using System.Web.Http;

namespace AutoFix.Controllers
{
    [Authorize(Roles = "Müşteri")]
    public class CustomerManageController : BaseController
    {
        #region dependency
        private readonly FailureRepo _failureRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CartRepo _cartRepo;
        private readonly ServiceProductRepo _serviceProductRepo;
        private readonly IPaymentService _paymentService;
        private readonly IEmailSender _emailSender;
        #endregion

        public CustomerManageController(FailureRepo failureRepo, IMapper mapper, UserManager<ApplicationUser> userManager, CartRepo cartRepo, ServiceProductRepo serviceProductRepo, IPaymentService paymentService, IEmailSender emailSender)
        {
            _failureRepo = failureRepo;
            _mapper = mapper;
            _userManager = userManager;
            _cartRepo = cartRepo;
            _serviceProductRepo = serviceProductRepo;
            _paymentService = paymentService;
            _emailSender = emailSender;

            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        public IActionResult FailureLogging()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FailureLogging(string lat,string lng,FailureLoggingViewModel model)
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
            model.Longitude = lng;
            model.Latitude = lat;
            model.CreatedUser = user.Id;
            model.FailureStatus = FailureStatus.Alındı.ToString();
            var data = _mapper.Map<FailureLogging>(model);
            var result = _failureRepo.Insert(data);
            _failureRepo.Save();
            var emailMesage = new EmailMessage()
            {
                Contacts = new string[] { user.Email },
                Body = "Arıza kaydınız alınmıştır.",
                Subject = "Arıza kayıt bilgilendirme mailidir."
            };
            await _emailSender.SendAsyc(emailMesage);

            return RedirectToAction("FailureGet", "CustomerManage");
            //return View(result);result son kaydın idsini tutar.
            //return RedirectToAction("Detail","CustomerManager",new {id = result});
        }

        public async Task<IActionResult> FailureGet()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            var data = _failureRepo.Get(x => x.CreatedUser == user.Id&&x.IsDeleted==false).ToArray().ToList();
            return View(data);
        }
        public IActionResult FailureDelete(Guid id)
        {
            //Soft deleted
            var data = _failureRepo.GetById(id);
            data.IsDeleted = true;
            _failureRepo.Update(data);
            //Silme için bildirim verdir
            return RedirectToAction("FailureGet", "CustomerManage");
        }
        [HttpGet]
        public IActionResult FailureUpdate(Guid id)
        {

            var data = _failureRepo.GetById(id);
            if (data == null) return NotFound();// 404 sayfası yapılacak
           var model = _mapper.Map<FailureLoggingViewModel>(data);
            return View(model);
        }
        [HttpPost]
        public IActionResult FailureUpdate(string lat, string lng, FailureLoggingViewModel model)
        {
            model.Latitude = lat;
            model.Longitude = lng;
            var data = _mapper.Map<FailureLogging>(model);
            //Güncelleme için bildirim verdir
            _failureRepo.Update(data);
            return RedirectToAction("FailureGet", "CustomerManage");
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
            
            foreach (var item in shopcart)
            {
                var failure = _failureRepo.GetById(item.FailureId);
                item.Failure = failure;
                var product = _serviceProductRepo.GetById(item.ServiceProductId);
                item.ServiceProduct = product;
            }
            var modelPayment = new PaymentViewModel();
            modelPayment.CartItem = shopcart;
            return View(modelPayment);
        }

        [HttpPost]
        public IActionResult Basket(PaymentViewModel model)
        {
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
                RegistrationDate = $"{user.CreateDate:yyyy-MM-dd HH:mm:ss}",
                RegistrationAddress = "hdthty",
            };
            var modelPayment = new PaymentModel()
            {
                AddressModel=addressModel,
                Installment = model.Installment,
                //CartItem =shopcart,
                BasketModel = basketModel, //new List<BasketModel>(){ basketModel },
                CardModel=model.CardModel,
                Price=model.Amount,
                UserId = HttpContext.GetUserId(),
                Customer=customerModel,
                Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString()
            };
           

            var installmentInfo = _paymentService.CheckInstallment(modelPayment.CardModel.CardNumber.Substring(0, 6), modelPayment.Price);

            var installmentNumber = installmentInfo.InstallmentPrices.FirstOrDefault(x => x.InstallmentNumber == model.Installment);

            modelPayment.PaidPrice = decimal.Parse(installmentNumber != null ? installmentNumber.TotalPrice : installmentInfo.InstallmentPrices[0].TotalPrice);

            //legacy code
            var result = _paymentService.Pay(modelPayment);
            if(result.Status== "success")
            {
                foreach (var item in shopcart)
                {
                    item.OrderStatus = OrderStatus.Odendi.ToString();
                    _cartRepo.Update(item);
                }
                var email = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Body = "Ödeme İşleminiz Başarılı Bir Şekilde Gerçekleşmiştir.",
                    Subject = "Başarılı Ödeme"
                };

                await _emailSender.SendAsyc(email);
            }
            
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
