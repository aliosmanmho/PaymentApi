using Microsoft.Extensions.Logging;
using Payment.Bussinies.Base.Repositories;
using Payment.Bussinies.Extensions;
using Payment.Bussinies.Models.Requests;
using Payment.Bussinies.Models.Responses;
using Payment.Bussinies.Repositories.Interfaces;
using Payment.Bussinies.StaticData;
using Payment.Core.Entities;
using Payment.Core.Enums;
using Payment.Core.Extension;
using Payment.Core.Repositories;
using Payment.Providers;
using Payment.Providers.Enums;

namespace Payment.Bussinies.Repositories
{
    public class PaymentRepository : BaseRepo, IPaymentRepository
    {
        IPaymentAvtivityRepository _paymentAvtivityRepository;
        IPaymentBinNumberRepository _paymentBinNumberRepository;
        public PaymentRepository(IPaymentAvtivityRepository paymentAvtivityRepository, IPaymentBinNumberRepository paymentBinNumberRepository, ILogger<BaseRepo> logger) : base(logger)
        {
            _paymentAvtivityRepository = paymentAvtivityRepository;
            _paymentBinNumberRepository = paymentBinNumberRepository;
        }

        public async Task<ServiceResponse<BinNumberResponse>> GetByBinNoAsycn(BinNummberRequest binNummberRequest)
        {
            ServiceResponse<BinNumberResponse> serviceResponse = new ServiceResponse<BinNumberResponse>();
            try
            {
                _logger.Log(LogLevel.Information, "GetBinNo Start", binNummberRequest);
                if (BinNumberCacher.Instance.GetCount() <= 0)
                {
                    var binMubers = await _paymentBinNumberRepository.GetAllAsync();
                    binMubers.All(x =>
                    {
                        BinNumberCacher.Instance.Add(x.BinCode.ToString(), x);
                        return true;
                    });
                }
                var binNumber = BinNumberCacher.Instance.GetOrNull(binNummberRequest.BinNummber.ToString()).TryCast<BinNumber>();

                if (binNumber == null)
                    throw new Exception($"Not Found Bin Number! Number:{binNummberRequest.BinNummber}");
                var resp = new BinNumberResponse()
                {
                    BankCode = binNumber.BankCode,
                    BankName = binNumber.BankName,
                    BinCode = binNumber.BinCode,
                    CartType = binNumber.CartType,
                    Organization = binNumber.Organization
                };
                _logger.Log(LogLevel.Information, "GetBinNo End", resp);
                serviceResponse.Data = resp;
            }
            catch (Exception ex)
            {
                serviceResponse.Succeeded = false;
                serviceResponse.Errors.Append(ex.ToString());
            }
           
            return serviceResponse;
        }

        public async Task<ServiceResponse<PaymentPayResponse>> PayAsycn(PaymentPayRequest paymentPayRequest)
        {
            ServiceResponse<PaymentPayResponse> serviceResponse = new ServiceResponse<PaymentPayResponse>();
            try
            {
                _logger.Log(LogLevel.Information, "PayAsycn Start", paymentPayRequest);
                PaymentActivity paymentActivity = new PaymentActivity()
                {
                    Amount = paymentPayRequest.Amount,
                    CreateDate = DateTime.Now,
                    Status = PaymentActivityStatus.Succes.GetValue<int>()
                };

                var binNumber = await GetByBinNoAsycn(new BinNummberRequest() { BinNummber = paymentPayRequest.CardNo.GetBimNumber() });
                if (!binNumber.Succeeded)
                    throw new Exception("Get Bin Number Don't Succeeded");
                IPaymentProvider paymentProvider = PaymentProviderFact.GetPaymentBankProvider(binNumber.Data.BankCode.ToEnum<BankCode>());
                var payResponse = await paymentProvider.PayAsync(new Providers.Model.Requests.PaymentPayProviderRequest() { Amount = paymentPayRequest.Amount });
                if (payResponse == null)
                {
                    paymentActivity.Status = PaymentActivityStatus.Fail.GetValue<int>();
                    await _paymentAvtivityRepository.AddAsync(paymentActivity);
                    throw new Exception("Payment Provider Return Null");
                }
                await _paymentAvtivityRepository.AddAsync(paymentActivity);
                var resp = new PaymentPayResponse() { Amount = payResponse.Amount };
                _logger.Log(LogLevel.Information, "PayAsycn End", resp);
                serviceResponse.Data = resp;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Warning, nameof(PayAsycn), ex);
                serviceResponse.Succeeded = false;
                serviceResponse.Errors.Append(ex.ToString());
            }
            return serviceResponse;
        }
    }
}
