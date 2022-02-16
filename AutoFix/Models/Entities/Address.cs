using AutoFix.Models.Abstracts;
using AutoFix.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class Address: BaseEntity<Guid>
    {
        public string Line { get; set; }
        public string PostCode { get; set; }
        public AdressTypes AddressTypes { get; set; }
        public int StateId { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(StateId))]
        public virtual State State { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
    public enum AdressTypes
    {
        Fatura,
        Teslimat
    }
}
