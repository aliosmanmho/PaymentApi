using Microsoft.EntityFrameworkCore;
using Payment.Core.Entities;
using Payment.Core.Repositories;
using Payment.Data.Context;
using Payment.Data.Repositories.Base;

namespace Payment.Data.Repositories
{
    public class PaymentActivityRepository : CoreRepository<Payment.Core.Entities.PaymentActivity>, IPaymentAvtivityRepository
    {
        public PaymentActivityRepository(PaymentContext paymentContext) : base(paymentContext) { }

        public async Task<IEnumerable<PaymentActivity>> GetLesThenDate(DateTime dateTime)
        {
            return await _paymentContext.paymentActivities.Where(x => x.CreateDate >= dateTime).ToListAsync();

        }
    }
}
