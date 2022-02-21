using Iyzipay;

namespace AutoFix.Models.Payment
{
    public class IyzicoPaymentOptions :Options
    {
        public const string Key = "IyzicoOptions";
        public string ThreedsCallbackUrl { get; set; }
    }
}
