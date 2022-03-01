using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class ServiceDetail
    {
        public Guid FailureId { get; set; }
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey(nameof(FailureId))]
        public FailureLogging Faulire { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ServiceProduct Product { get; set; }
    }
}