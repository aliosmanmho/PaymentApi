using Payment.Bussinies.StaticData.FileRead;
using Payment.Providers.Cache.Memory;
using Payment.Providers.Cache.Models;
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
        public static async void Initilize(string serilizer)
        {
            BinNumberCacher<BinNumberCacherModel>.Initilize(serializerEnum: (SerializerEnum)Enum.Parse(typeof(SerializerEnum), serilizer));
            CountryCacher<CountryCacherModel>.Initilize(serializerEnum: (SerializerEnum)Enum.Parse(typeof(SerializerEnum), serilizer));
        }
        //public static void IntilizeDatabaseValues(PaymentContext appContext)
        //{
        //    if(appContext!=null)
        //    {
        //        if (appContext.BinNumbers.Any())
        //        {
        //            var data = appContext.BinNumbers.ToList();
        //            var taskList = data.Select(x =>
        //            {
        //                return BinNumberCacher<BinNumberCacherModel>.Get().AddAsync(x.BinCode.ToString(), BinNumberCacherModel.ToCacheModel(x));
        //                //return true;
        //            });
        //            Task.WhenAll(taskList);
        //        }
        //    }
        //}
    }
}
