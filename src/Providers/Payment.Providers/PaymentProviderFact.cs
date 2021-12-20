using Payment.Providers.BankProviders.Akbank;
using Payment.Providers.BankProviders.ZiraatBank;
using Payment.Providers.Enums;

namespace Payment.Providers
{
    public class PaymentProviderFact
    {
        public static IPaymentProvider GetPaymentBankProvider(BankCode bankCode)
        {
            switch (bankCode)
            {
                case BankCode.TZiraatBank:
                    return new ZiraatBankPaymentProvider();
                case BankCode.Akbank:
                    return new AkBankPaymentProvider();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
