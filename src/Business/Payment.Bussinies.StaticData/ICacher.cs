using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.StaticData
{
    public interface ICacher
    {
        public void Add(string key, object value);
        public object GetOrNull(string key);
        public object GetOrAdd(string key, Func<object> factory);
        public void Clear();
        public bool Remove(string key);
        public int GetCount();
    }
}
