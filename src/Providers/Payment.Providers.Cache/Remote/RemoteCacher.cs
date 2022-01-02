using Payment.Providers.Cache.Models.Base;
using Payment.Providers.Cache.Remote.Redis;
using Payment.Providers.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Remote
{
    public class RemoteCacher<T> : ICacher<T> where T : BaseCacherModel
    {
        protected readonly IRemoteCacher _cache;
        protected readonly object _lock = new object();
        protected MemorySerializer _serializerEnum { get; private set; }
        protected RemoteCacher()
        {
            _cache = new RedisServer(null);

            _serializerEnum = MemorySerializer.Json;
        }
        protected RemoteCacher(MemorySerializer serializerEnum,IRemoteCacher cacher)
        {
            _cache = cacher;
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
                _cache.Add(key,result);
            }
        }

        public async Task AddAsync(string key, T value)
        {
            var result = await MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).SerilizeAsync<T,string>(value);

            lock (_lock)
            {
                _cache.Add(key, result as string);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _cache.Clear();
            }
        }

        public int GetCount()
        {
            lock (_lock)
            {
                return _cache.Count();
            }
        }

        public T GetOrAdd(string key, Func<T> factory)
        {
            string? value;
            lock (_lock)
            {
                value = _cache.Get(key);
                if (string.IsNullOrEmpty(value))
                {
                    value = factory() as string;

                    _cache.Add(key, value);
                }
            }
            return MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilize<T,string>(value);
        }

        public async Task<T> GetOrAddAsync(string key, Func<T> factory)
        {
            string? value;
            lock (_lock)
            {
                value = _cache.Get(key);
                if (value == null)
                {
                    value = factory() as string;
                    _cache.Add(key, value);
                }
            }
            return await MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilizeAsycn<T,string>(value);
        }

        public T? GetOrNull(string key)
        {
            string? value;
            lock (_lock)
            {
                value = _cache.Get(key);
                if (value == null)
                {
                    return default(T);
                }
            }
            return MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilize<T,string>(value);
        }

        public async Task<T?> GetOrNullAsync(string key)
        {
            string? value;
            lock (_lock)
            {
                value = _cache.Get(key);
                if (value == null)
                {
                    return default(T);
                }
            }
            return await MemorySerializerFactory.Instance.GetSerilizer(_serializerEnum).DeSerilizeAsycn<T,string>(value);
        }

        public bool Remove(string key)
        {
            lock (_lock)
            {
                return _cache.Remove(key);
            }
        }
    }
}
