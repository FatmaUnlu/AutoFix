using AutoFix.Areas.Admin.ViewModels;
using AutoFix.Data;
using AutoFix.Extensions;
using AutoFix.Models.Identity;
using AutoFix.ViewModels;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFix.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]

    [Authorize(Roles = "Admin")]
    public class UserApiController : ControllerBase
    {
        private readonly MyContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<AplicationRole> _roleManager;
        public UserApiController(UserManager<ApplicationUser> userManager, MyContext dbContext, RoleManager<AplicationRole> roleManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetUsers(DataSourceLoadOptions loadOptions)
        {
            var data = _userManager.Users;
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUsers(string key, string values)
        {
            //Kullanıcı 
            var data = _userManager.Users.FirstOrDefault(x => x.Id == key);

            if (data == null)
                return StatusCode(StatusCodes.Status409Conflict, new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı Bulunamadı"
                });

            var userRoleUpdateModel = new UserRoleUpdateViewModel();
            
            var useroldrole = _dbContext.UserRoles.Where(x => x.UserId == data.Id).Select(x=>x.RoleId).Single();
            

            string oldRoleName =  _dbContext.Roles.SingleOrDefault(r => r.Id == useroldrole).Name;
            
            JsonConvert.PopulateObject(values, userRoleUpdateModel);
            string newroleName = _dbContext.Roles.SingleOrDefault(r => r.Id == userRoleUpdateModel.RoleId).Name;

            if (!string.IsNullOrEmpty(userRoleUpdateModel.RoleId))
            {
                 await _userManager.RemoveFromRoleAsync(data,oldRoleName);
                 await _userManager.AddToRoleAsync(data, newroleName);
                
            }

            JsonConvert.PopulateObject(values, data); //değişiklik varsa değişiklik olanları günceller
            if (!TryValidateModel(data))
                return BadRequest(ModelState.ToFullErrorString());

            var result = await _userManager.UpdateAsync(data);

            if (!result.Succeeded)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı Güncellenemedi"
                });
            return Ok(new JsonResponseViewModel());
        }
        [HttpGet]
        public async Task<object> RolesLookUp(string userId, DataSourceLoadOptions loadOptions)
        {
            string role = string.Empty;  
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                role = _userManager.GetRolesAsync(user).Result.First();
            }

            var data = _dbContext.Roles
               .OrderBy(x => x.Id)
               .Select(x => new
               {
                   //id = x.Id,
                   Value = x.Id,
                   Text = $"{x.Name}",
                   Selected = x.Name == role ? true : false
               });

            if (loadOptions.Filter != null)
            {
                List<string> data1 = new List<string>();
                var resultJson1 = JsonConvert.SerializeObject(loadOptions.Filter);
                for (int i = 0; i < loadOptions.Filter.Count; i = i + 2)
                {
                    var filter = JsonConvert.DeserializeObject(loadOptions.Filter[i].ToString()) as IList;
                    data1.Add(filter[2].ToString());
                    //data1.Add(loadOptions.Filter[i].ToString());
                    //var resultJson = JsonConvert.SerializeObject(loadOptions.Filter[i]);


                }
            }



            var userRoles = _dbContext.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();

            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

     

            //[HttpGet]
            //public IActionResult GetTest()
            //{
            //    var users = new List<UserProfileViewModel>();
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        users.Add(new UserProfileViewModel
            //        {
            //            Email = "Deneme" + i,
            //            Surname = "Soyad" + i,
            //            Name = "ad" + i
            //        });
            //    }

            //    return Ok(new JsonResponseViewModel()
            //    {
            //        Data = users
            //    });
            //}
        }
}
