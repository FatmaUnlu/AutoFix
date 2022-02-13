using System.ComponentModel.DataAnnotations;

namespace AutoFix.ViewModels
{
    public class FailureLoggingViewModel
    {
        [Required(ErrorMessage = "Arıza alanı boş geçilemez.")]
        [Display(Name = "Arıza")]
        [StringLength(70)]
        public string FailureName { get; set; }
        [Required(ErrorMessage = "Arıza tanımı boş geçilemez.")]
        [Display(Name = "Arıza Tanım")]
        [StringLength(100)]
        public string FailureDescription { get; set; }
        public string FailureSatus { get; set; }
        public float Latitude { get; set; }//Enlem
        public float Longitude { get; set; }//Boylam
        [Required(ErrorMessage = "Arıza adres detay bilgileri boş geçilemez.")]
        [Display(Name = "Arıza Adres Detay")]
        [StringLength(70)]
        public string AddressDetail { get; set; }

    }
}
