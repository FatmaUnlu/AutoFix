﻿using AutoFix.Extensions;
using AutoFix.Models;
using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using AutoFix.Repository;
using AutoFix.Services;
using AutoFix.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFix.Controllers
{
    [Authorize(Roles = "Teknisyen")]
    public class TechnicianManageController : BaseController
    {
        private readonly IEmailSender _emailSender;
        private readonly ServiceProductRepo _serviceProductRepo;
        private readonly FailureRepo _failureRepo;
        public readonly UserManager<ApplicationUser> _userManager;

        private readonly CartRepo _cartRepo;
       
        private readonly IMapper _mapper;


        public TechnicianManageController(ServiceProductRepo serviceProductRepo, IMapper mapper, FailureRepo failureRepo, CartRepo cartRepo, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _serviceProductRepo = serviceProductRepo;
            _mapper = mapper;
            _failureRepo = failureRepo;
            _cartRepo = cartRepo;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region TechFailure
        public async Task<IActionResult> TechFailureGet()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            var data = _failureRepo.GetByTechnicianId(user.Id).ToList().Select(x => _mapper.Map<FailureLoggingViewModel>(x)).ToList(); ;

            return View(data);
        }
        #endregion

        #region Shop
        //Teknisyen verdiği hizmetler için işlemler
        [HttpGet]
        public IActionResult ServiceProductGet(string id)
        {
            TempData["FailureId"] = id;
            var data = _serviceProductRepo.Get().ToList().Select(x => _mapper.Map<ServiceProductViewModel>(x)).ToList(); 
            return View(data);
        }
        public async  Task<IActionResult> ServiceProductAdd(Guid id)
        {
            //Teknisyen bilgileri- userTechnian
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            if (user == null)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı için bir sorun oluştu"
                });
            }
            // hizmet-Ürün bilgileri
            var serviceProduct = _serviceProductRepo.GetById(id);
            if (serviceProduct == null)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Hizmet için bir sorun oluştu"
                });

            }
            //failure bilgileri
            string failureId = TempData["FailureId"].ToString();
            var failure = _failureRepo.GetById(Guid.Parse(failureId));

            var cartItem = new CartItem()
            {
                CreatedUser=user.Id,
                Failure = failure,
                FailureId = failure.Id,
                CustomerId = failure.CreatedUser,
                ServiceProductId = serviceProduct.Id,
                OrderStatus=OrderStatus.Eklendi.ToString()
                
            };
            var result = _cartRepo.Insert(cartItem);
            _cartRepo.Save();

            return RedirectToAction("index", "home");
            //return View();
        }

        #endregion
      

        public async Task<IActionResult> StatusUpdate(string status, string failureId)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());

            //Failure status güncelleme
            var failure = _failureRepo.GetById(Guid.Parse(failureId));
            failure.FailureStatus = status;
            _failureRepo.Update(failure);
            if(status==FailureStatus.Tamamlandi.ToString())
            {
                var customer = await _userManager.FindByIdAsync(failure.CreatedUser);

                var emailMesage = new EmailMessage()
                {
                    Contacts = new string[] { customer.Email },
                    Body = "İşlem bilgilendirme mailidir.",
                    Subject = "Arıza işleminiz tamamlandı"
                };
                await _emailSender.SendAsyc(emailMesage);

            }
            return View();  
        }

        #region ShopCart
       /// [Authorize(Roles = "Müşteri")]
        public IActionResult ShopCart(string id)
        {
            var cartItemProducts = _cartRepo.Get(x => x.FailureId == Guid.Parse(id) && x.OrderStatus==OrderStatus.Eklendi.ToString()).Select(x=>x.ServiceProductId).ToList();

            if (cartItemProducts.Count==0)
            {
                return RedirectToAction("TechFailureGet", "TechnicianManage");
            }
            var failure = _failureRepo.GetById(Guid.Parse(id));

            var shopcart = _cartRepo.Get(x => x.FailureId == Guid.Parse(id)).ToList().Select(x => _mapper.Map<CartItemViewModel>(x)).ToList();

            int sayac = 0;
            foreach (var item in shopcart)
            {
                for (int i = 0; i < cartItemProducts.Count; i++)
                {
                    item.ServiceProduct = _serviceProductRepo.GetById(cartItemProducts[sayac]);
                    sayac++;
                    break;
                }
                item.Failure = failure;
            }
            
            return View(shopcart);
        }

        public async Task<IActionResult> CustomerRoot(Guid id)
        {
            var cartItemProducts = _cartRepo.Get(x => x.FailureId == id && x.OrderStatus==OrderStatus.Eklendi.ToString()).ToList();
            foreach (var item in cartItemProducts)
            {
                item.OrderStatus = OrderStatus.Odeme_Bekliyor.ToString();
                _cartRepo.Update(item);
            }
            
            var user = await _userManager.FindByIdAsync(cartItemProducts[0].CustomerId);
            var emailMesage = new EmailMessage()
            {
                Contacts = new string[] { user.Email },
                Body = "Arızanız giderilmiştir. Ödeme işleminin gerçekleştirebilirsiniz.",
                Subject = "Ödeme bilgilendirme."
            };
            await _emailSender.SendAsyc(emailMesage);
            return RedirectToAction("TechFailureGet", "TechnicianManage");
        }
        #endregion
            //TODO
            /*
             * Eklenenler tabloda gösterilecek
             * Tabloya remove kolonu eklenecek
             * Ödeme işlemlerine geçilecek7,
             */

        }
}
