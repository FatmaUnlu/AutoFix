
using AutoFix.Models.Entities;
using AutoFix.ViewModels;
using AutoMapper;

namespace AutoFix.MapperProfiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<FailureLogging, FailureLoggingViewModel>().ReverseMap();
        }
    }
}
