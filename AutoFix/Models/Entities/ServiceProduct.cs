using AutoFix.Models.Abstracts;
using System;
using System.Collections.Generic;

namespace AutoFix.Models.Entities
{
    public class ServiceProduct : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public decimal Price { get; set; }
        public string File { get; set; }
        public ICollection<ServiceDetail> ServiceDetails { get; set; }
    }
}
