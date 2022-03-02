using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoFix.Models.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [StringLength(40)]
        public string Name { get; set; }

        public int Order { get; set; }

        public virtual List<State> States { get; set; }
    }
}
