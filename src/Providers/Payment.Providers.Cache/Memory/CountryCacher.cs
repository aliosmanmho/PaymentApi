using Payment.Providers.Cache.Models.Base;
using Providers.Payments.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Memory
{
    public class CountryCacher<T> : BaseMemoryCacher, ICacher<T> where T : BaseCacherModel
    {
        private static readonly object _lockInstance = new object();
        private CountryCacher(SerializerEnum serializerEnum) : base(serializerEnum) { }
        private CountryCacher() { }
        private static CountryCacher<T>? Instance;
        public static CountryCacher<T> Get()
        {
            lock (_lockInstance)
            {
                if (Instance == null)
                    throw new Exception($"Not Initilize! Get Bore Call {nameof(Instance)}");
                return Instance;
            }
        }
        /// <summary>
        /// Initilize App Start This Method
        /// </summary>
        /// <param name="serializerEnum"></param>
        /// <returns></returns>
        public static CountryCacher<T> Initilize(SerializerEnum serializerEnum,bool initialForce = false)
        {
            lock (_lockInstance)
            {
                if (Instance == null || initialForce)
                {
                    Instance = new CountryCacher<T>(serializerEnum);
                }
                return Instance;
            }

        }
        public void Add(string key, T value)
        {
            var result = SerializerFactory.Instance.GetSerilizer(_serializerEnum).Serilize<T>(value);
            lock (base._lock)
            {
                _cacheDictionary[key] = result;
            }
        }

        public async Task AddAsync(string key, T value)
        {
            var result = await SerializerFactory.Instance.GetSerilizer(_serializerEnum).SerilizeAsync<T>(value);

            lock (base._lock)
            {
                _cacheDictionary[key] = result;
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
            return SerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilize<T>(value);
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
            return await SerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilizeAsycn<T>(value);
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
            return SerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilize<T>(value);
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
            return await SerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilizeAsycn<T>(value);
        }

        public void Clear()
        {
            lock (_lock)
            {
                _cacheDictionary.Clear();
            }
        }

        public bool Remove(string key)
        {
            lock (_lock)
            {
                return _cacheDictionary.Remove(key);
            }
        }

        public int GetCount()
        {
            lock (_lock)
            {
                return _cacheDictionary.Count();
            }
        }
    }
}
