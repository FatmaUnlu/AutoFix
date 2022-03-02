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
        public string CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual ApplicationUser Customer { get; set; }

        public Guid ServiceProductId { get; set; }
        [ForeignKey(nameof(ServiceProductId))]
        public virtual ServiceProduct Product { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
    public enum OrderStatus
    {
        Eklendi,
        Iptal_Edildi,
        Odeme_Bekliyor,
        Odendi
    }
}