using Payment.Bussinies.StaticData.FileRead.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace Payment.Bussinies.StaticData.FileRead.Mapping
{
    public class BinNumberMapping : CsvMapping<BinNumberFr>
    {
        public BinNumberMapping()
            : base()
        {
            MapProperty(0, x => x.BinCode);
            MapProperty(1, x => x.BankCode);
            MapProperty(2, x => x.BankName);
            MapProperty(3, x => x.CartType);
            MapProperty(4, x => x.Organization);
            MapProperty(5, x => x.IsCommercialCard);
            MapProperty(6, x => x.IsSupportInstallment);
            MapProperty(7, x => x.IsActive);
        }
    }
}
