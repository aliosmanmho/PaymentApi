using Payment.Bussinies.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.Models.Responses
{
    public class BinNumberResponse : BaseResponse
    {
        public int BankCode { get; set; }
        public int BinCode { get; set; }
        public string BankName { get; set; }
        public string CartType { get; set; }
        public string Organization { get; set; }
    }
}
