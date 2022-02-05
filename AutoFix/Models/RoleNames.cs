using System.Collections.Generic;

namespace AutoFix.Models
{
    public class RoleNames
    {
        public static string Admin = "Admin";
        public static string Operator = "Operatör";
        public static string Teknisyen = "Teknisyen";
        public static string Musteri = "Müşteri";
        public static string Passive = "Passive";


        public static List<string> Roles => new List<string> { Admin, Musteri, Passive,Operator,Teknisyen };
    }
}
