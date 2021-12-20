using Payment.Providers.Model.Base;
using Payment.Providers.Model.Requests;
using Payment.Providers.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.BankProviders.ZiraatBank
{
    public class ZiraatBankPaymentProvider : BasePaymentProvider, IPaymentProvider
    {
        public Task<PaymentPayProviderResponse> PayAsync(PaymentPayProviderRequest paymentPayClientRequest)
        {
            return Task.FromResult(new PaymentPayProviderResponse() { Amount = paymentPayClientRequest.Amount});
        }
    }
}
