using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Api.Controllers.Base;
using Payment.Bussinies;
using Payment.Bussinies.Models.Requests;
using Payment.Bussinies.Models.Responses;
using Payment.Bussinies.Repositories.Interfaces;

namespace Payment.Api.Controllers
{
    /// <summary>
    /// Payment Process
    /// </summary>
    [ApiVersion("1")]
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        IPaymentRepository _paymentRepository;
        /// <summary>
        /// Payment Constructor
        /// </summary>
        /// <param name="logger"></param>
        public PaymentController(ILogger<PaymentController> logger, IPaymentRepository paymentRepository) : base(logger) {
            _paymentRepository = paymentRepository;
        }

        [HttpPost("GetBinNumber")]
        public async Task<ServiceResponse<BinNumberResponse>> GetBinNumber([FromBody] BinNummberRequest binNummberRequest)
        {
            return await _paymentRepository.GetByBinNoAsycn(binNummberRequest);
        }
        [HttpPost("Pay")]
        public async Task<ServiceResponse<PaymentPayResponse>> Pay([FromBody] PaymentPayRequest paymentPayRequest)
        {
            return await _paymentRepository.PayAsycn(paymentPayRequest);
        }

    }
}
