using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.StaticData.FileRead.Model
{
    public class BinNumberFr
    {
        public int BinCode { get; set; }
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public string CartType { get; set; }
        public string Organization { get; set; }
        public bool IsCommercialCard { get; set; }
        public bool IsSupportInstallment { get; set; }
        public bool IsActive { get; set; }
    }
}
