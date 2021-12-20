using Payment.Bussinies.Models.Requests;
using Payment.Bussinies.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        #region Pay
        public Task<ServiceResponse<PaymentPayResponse>> PayAsycn(PaymentPayRequest paymentPayRequest);
        #endregion
        #region BinNumber
        public Task<ServiceResponse<BinNumberResponse>> GetByBinNoAsycn(BinNummberRequest binNummberRequest);
        #endregion
    }
}
