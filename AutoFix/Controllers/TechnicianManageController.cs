using AutoFix.Models.Identity;
using AutoFix.Repository;
using AutoFix.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AutoFix.Controllers
{
    public class TechnicianManageController : TechnicianBaseController
    {
        private readonly ServiceProductRepo _serviceProductRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public TechnicianManageController(ServiceProductRepo serviceProductRepo, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _serviceProductRepo = serviceProductRepo;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ServiceProductGet()
        
        {
            var data = _serviceProductRepo.Get().ToList().Select(x => _mapper.Map<ServiceProductViewModel>(x)).ToList(); 
            return View(data);

           
        }


    }
}
