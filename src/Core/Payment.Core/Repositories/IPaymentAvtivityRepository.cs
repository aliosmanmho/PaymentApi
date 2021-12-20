using Payment.Core.Entities;
using Payment.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Core.Repositories
{
    public interface IPaymentAvtivityRepository : IRepository<PaymentActivity>
    {
        Task<IEnumerable<PaymentActivity>> GetLesThenDate(DateTime dateTime);
    }
}
