using AutoFix.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Data
{
    public class MyContext : IdentityDbContext<ApplicationUser, AplicationRole, string>//varsayılan ıd tipi string guid
    {
        public MyContext(DbContextOptions options) : base(options)
        {

        }
    }
}
