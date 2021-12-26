using Xunit;
using Payment.Bussinies.Repositories;
using Microsoft.EntityFrameworkCore;
using Payment.Data.Context;
using Payment.Core.Repositories;
using Payment.Data.Repositories;
using Microsoft.Extensions.Logging;
using Payment.Core.Entities;
using Payment.Bussinies.StaticData;
using System.IO;
using Payment.Providers.Cache.Memory;
using Providers.Payments.Serilizer;
using Payment.Providers.Cache.Models;

namespace Payment.Bussinies.Test
{
    public class PaymentBussinies
    {
        public static readonly string App = Directory.GetCurrentDirectory();
        public static readonly string Templates = Path.Combine(App, "Files");
        public PaymentBussinies()
        {
            BinNumberCacher<BinNumberCacherModel>.Initilize(SerializerEnum.JsonSerilize, true);
        }
        [Fact]
        public async void GetBinNoTest()
        {
            var options = new DbContextOptionsBuilder<PaymentContext>()
           .UseInMemoryDatabase("testDb")
           .Options;

            using (var c = new PaymentContext(options))
            {
                var binNumber = new BinNumber() { BankCode = 123, BankName = "Abc", BinCode = 123 };
                await BinNumberCacher<BinNumberCacherModel>.Get().AddAsync(binNumber.BinCode.ToString(), BinNumberCacherModel.ToCacheModel(binNumber));
                IPaymentAvtivityRepository _paymentAvtivityRepository = new PaymentActivityRepository(c);
                IPaymentBinNumberRepository _paymentBinNumberRepository = new PaymentBinNumberRepository(c);
                var rep = new PaymentRepository(_paymentAvtivityRepository, _paymentBinNumberRepository, null);

                var r = await rep.GetByBinNoAsycn(new Models.Requests.BinNummberRequest() { BinNummber = 123 });
                Assert.True(r.Succeeded);
                Assert.Equal(r.Data.BinCode, binNumber.BinCode);
            }
        }
        [Fact]
        public async void PayTest()
        {
            var options = new DbContextOptionsBuilder<PaymentContext>()
           .UseInMemoryDatabase("testDb")
           .Options;

            using (var c = new PaymentContext(options))
            {
                var binNumber = new BinNumber() { BankCode = 10, BankName = "Abc", BinCode = 123454 };
                await BinNumberCacher<BinNumberCacherModel>.Get().AddAsync(binNumber.BinCode.ToString(), BinNumberCacherModel.ToCacheModel(binNumber));
                IPaymentAvtivityRepository _paymentAvtivityRepository = new PaymentActivityRepository(c);
                IPaymentBinNumberRepository _paymentBinNumberRepository = new PaymentBinNumberRepository(c);
                var rep = new PaymentRepository(_paymentAvtivityRepository, _paymentBinNumberRepository, null);
                var payReq = new Models.Requests.PaymentPayRequest() { Amount = 100, CardNo = "1234548845487", Ccv = 121, Month = 1, Year = 1923, OwnerName = "deneme" };
                var r = await rep.PayAsycn(payReq);
                Assert.True(r.Succeeded);
                Assert.Equal(r.Data.Amount, payReq.Amount);
            }
        }
    }
}