using Payment.Providers.Cache.Models;
using Payment.Providers.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Memory
{
    public class BinNumberCacher<T> : MemoryCacher<T> where T : BinNumberCacherModel
    {
        private static readonly object _lockInstance = new object();
        private BinNumberCacher(MemorySerializer serializerEnum) : base(serializerEnum) { }
        private BinNumberCacher() { }
        private static BinNumberCacher<T>? Instance;
        public static BinNumberCacher<T> Get()
        {
            lock (_lockInstance)
            {
                if (Instance == null)
                    throw new Exception($"Not Initilize! Get Before Call {nameof(Instance)}");
                return Instance;
            }
        }
        /// <summary>
        /// Initilize App Start This Method
        /// </summary>
        /// <param name="serializerEnum"></param>
        /// <returns></returns>
        public static BinNumberCacher<T> Initilize(MemorySerializer serializerEnum, bool initiliaForce = false)
        {
            lock (_lockInstance)
            {
                if (Instance == null || initiliaForce)
                {
                    Instance = new BinNumberCacher<T>(serializerEnum);
                }
                return Instance;
            }

        }

    }
}
