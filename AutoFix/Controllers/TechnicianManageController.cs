using AutoFix.Extensions;
using AutoFix.Migrations;
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

        #region Shop
        //Teknisyen verdiği hizmetler için işlemler
        [HttpGet]
        public IActionResult ServiceProductGet()
        {
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
            var failure = _failureRepo.GetByTechnicianId(user.Id);
            if (failure == null)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Arıza kayıtlarında  bir sorun oluştu"
                });

            }
            CartItem cartItem = new CartItem()
            {
                Failure = failure,
                ServiceProduct = serviceProduct,
                CreatedUser = user.Id,
                UserId = failure.CreatedUser
            };
            var result = _cartRepo.Insert(cartItem);
            _cartRepo.Save();

            return View();
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
