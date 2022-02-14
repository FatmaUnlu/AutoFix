using AutoFix.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class FailureLogging:BaseEntity
    {
        public string FailureName { get; set; }
        public string FailureDescription{ get; set; }
        public string FailureStatus { get; set; }
        public float Latitude { get; set; }//Enlem
        public float Longitude { get; set; }//Boylam
        public string AddressDetail { get; set; }
        public Guid UserId { get; set; }
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
