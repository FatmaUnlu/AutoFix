using AutoFix.Models.Identity;
using AutoFix.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace AutoFix.Areas.Admin.Controllers
{
    public class ManageController : AdminBaseController
    {
        private readonly RoleManager<AplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageController(RoleManager<AplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ServiceProducts()
        {
            return View();
        }
        public IActionResult Deneme()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            List<AplicationRole> allRoles = _roleManager.Roles.ToList();
            List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            List<RoleAssignViewModel> assignRoles = new List<RoleAssignViewModel>();
            allRoles.ForEach(role => assignRoles.Add(new RoleAssignViewModel
            {
                HasAssign = userRoles.Contains(role.Name),
                RoleId = role.Id,
                RoleName = role.Name
            }));

            return View(assignRoles);
        }
        [HttpPost]
        public async Task<ActionResult> RoleAssign(List<RoleAssignViewModel> modelList, string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            foreach (RoleAssignViewModel role in modelList)
            {
                if (role.HasAssign)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            }
            return RedirectToAction("Users", "Manage");
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Deneme(IFormFile file)
        {
            if (file != null)
            {
                string imageExtension = Path.GetExtension(file.FileName);

                string imageName = Guid.NewGuid() + imageExtension;

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Upload/{imageName}");

                using var stream = new FileStream(path, FileMode.Create);

                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Deneme");
        }
    }
}



//string fileName = Path.GetFileNameWithoutExtension(data.PicturePath);
//string extName = Path.GetExtension(data.PicturePath);
//fileName = fileName.Replace(" ", "");
//fileName += Guid.NewGuid().ToString().Replace("-", "");
//fileName = MUsefullMethods.StringHelpers.CharacterConverter(fileName);
////string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Upload\"}";
//string path = Path.Combine(_hostingEnvironment.WebRootPath, "/Upload/");
////string path = _hostingEnvironment.WebRootFileProvider.GetFileInfo("Upload/"+ fileName + extName).PhysicalPath;

//var dosyayolu = path + fileName + extName;