using AutoFix.Models.Payment;

namespace AutoFix.Services
{
    public interface IPaymentService
    {
        public InstallmentModel CheckInstallment(string binNumber, decimal price);

        public PaymentResponseModel Pay(PaymentModel model);
    }
}
