using AutoFix.Models.Entities;
using AutoFix.ViewModels;
using AutoMapper;

namespace AutoFix.MapperProfiles
{
    public class TechnicianProfile:Profile
    {
        public TechnicianProfile()
        {
            CreateMap<ServiceProduct, ServiceProductViewModel>().ReverseMap();
            CreateMap<CartItem, CartItemViewModel>().ReverseMap();
        }
    }
}
