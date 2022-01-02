using Payment.Core.Entities;
using Payment.Providers.Cache.Models;
using Payment.Providers.Cache.Remote;
using Payment.Providers.Serilizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Payment.Providers.Cache.Test
{
    public class RemoteCacherTest
    {
        private readonly SemaphoreSlim m_lock = new SemaphoreSlim(1);

        [Fact]
        public async void BinNumberRemoteCacheJsonTest()
        {
            this.m_lock.Wait();
            try
            {
                var binNumber = new BinNumber() { BankCode = 10, BankName = "Abc", BinCode = 123454 };
            

                BinNumberRemoteCacher<BinNumberCacherModel>.Initilize(MemorySerializer.Json, true, new RedisConfig()
                {
                    //AppSettingForDevInitilize
                });
                BinNumberRemoteCacher<BinNumberCacherModel>.Get().Clear();
                await BinNumberRemoteCacher<BinNumberCacherModel>.Get().AddAsync(binNumber.BinCode.ToString(), BinNumberCacherModel.ToCacheModel(binNumber));
                var data = await BinNumberRemoteCacher<BinNumberCacherModel>.Get().GetOrNullAsync(binNumber.BinCode.ToString());
                Assert.NotNull(data);
                Assert.Equal(binNumber.BinCode, data?.BinCode);
            }
            finally
            {
                m_lock.Release();
            }
        }
    }
}
