using AutoFix.Models.Abstracts;
using AutoFix.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class CartItem : BaseEntity<Guid>
    {
        public Guid FailureId { get; set; }

        [ForeignKey(nameof(FailureId))]
        public virtual FailureLogging Failure { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Guid ServiceProductId { get; set; }
        [ForeignKey(nameof(ServiceProductId))]
        public virtual ServiceProduct ServiceProduct { get; set; }
        public string OrderStatus { get; set; }
    }
    public enum OrderStatus
    {
        Eklendi,
        İptal_Edildi,
        Ödendi

    }
}
