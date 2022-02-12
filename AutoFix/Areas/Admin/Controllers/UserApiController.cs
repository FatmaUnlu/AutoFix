using AutoFix.Data;
using AutoFix.Extensions;
using AutoFix.Models.Identity;
using AutoFix.ViewModels;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
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
        public UserApiController(UserManager<ApplicationUser> userManager, MyContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
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
            var data = _userManager.Users.FirstOrDefault(x => x.Id == key);

            if (data == null)
                return StatusCode(StatusCodes.Status409Conflict, new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı Bulunamadı"
                });

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
        public object RolesLookUp(string userId, DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.Roles
               .OrderBy(x => x.Id)
               .Select(x => new
               {
                   //id = x.Id,
                   Value = x.Id,
                   Text = $"{x.Name}"
               });

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
