using Payment.Providers.Cache.Models;
using Payment.Providers.Cache.Models.Base;
using Payment.Providers.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Memory
{
    public class CountryCacher<T> : MemoryCacher<T> where T : CountryCacherModel
    {
        private static readonly object _lockInstance = new object();
        private CountryCacher(MemorySerializer serializerEnum) : base(serializerEnum) { }
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
        public static CountryCacher<T> Initilize(MemorySerializer serializerEnum,bool initialForce = false)
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
     
    }
}
