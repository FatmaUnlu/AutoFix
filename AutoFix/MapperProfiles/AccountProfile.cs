using AutoFix.Models.Identity;
using AutoFix.ViewModels;
using AutoMapper;

namespace AutoFix.MapperProfiles
{
    public class AccountProfile:Profile
    {
        public AccountProfile()
        {
            //CreateMap<ApplicationUser, UserProfileViewModel>().ReverseMap();
            // CreateMap<UserProfileViewModel,ApplicationUser>()  // Yukarda ReverseMap() işlemi yapıldığı için 
           
        }
    }
}
