
using AutoFix.Models.Entities;
using AutoFix.Models.Payment;
using AutoFix.ViewModels;
using AutoMapper;

namespace AutoFix.MapperProfiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<FailureLogging, FailureLoggingViewModel>().ReverseMap();
            CreateMap<BasketModel, ServiceProduct>().ReverseMap();
        }
    }
}
