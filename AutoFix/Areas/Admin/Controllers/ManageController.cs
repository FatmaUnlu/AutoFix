using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace AutoFix.Areas.Admin.Controllers
{
    public class ManageController : AdminBaseController
    {
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

        public IActionResult Role()
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