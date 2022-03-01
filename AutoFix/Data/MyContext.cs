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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ServiceProduct>()
                .Property(x => x.Price)
                .HasPrecision(7, 2);

            builder.Entity<ServiceDetail>()
                .Property(x => x.Price)
                .HasPrecision(7, 2);

            builder.Entity<ServiceDetail>()
            .HasKey(x => new { x.ProductId, x.FailureId });
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }
        public DbSet<FailureLogging> FailureLoggings { get; set; }
        public DbSet<CartItem> ShoppingCarts { get; set; }
        public DbSet<ServiceDetail> ServiceDetails { get; set; }

    }
}
