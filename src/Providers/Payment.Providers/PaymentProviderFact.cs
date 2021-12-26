using Payment.Providers.Enums;

namespace Payment.Providers
{
    public class PaymentProviderFact
    {
        public static PaymentProviderFact Instance { get; private set; } = new PaymentProviderFact();

        private Dictionary<BankCode, IPaymentProvider> _payments = new Dictionary<BankCode, IPaymentProvider>();
        private PaymentProviderFact()
        {
            foreach (BankCode bank in Enum.GetValues(typeof(BankCode)))
            {
                Type? t = Type.GetType($"Payment.Providers.BankProviders.{Enum.GetName(typeof(BankCode), bank)}PaymentProvider");
                if (t != null)
                {
                    var factory = Activator.CreateInstance(t);
                    if (factory != null)
                        _payments.Add(bank, (IPaymentProvider)factory);
                }
            }
        }
        public IPaymentProvider GetPaymentBankProvider(BankCode bankCode)
        {
            if (!_payments.TryGetValue(bankCode, out var value))
                throw new NotImplementedException($"Not İmplement Payment Provider : {Enum.GetName(typeof(BankCode), bankCode)}");
            return value;
        }
    }
}
