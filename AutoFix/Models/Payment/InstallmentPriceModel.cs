namespace AutoFix.Models.Payment
{
    public class InstallmentPriceModel
    {
        public string Price { get; set; }
        public string TotalPrice { get; set; }
        public int? InstallmentNumber { get; set; }
    }
}
