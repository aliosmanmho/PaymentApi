using Payment.Bussinies.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.Models.Responses
{
    public class PaymentPayResponse : BaseResponse
    {
        public decimal Amount { get; set; }
    }
}
