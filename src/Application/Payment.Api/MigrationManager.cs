using Microsoft.EntityFrameworkCore;
using Payment.Bussinies.StaticData;
using Payment.Bussinies.StaticData.FileRead;
using Payment.Core.Entities;
using Payment.Data.Context;

namespace Payment.Api
{
    public static class MigrationManager
    {
        public static readonly string App = Directory.GetCurrentDirectory();

        public static readonly string Templates = Path.Combine(App, "Files");
        /// <summary>
        /// Database Migration Method
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<PaymentContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                        if (!appContext.BinNumbers.Any())
                        {
                             var readData = FileReadParser.ReadBinNumbers(Templates);

                            if(readData != null && readData.IsCompleted)
                            {
                                List<BinNumber> binNumbers = new List<BinNumber>();
                                readData.Result.All(x =>
                                {
                                    var data = new BinNumber()
                                    {
                                        BankCode = x.BankCode,
                                        BankName = x.BankName,
                                        BinCode = x.BinCode,
                                        CartType = x.CartType,
                                        IsActive = x.IsActive,
                                        IsCommercialCard = x.IsCommercialCard,
                                        IsSupportInstallment = x.IsSupportInstallment,
                                        Organization = x.Organization,
                                    };
                                    binNumbers.Add(data);
                                    BinNumberCacher.Instance.Add(data.BinCode.ToString(), data);
                                    return true;
                                    
                                });
                                appContext.BinNumbers.AddRange(binNumbers);
                                appContext.SaveChanges();

                                
                            }
                        }
                        else
                        {
                            var data = appContext.BinNumbers.ToList();
                            data.All(x => {
                                BinNumberCacher.Instance.Add(x.BinCode.ToString(), x);
                                return true;
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
