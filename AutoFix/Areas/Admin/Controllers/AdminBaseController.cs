using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")] //rol tabanlı authotization
    public class AdminBaseController : Controller
    {
        
       
    }
}
