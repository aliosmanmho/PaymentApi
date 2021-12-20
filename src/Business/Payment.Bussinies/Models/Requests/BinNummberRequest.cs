using Payment.Bussinies.Models.Requests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.Models.Requests
{
    /// <summary>
    /// Car BinNumber Request
    /// </summary>
    public class BinNummberRequest : BaseRequest
    {
        /// <summary>
        /// Cart First 6 Number
        /// </summary>
        public int BinNummber { get; set; }
    }
}
