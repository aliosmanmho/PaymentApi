using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Serilizer
{
    public interface ISerilizer
    {
        public TResult Serilize<TValue,TResult>(TValue value);
        public Task<TResult> SerilizeAsync<TValue, TResult>(TValue value);
        public TValue DeSerilize<TValue, TResult>(TResult value);
        public Task<TValue> DeSerilizeAsycn<TValue, TResult>(TResult value);
    }
}
