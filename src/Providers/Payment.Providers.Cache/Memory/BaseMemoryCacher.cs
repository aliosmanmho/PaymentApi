using Payment.Providers.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Memory
{
    public abstract class BaseMemoryCacher
    {
        protected readonly IDictionary<string, object> _cacheDictionary;
        protected readonly object _lock = new object();
        protected MemorySerializer _serializerEnum { get; private set; }
        protected BaseMemoryCacher()
        {
            _cacheDictionary = new Dictionary<string, object>();

            _serializerEnum = MemorySerializer.Json;
        }
        protected BaseMemoryCacher(MemorySerializer serializerEnum)
        {
            _cacheDictionary = new Dictionary<string, object>();
            if (serializerEnum == default(MemorySerializer))
                _serializerEnum = MemorySerializer.Json;
            else
                _serializerEnum = serializerEnum;
        }
    }
}
