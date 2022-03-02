using AutoFix.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoFix.Models.Entities
{
    public class ServiceProduct : BaseEntity<Guid>
    {
        [StringLength(40)]
        public string Name { get; set; }
        [StringLength(250)]
        public string PicturePath { get; set; }
        public decimal Price { get; set; }
        public string File { get; set; }
        public ICollection<ServiceDetail> ServiceDetails { get; set; }
    }
}
