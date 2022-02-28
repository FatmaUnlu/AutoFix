using AutoFix.Extensions;
using AutoFix.Models.Entities;
using AutoFix.Models.Payment;
using AutoFix.Repository;
using AutoFix.Services;
using AutoFix.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoFix.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ServiceProductRepo _serviceProductRepo;
        private readonly IMapper _mapper; 

        public PaymentController(IPaymentService paymentService, ServiceProductRepo serviceProductRepo, IMapper mapper)
        {
            _paymentService= paymentService;
            _serviceProductRepo = serviceProductRepo;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult CheckInstallment(string binNumber, decimal price)
        {
            if (binNumber.Length < 6 || binNumber.Length > 16) return BadRequest(new
            {
                Message = "Bad Request"
            });

            var result = _paymentService.CheckInstallment(binNumber, price);
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Index(PaymentViewModel model)
        {
            var paymentModel = new PaymentModel()
            {
                Installment = model.Installment,
                //AddressModel = new AddressModel(),
                //BasketModel = new List<BasketModel>(),
                CustomerModel = new CustomerModel(),
                CardModel = model.CardModel,
                Price = 1000,
                UserId = HttpContext.GetUserId(),
                Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            };

            var installmentInfo = _paymentService.CheckInstallment(paymentModel.CardModel.CardNumber.Substring(0, 6), paymentModel.Price);

            var installmentNumber = installmentInfo.InstallmentPrices.FirstOrDefault(x => x.InstallmentNumber == model.Installment);

            paymentModel.PaidPrice = decimal.Parse(installmentNumber != null ? installmentNumber.TotalPrice.Replace('.', ',') : installmentInfo.InstallmentPrices[0].TotalPrice.Replace('.', ','));
        
            var result = _paymentService.Pay(paymentModel);
            return View();
        }

        //[Authorize]
        //public IActionResult Purchase(List<CartItemViewModel> model )
        //{
        //    //var data = _serviceProductRepo.GetById(id);

        //    //if (data == null)
        //    //{
        //    //    return RedirectToAction("Index", "Home");
        //    //}

        //    var model = _mapper.Map<ServiceProductViewModel>(data);
        //    ViewBag.ServiceProduct = model;

        //    var model2  = new  PaymentViewModel()
        //    {
        //        CartItem = new CartItem()
        //        {
                    
        //            ServiceProduct = data.Name,
        //        }
        //    }

        //    return View();
        //}
    }
}
