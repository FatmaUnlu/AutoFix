using AutoFix.Models.Abstracts;
using AutoFix.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;

namespace AutoFix.Models
{
    public class ServiceProduct:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public decimal Price { get; set; }

        public string File { get; set; }



    }
}
