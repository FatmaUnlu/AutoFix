using Microsoft.AspNetCore.Identity;

namespace AutoFix.Models.Identity
{
    public class AplicationRole: IdentityRole
    {
        public AplicationRole()
        {

        }
        public AplicationRole(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
        public string Description { get; set; }
        

        
    }
}
