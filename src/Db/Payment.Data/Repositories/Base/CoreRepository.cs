using Microsoft.EntityFrameworkCore;
using Payment.Core.Repositories.Base;
using Payment.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Data.Repositories.Base
{
    public class CoreRepository<T> : IRepository<T> where T : class
    {
        protected readonly PaymentContext _paymentContext;
        public CoreRepository(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _paymentContext.Set<T>().AddAsync(entity);
            await _paymentContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _paymentContext.Set<T>().Remove(entity);
            await _paymentContext.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _paymentContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _paymentContext.Set<T>().FindAsync(id);
        }
        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
