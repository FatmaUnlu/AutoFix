using Microsoft.AspNetCore.Http;
using System;

namespace AutoFix.ViewModels
{
    public class ServiceProductViewModel
    {
        public Guid Id { get; set;}
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public decimal Price { get; set; }
        public IFormFile File { get; set; }
    }
}
