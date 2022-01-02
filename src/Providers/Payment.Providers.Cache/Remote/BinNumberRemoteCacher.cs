using Payment.Providers.Cache.Models;
using Payment.Providers.Cache.Remote.Redis;
using Payment.Providers.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Remote
{
    public class BinNumberRemoteCacher<T> : RemoteCacher<T> where T : BinNumberCacherModel
    {
        private static readonly object _lockInstance = new object();
        private BinNumberRemoteCacher(MemorySerializer serializerEnum, IRemoteCacher cacher) : base(serializerEnum, cacher) { }
        private BinNumberRemoteCacher() { }
        private static BinNumberRemoteCacher<T>? Instance;
        public static BinNumberRemoteCacher<T> Get()
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
        public static BinNumberRemoteCacher<T> Initilize(MemorySerializer serializerEnum, bool initiliaForce = false, RedisConfig config = null)
        {
            lock (_lockInstance)
            {
                if (Instance == null || initiliaForce)
                {
                    RedisServer server = new RedisServer(config);
                    Instance = new BinNumberRemoteCacher<T>(serializerEnum, server);
                }
                return Instance;
            }

        }
    }
}
