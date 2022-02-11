using AutoFix.Models.Entities;

namespace AutoFix.Models
{
    public class ServiceProduct:BaseEntity
    {
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public decimal Price { get; set; }
    }
}
