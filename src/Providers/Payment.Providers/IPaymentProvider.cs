using Payment.Providers.Model.Requests;
using Payment.Providers.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers
{
    public interface IPaymentProvider
    {
        public Task<PaymentPayProviderResponse> PayAsync(PaymentPayProviderRequest paymentPayClientRequest);
    }
}
