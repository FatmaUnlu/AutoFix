namespace AutoFix.Models.Payment
{
    public class CustomerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string LastLoginDate { get; set; }
        public string RegistrationAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Ip { get; set; }
    }
}
