using System.ComponentModel.DataAnnotations;

namespace AutoFix.ViewModels
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı gereklidir.")]
        [Display(Name = "Kullnanıcı Adı")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [Display(Name = "Ad")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        [Display(Name = "Soyad")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-Posta alanı gereklidir.")]
        [EmailAddress]
        [Display(Name = "Email Adres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Numarası alanı gereklidir.")]
        [Phone]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }


    }
}
