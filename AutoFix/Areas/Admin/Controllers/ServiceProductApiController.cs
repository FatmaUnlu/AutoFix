using AutoFix.Data;
using AutoFix.Extensions;
using AutoFix.Models.Entities;
using AutoFix.ViewModels;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace AutoFix.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServiceProductApiController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ServiceProductApiController(MyContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Crud

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions options)
        {
            var data = _dbContext.ServiceProducts;

            return Ok(DataSourceLoader.Load(data, options));
        }
        [HttpGet]
        public IActionResult Detail(Guid id, DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.ServiceProducts
                .Where(x => x.Id == id);

            return Ok(DataSourceLoader.Load(data, loadOptions));
        }
        [HttpPost]
        public IActionResult Insert(string values,string picture)
        {
            var data = new ServiceProduct();
            JsonConvert.PopulateObject(values, data);

            if (!TryValidateModel(data))
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            
            if (data.PicturePath != null && data.PicturePath.Length > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(data.PicturePath);
                string extName = Path.GetExtension(data.PicturePath);
                fileName = fileName.Replace(" ", "");
                fileName += Guid.NewGuid().ToString().Replace("-", "");
                fileName = MUsefullMethods.StringHelpers.CharacterConverter(fileName);
                //string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Upload\"}";
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "/Upload/");
                //string path = _hostingEnvironment.WebRootFileProvider.GetFileInfo("Upload/"+ fileName + extName).PhysicalPath;

                var dosyayolu =path+ fileName+extName;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //data.PicturePath.SaveAs(dosyayolu);
                data.PicturePath = dosyayolu;
               // WebImage img = new WebImage("C:/Users/BERNA/Desktop/Special/resim1.jpg");
                //img.Resize(142, 142, false);
                //img.AddTextWatermark("Wissen", "RoyalBlue", opacity: 75, fontSize: 25, fontFamily: "Verdana", horizontalAlign: "Left");
                //img.Save(path);
                //new FotografRepo().Insert(new Fotograf()
                //{
                //    KonutID = yeniKonut.ID,
                //    Yol = @"Upload/" + yeniKonut.ID + "/" + fileName + extName
                //});
            }

            _dbContext.ServiceProducts.Add(data);

            var result = _dbContext.SaveChanges();
            if (result == 0)
                return BadRequest(new JsonResponseViewModel
                {
                    IsSuccess = false,
                    ErrorMessage = "Yeni üyelik tipi kaydedilemedi."
                });
            return Ok(new JsonResponseViewModel());
        }
        [HttpPut]
        public IActionResult Update(Guid key, string values)
        {
            var data = _dbContext.ServiceProducts.Find(key);
            if (data == null)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });

            JsonConvert.PopulateObject(values, data);
            if (!TryValidateModel(data))
                return BadRequest(ModelState.ToFullErrorString());

            var result = _dbContext.SaveChanges();
            if (result == 0)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Üyelik tipi güncellenemedi"
                });
            return Ok(new JsonResponseViewModel());
        }
        //[HttpDelete]
        //public IActionResult Delete(Guid key)
        //{
        //    var data = _dbContext.ServiceProducts.Find(key);
        //    if (data == null)
        //        return StatusCode(StatusCodes.Status409Conflict, "Üyelik tipi bulunamadı");

        //    _dbContext.ServiceProducts.Remove(data);

        //    var result = _dbContext.SaveChanges();
        //    if (result == 0)
        //        return BadRequest("Silme işlemi başarısız");
        //    return Ok(new JsonResponseViewModel());
        //}
        #endregion
    }
}
