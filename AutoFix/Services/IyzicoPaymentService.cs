
using AutoFix.Models.Identity;
using AutoFix.Models.Payment;
using AutoMapper;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MUsefullMethods;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AutoFix.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IyzicoPaymentOptions _options;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public IyzicoPaymentService(IConfiguration configuration, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            var section = configuration.GetSection(IyzicoPaymentOptions.Key);

            _options = new IyzicoPaymentOptions()
            {
                ApiKey = section["ApiKey"],
                SecretKey = section["SecretKey"],
                BaseUrl = section["BaseUrl"],
                ThreedsCallbackUrl = section["ThreedsCallbackUrl"],
            };
        }

        private CreatePaymentRequest InitialPaymentRequest(PaymentModel model)
        {
            var paymentRequest = new CreatePaymentRequest
            {

                Installment = model.Installment,
                Locale = Locale.TR.ToString(),
                ConversationId = GenerateConversationId(),
                Price = model.Price.ToString(new CultureInfo("en-US")),
                PaidPrice = model.PaidPrice.ToString(new CultureInfo("en-US")),
                Currency = Currency.TRY.ToString(),
                BasketId = StringHelpers.GenerateUniqueCode(),
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.SUBSCRIPTION.ToString(),
                PaymentCard = _mapper.Map<PaymentCard>(model.CardModel),
                Buyer = _mapper.Map<Buyer>(model.Customer),
                BillingAddress = _mapper.Map<Address>(model.AddressModel)
            };

            var basketItems = new List<BasketItem>();

            foreach (var basketModel in model.BasketModel)
            {
                basketModel.ItemType =BasketItemType.VIRTUAL.ToString();
                basketModel.Category1 = "Hizmet";
                basketItems.Add(_mapper.Map<BasketItem>(basketModel));
            }

            paymentRequest.BasketItems = basketItems;

            return paymentRequest;
        }

        private string GenerateConversationId()
        {
            return StringHelpers.GenerateUniqueCode();
        }

        public InstallmentModel CheckInstallment(string binNumber, decimal price)
        {
            var conversationId = GenerateConversationId();
            var request = new RetrieveInstallmentInfoRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = conversationId,
                BinNumber = binNumber.Substring(0, 6),
                Price = price.ToString(new CultureInfo("en-US"))
            };
            var result = InstallmentInfo.Retrieve(request, _options);
            if (result.Status == "failure")
            {
                throw new Exception(result.ErrorMessage);
            }
            if (result.ConversationId != conversationId)
            {
                throw new Exception("Hatalı istek oluturuldu");

            }
            var resultModel = _mapper.Map<InstallmentModel>(result.InstallmentDetails[0]);
            Console.WriteLine();
            return resultModel;
        }

        public PaymentResponseModel Pay(PaymentModel model)
        {
            var request = this.InitialPaymentRequest(model);
            var payment = Payment.Create(request, _options);

            return _mapper.Map<PaymentResponseModel>(payment);
        }
    }
}
