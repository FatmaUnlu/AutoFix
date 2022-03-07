using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        [StringLength(40)]
        public string Name { get; set; }
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
    }
}
