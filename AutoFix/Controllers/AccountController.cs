using AutoFix.Extensions;
using AutoFix.Models;
using AutoFix.Models.Identity;
using AutoFix.Services;
using AutoFix.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AutoFix.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<AplicationRole> _roleManager;
        private readonly IMapper _mapper;


        public AccountController(IEmailSender emailSender, UserManager<ApplicationUser> userManager, RoleManager<AplicationRole> roleManager, IMapper mapper)
        {
           
            _emailSender = emailSender;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            CheckRoles();
        }
        private void CheckRoles()
        {
            foreach (var roleName in RoleNames.Roles)
            {
                if (!_roleManager.RoleExistsAsync(roleName).Result)
                {
                    var result = _roleManager.CreateAsync(new AplicationRole()
                    {
                        Name = roleName,
                    }).Result;
                }
            }
        }

        public async Task SendConfirmEmailAsync(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

            var emailMesage = new EmailMessage()
            {
                Contacts = new string[] { user.Email },
                Body = $"Please confirm your account by <a href ='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>",
                Subject = "Confirm your email"
            };

            await _emailSender.SendAsyc(emailMesage);

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Password = string.Empty;
                model.ConfirmPassword = string.Empty;
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.UserName), "Bu kullanıcı adı daha önce sisteme kayıt edilmiştir.");
                return View(model);
            }

            user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Bu kullanıcı adı daha önce sisteme kayıt edilmiştir.");
                return View(model);
            }

            user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //kullanıcıya rol atama 
                var count = _userManager.Users.Count();

                result = await _userManager.AddToRoleAsync(user, count == 1 ? RoleNames.Admin : RoleNames.Passive);

                //kullanıcıya mail dogrulaması atma
                await SendConfirmEmailAsync(user);
            }
            else
            {
                ModelState.AddModelError(string.Empty, ModelState.ToFullErrorString());

                return View(model);
            }

            return View();
        }

        [HttpGet]
      
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($" Unable to load user with ID'{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            ViewBag.StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";

            if (result.Succeeded && _userManager.IsInRoleAsync(user, RoleNames.Passive).Result)
            {
                await _userManager.RemoveFromRoleAsync(user, RoleNames.Passive);
                await _userManager.AddToRoleAsync(user, RoleNames.Musteri);
            }

            return View();
        }
    }
}
