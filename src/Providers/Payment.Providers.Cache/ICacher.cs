using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache
{
    public interface ICacher<T>
    {
        public void Add(string key, T value);
        public Task AddAsync(string key, T value);
        public T? GetOrNull(string key);
        public Task<T?> GetOrNullAsync(string key);
        public T GetOrAdd(string key, Func<T> factory);
        public Task<T> GetOrAddAsync(string key, Func<T> factory);

        public void Clear();
        public bool Remove(string key);
        public int GetCount();
    }
}
