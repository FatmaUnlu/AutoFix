using AutoFix.Extensions;
using AutoFix.Models.Payment;
using AutoFix.Services;
using AutoFix.ViewModels;
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

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService= paymentService;
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
                AddressModel = new AddressModel(),
                BasketModel = new List<BasketModel>(),
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

        [Authorize]
        public IActionResult Purchase()
        {
            
            return View();
        }
    }
}
