using AutoFix.Models.Entities;
using AutoFix.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.ViewModels
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }
        public Guid FailureId { get; set; }

        [ForeignKey(nameof(FailureId))]
        public virtual FailureLogging Failure { get; set; }
        public virtual CartItem CartItem { get; set; }
        public string CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual ApplicationUser CustomerUser { get; set; }
        public Guid ServiceProductId { get; set; }
        [ForeignKey(nameof(ServiceProductId))]
        public virtual ServiceProduct ServiceProduct { get; set; }
        public string OrderStatus { get; set; }


    }
}
