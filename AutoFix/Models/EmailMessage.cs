namespace AutoFix.Models
{
    public class EmailMessage
    {
        public string[] Contacts { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
