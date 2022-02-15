using AutoFix.Models;
using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Data
{
    public class MyContext : IdentityDbContext<ApplicationUser, AplicationRole, string>//varsayılan ıd tipi string guid
    {
        public MyContext(DbContextOptions<MyContext> options)
          : base(options)
        {

        }

        public DbSet<ServiceProduct> ServiceProducts { get; set; }
        public DbSet<FailureLogging> FailureLoggings { get; set; }
    }
}
