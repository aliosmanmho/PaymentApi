using Microsoft.EntityFrameworkCore;
using Payment.Core.Entities;
using Payment.Core.Repositories;
using Payment.Data.Context;
using Payment.Data.Repositories.Base;

namespace Payment.Data.Repositories
{
    public class PaymentBinNumberRepository : CoreRepository<Payment.Core.Entities.BinNumber>, IPaymentBinNumberRepository
    {
        public PaymentBinNumberRepository(PaymentContext paymentContext) : base(paymentContext) { }

    }
}
