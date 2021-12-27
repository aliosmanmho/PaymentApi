using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Serilizer
{
    public interface ISerilizer
    {
        public object Serilize<T>(T value) where T : class;
        public Task<object> SerilizeAsync<T>(T value) where T : class ;
        public T DeSerilize<T>(object value);
        public Task<T> DeSerilizeAsycn<T>(object value);
    }
}
