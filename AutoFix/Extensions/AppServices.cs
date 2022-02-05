using AutoFix.MapperProfiles;
using AutoFix.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoFix.Extensions
{
    public static class AppServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(options =>
            {
                //Mapper ile eşlenecek modellerin Profileri eklenmek zorundadır.
                options.AddProfile(typeof(AccountProfile));
            });
            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}
