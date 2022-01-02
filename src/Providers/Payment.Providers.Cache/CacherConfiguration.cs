using Payment.Bussinies.StaticData.FileRead;
using Payment.Providers.Cache.Memory;
using Payment.Providers.Cache.Models;
using Payment.Providers.Cache.Remote;
using Payment.Providers.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache
{
    public class CacherConfiguration
    {
        public static void Initilize(string serilizer)
        {
            BinNumberCacher<BinNumberCacherModel>.Initilize(serializerEnum: (MemorySerializer)Enum.Parse(typeof(MemorySerializer), serilizer));
            CountryCacher<CountryCacherModel>.Initilize(serializerEnum: (MemorySerializer)Enum.Parse(typeof(MemorySerializer), serilizer));
            //BinNumberRemoteCacher<BinNumberCacherModel>.Initilize(MemorySerializer.Json, true); //TODO:Remote Cacher Fix
        }
    }
}
