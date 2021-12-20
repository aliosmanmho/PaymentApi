using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.StaticData
{
    public class BinNumberCacher : ICacher
    {
        public static BinNumberCacher Instance { get; protected set; } = new BinNumberCacher();
        private readonly IDictionary<string, object> _cacheDictionary;
        protected internal BinNumberCacher()
        {
            _cacheDictionary = new Dictionary<string, object>();
        }
       
        public void Add(string key, object value)
        {
            lock (_cacheDictionary)
            {
                _cacheDictionary[key] = value;
            }
        }

        public void Clear()
        {
            lock (_cacheDictionary)
            {
                _cacheDictionary.Clear();
            }
        }

        public int GetCount()
        {
            lock (_cacheDictionary)
            {
                return _cacheDictionary.Count;
            }
        }

        public object GetOrAdd(string key, Func<object> factory)
        {
            lock (_cacheDictionary)
            {
                var value = GetOrNull(key);
                if (value != null)
                {
                    return value;
                }

                value = factory();
                Add(key, value);

                return value;
            }
        }

        public object GetOrNull(string key)
        {
            lock (_cacheDictionary)
            {
                if (_cacheDictionary.TryGetValue(key, out object value))
                {
                    return value;
                }

                return null;
            }
        }

        public bool Remove(string key)
        {
            lock (_cacheDictionary)
            {
                return _cacheDictionary.Remove(key);
            }
        }
    }
}
