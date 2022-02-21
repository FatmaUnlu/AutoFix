namespace AutoFix.Models.Payment
{
    public class PaymentResponseModel
    {
        public string Price { get; set; }
        public string PaidPrice { get; set; }
        public int? Installment { get; set; }
        public string Currency { get; set; }
        public string PaymentId { get; set; }
        public string Status { get; set; }
        public int? FraudStatus { get; set; }
        public string MerchantCommissionRate { get; set; }
        public string MerchantCommissionRateAmount { get; set; }
        public string PosCommissionRateAmount { get; set; }
        public string PosCommissionFee { get; set; }
        public string CardType { get; set; }
        public string CardAssociation { get; set; }
        public string CardFamily { get; set; }
        public string CardToken { get; set; }
        public string CardUserKey { get; set; }
        public string BinNumber { get; set; }
        public string LastFourDigits { get; set; }
        public string BasketId { get; set; }
    }
}
