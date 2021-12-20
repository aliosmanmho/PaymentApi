using Payment.Core.Enums;
using Payment.Core.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Core.Entities
{
    public class PaymentActivity
    {
        public PaymentActivity()
        {
            this.Status = PaymentActivityStatus.Succes.GetValue<int>();
        }
        [Key]
        public int Id { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int Status { get; set; }
        //[Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
    }
}
