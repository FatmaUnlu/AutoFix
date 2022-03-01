using System;
using System.ComponentModel.DataAnnotations;

namespace AutoFix.Models.Abstracts
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(128)]
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [StringLength(128)]
        //public DateTime? DeletedDate { get; set; }

        public bool? IsDeleted { get; set; } = false;
        public string UpdatedUser { get; set; }
    }
}
