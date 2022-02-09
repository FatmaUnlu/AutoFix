using System;

namespace AutoFix.ViewModels
{
    public class JsonResponseViewModel 
    {
        public bool IsSuccess { get; set; } = true; //sunucunun response verip vermemesini, response başarılı mı değil mi onu kontrol etmek için
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;

    }
}
