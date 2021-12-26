using Providers.Payments.Serilizer;
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
        protected SerializerEnum _serializerEnum { get; private set; }
        protected BaseMemoryCacher()
        {
            _cacheDictionary = new Dictionary<string, object>();

            _serializerEnum = SerializerEnum.JsonSerilize;
        }
        protected BaseMemoryCacher(SerializerEnum serializerEnum)
        {
            _cacheDictionary = new Dictionary<string, object>();
            if (serializerEnum == default(SerializerEnum))
                _serializerEnum = SerializerEnum.JsonSerilize;
            else
                _serializerEnum = serializerEnum;
        }
    }
}
