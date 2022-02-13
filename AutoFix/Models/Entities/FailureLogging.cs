using AutoFix.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFix.Models.Entities
{
    public class FailureLogging:BaseEntity
    {
        public string FailureName { get; set; }
        public string FailureDescription{ get; set; }
        public string FailureSatus { get; set; }
        public float Latitude { get; set; }//Enlem
        public float Longitude { get; set; }//Boylam
        public string AddressDetail { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }


        






    }
}
