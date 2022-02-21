using AutoFix.Models.Payment;

namespace AutoFix.ViewModels
{
    public class PaymentViewModel
    {
        public CardModel CardModel { get; set; }
        public AddressModel AddressModel { get; set; }
        public BasketModel BasketModel { get; set; }
        public decimal Paid { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public int Installment { get; set; }
    }
}
