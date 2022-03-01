using AutoFix.Models.Entities;
using System.Collections.Generic;

namespace AutoFix.Models.Payment
{
    public class PaymentModel
    {
        public string PaymentId { get; set; }
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }
        public int Installment { get; set; }
        public CardModel CardModel { get; set; }
        public CustomerModel Customer { get; set; }
        public List<BasketModel> BasketModel { get; set; }
        //public CustomerModel CustomerModel { get; set; }
        public AddressModel AddressModel { get; set; }
        //public List<CartItem> CartItem { get; set; }
        public string Ip { get; set; }
        public string UserId { get; set; }
    }
}
