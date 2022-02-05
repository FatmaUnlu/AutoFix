using System.ComponentModel.DataAnnotations;

namespace AutoFix.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı gereklidir.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [Display(Name = "Ad")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        [Display(Name = "Soyad")]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "E-posta alanı gereklidir.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        //[StringLength(100),MinLength(6,ErrorMessage ="Şifreniz minimum 6 karekterli olmalıdır.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifreniz minimum 6 karekterli olmalıdır.")]

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre tekrar alanı gereklidir.")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
