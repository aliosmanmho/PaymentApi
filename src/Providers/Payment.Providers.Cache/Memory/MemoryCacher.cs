using Payment.Providers.Cache.Models.Base;
using Payment.Providers.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Memory
{
    public abstract class MemoryCacher<T> : ICacher<T> where T : BaseCacherModel
    {
        protected readonly IDictionary<string, object> _cacheDictionary;
        protected readonly object _lock = new object();
        protected MemorySerializer _serializerEnum { get; private set; }
        protected MemoryCacher()
        {
            _cacheDictionary = new Dictionary<string, object>();

            _serializerEnum = MemorySerializer.Json;
        }
        protected MemoryCacher(MemorySerializer serializerEnum)
        {
            _cacheDictionary = new Dictionary<string, object>();
            if (serializerEnum == default(MemorySerializer))
                _serializerEnum = MemorySerializer.Json;
            else
                _serializerEnum = serializerEnum;
        }
        public void Add(string key, T value)
        {

            var result = MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).Serilize<T,string>(value);
            lock (_lock)
            {
                _cacheDictionary[key] = result;
            }
        }

        public async Task AddAsync(string key, T value)
        {
            var result = await MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).SerilizeAsync<T,string>(value);

            lock (_lock)
            {
                _cacheDictionary[key] = result;
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _cacheDictionary.Clear();
            }
        }

        public int GetCount()
        {
            lock (_lock)
            {
                return _cacheDictionary.Count();
            }
        }

        public T GetOrAdd(string key, Func<T> factory)
        {
            object value;
            lock (_lock)
            {
                _cacheDictionary.TryGetValue(key, out value);
                if (value == null)
                {
                    value = factory();
                    _cacheDictionary[key] = value;
                }
            }
            return MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilize<T,object>(value);
        }

        public async Task<T> GetOrAddAsync(string key, Func<T> factory)
        {
            object value;
            lock (_lock)
            {
                _cacheDictionary.TryGetValue(key, out value);
                if (value == null)
                {
                    value = factory();
                    _cacheDictionary[key] = value;
                }
            }
            return await MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilizeAsycn<T,object>(value);
        }

        public T? GetOrNull(string key)
        {
            object value;
            lock (_lock)
            {
                _cacheDictionary.TryGetValue(key, out value);
                if (value == null)
                {
                    return default(T);
                }
            }
            return MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilize<T,object>(value);
        }

        public async Task<T?> GetOrNullAsync(string key)
        {
            object value;
            lock (_lock)
            {
                _cacheDictionary.TryGetValue(key, out value);
                if (value == null)
                {
                    return default(T);
                }
            }
            return await MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilizeAsycn<T,object>(value);
        }

        public bool Remove(string key)
        {
            lock (_lock)
            {
                return _cacheDictionary.Remove(key);
            }
        }

    }
}
