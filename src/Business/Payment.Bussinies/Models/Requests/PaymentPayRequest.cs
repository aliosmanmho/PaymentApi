using Payment.Bussinies.Models.Requests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.Models.Requests
{
    /// <summary>
    /// Pay Request
    /// </summary>
    public class PaymentPayRequest : BaseRequest
    {
        /// <summary>
        /// Credit Cart No
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// Credit Cart Owner Name
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// CCV
        /// </summary>
        public int Ccv { get; set; }
        /// <summary>
        /// Card Expire Month
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// Card Expire Year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Pay Amount
        /// </summary>
        public decimal Amount { get; set; }

    }
}
