using AutoFix.Models.Abstracts;
using AutoFix.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class CartItem : BaseEntity<Guid>
    {
        public string FailureId { get; set; }

        [ForeignKey(nameof(FailureId))]
        public virtual FailureLogging Failure { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int ServiceProductId { get; set; }
        [ForeignKey(nameof(ServiceProductId))]

        public virtual ServiceProduct ServiceProduct { get; set; }
    }
}
