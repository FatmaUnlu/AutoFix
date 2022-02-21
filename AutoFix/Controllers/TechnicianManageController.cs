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
    public class TechnicianManageController : TechnicianBaseController
    {
        private readonly ServiceProductRepo _serviceProductRepo;
        private readonly FailureRepo _failureRepo;
        private readonly CartRepo _cartRepo;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public TechnicianManageController(ServiceProductRepo serviceProductRepo, UserManager<ApplicationUser> userManager, IMapper mapper, FailureRepo failureRepo, CartRepo cartRepo)
        {
            _serviceProductRepo = serviceProductRepo;
            _userManager = userManager;
            _mapper = mapper;
            _failureRepo = failureRepo;
            _cartRepo = cartRepo;
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
      

        public IActionResult StatusUpdate(string status, string failureId)
        {
            //Failure status güncelleme
            var failure = _failureRepo.GetById(Guid.Parse(failureId));
            failure.FailureStatus = status;
            _failureRepo.Update(failure);
            return View();  
        }

        #region ShopCart
        public IActionResult ShopCart(string id)
        {
            var cartItemProducts = _cartRepo.Get(x => x.FailureId == Guid.Parse(id)).Select(x=>x.ServiceProductId).ToList();
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

        #endregion
            //TODO
            /*
             * Eklenenler tabloda gösterilecek
             * Tabloya remove kolonu eklenecek
             * Ödeme işlemlerine geçilecek7,
             */

        }
}
