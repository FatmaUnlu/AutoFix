using AutoFix.Models.Abstracts;
using AutoFix.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class FailureLogging : BaseEntity<Guid>
    {
        [StringLength(100)]
        public string FailureName { get; set; }
        [StringLength(350)]
        public string FailureDescription { get; set; }
        public FailureStatus FailureStatus { get; set; }
        [StringLength(40)]
        public string Latitude { get; set; }//Enlem
        [StringLength(40)]
        public string Longitude { get; set; }//Boylam
        [StringLength(100)]
        public string AddressDetail { get; set; }
        [StringLength(450)]
        public string TechnicianId { get; set; }

        [ForeignKey(nameof(TechnicianId))]
        public virtual ApplicationUser Technician { get; set; }
        [StringLength(450)]
        public string OperatorId { get; set; }

        [ForeignKey(nameof(OperatorId))]
        public virtual ApplicationUser Operator { get; set; }

        public ICollection<ServiceDetail> ServiceDetails { get; set; }
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
