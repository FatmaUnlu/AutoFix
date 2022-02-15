using AutoFix.Models.Abstracts;
using AutoFix.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class FailureLogging:BaseEntity<Guid>
    {
        public string FailureName { get; set; }
        public string FailureDescription{ get; set; }
        public string FailureStatus { get; set; }
        public string Latitude { get; set; }//Enlem
        public string Longitude { get; set; }//Boylam
        public string AddressDetail { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }


       

    }
    public enum FailureStatus
    {
        Alındı,
        Yönlendirildi,
        Beklemede,
        HizmetVeriliyor,
        Tamamlandi
    }
}
