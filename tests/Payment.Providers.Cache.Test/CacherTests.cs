using Payment.Core.Entities;
using Payment.Providers.Cache.Memory;
using Payment.Providers.Cache.Models;
using Payment.Providers.Serilizer;
using System.Threading;
using Xunit;

namespace Payment.Providers.Cache.Test
{
    public class CacherTests
    {
        private readonly SemaphoreSlim m_lock = new SemaphoreSlim(1);


        [Fact]
        public async void BinNumberCacheJsonTest()
        {
            this.m_lock.Wait();
            try
            {
                var binNumber = new BinNumber() { BankCode = 10, BankName = "Abc", BinCode = 123454 };
                BinNumberCacher<BinNumberCacherModel>.Initilize(SerializerEnum.Json, true);
                BinNumberCacher<BinNumberCacherModel>.Get().Clear();
                await BinNumberCacher<BinNumberCacherModel>.Get().AddAsync(binNumber.BinCode.ToString(), BinNumberCacherModel.ToCacheModel(binNumber));
                var data = await BinNumberCacher<BinNumberCacherModel>.Get().GetOrNullAsync(binNumber.BinCode.ToString());
                Assert.NotNull(data);
                Assert.Equal(binNumber.BinCode, data?.BinCode);
            }
            finally
            {
                m_lock.Release();
            }
        }

        [Fact]
        public async void BinNumberCacheMessagePackTest()
        {
            this.m_lock.Wait();
            try
            {
                var binNumber = new BinNumber() { BankCode = 10, BankName = "Abc", BinCode = 123454 };
                BinNumberCacher<BinNumberCacherModel>.Initilize(SerializerEnum.MesagePackage, true);
                BinNumberCacher<BinNumberCacherModel>.Get().Clear();
                await BinNumberCacher<BinNumberCacherModel>.Get().AddAsync(binNumber.BinCode.ToString(), BinNumberCacherModel.ToCacheModel(binNumber));
                var binNumberCache = await BinNumberCacher<BinNumberCacherModel>.Get().GetOrNullAsync(binNumber.BinCode.ToString());
                Assert.NotNull(binNumberCache);
                Assert.Equal(binNumber.BinCode, binNumberCache?.BinCode);
            }
            finally
            {
                m_lock.Release();
            }
        }
        [Fact]
        public async void CountryCacherJsonTest()
        {
            this.m_lock.Wait();
            try
            {
                var country = new CountryCacherModel() { Code = "09", Name = "Turkey" };
                CountryCacher<CountryCacherModel>.Initilize(SerializerEnum.Json, true);
                CountryCacher<CountryCacherModel>.Get().Clear();
                await CountryCacher<CountryCacherModel>.Get().AddAsync(country.Code, country);
                var countryCacher = await CountryCacher<CountryCacherModel>.Get().GetOrNullAsync(country.Code);
                Assert.NotNull(countryCacher);
                Assert.Equal(country.Code, countryCacher?.Code);
            }
            finally
            {
                m_lock.Release();
            }
        }
        [Fact]
        public async void CountryCacherMesagePackTest()
        {
            this.m_lock.Wait();
            try
            {
                var country = new CountryCacherModel() { Code = "09", Name = "Turkey" };
                CountryCacher<CountryCacherModel>.Initilize(SerializerEnum.MesagePackage, true);
                CountryCacher<CountryCacherModel>.Get().Clear();
                await CountryCacher<CountryCacherModel>.Get().AddAsync(country.Code, country);
                var countryCacher = await CountryCacher<CountryCacherModel>.Get().GetOrNullAsync(country.Code);
                Assert.NotNull(countryCacher);
                Assert.Equal(country.Code, countryCacher?.Code);
            }
            finally
            {
                m_lock.Release();
            }
        }
    }
}