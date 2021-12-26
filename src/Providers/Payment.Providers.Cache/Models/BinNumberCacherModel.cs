using MessagePack;
using Payment.Core.Entities;
using Payment.Providers.Cache.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Models
{
    [MessagePackObject]
    public class BinNumberCacherModel : BaseCacherModel
    {
        [Key(0)]
        public int BinCode { get; set; }
        [Key(1)]
        public int BankCode { get; set; }
        [Key(2)]
        public string BankName { get; set; }
        [Key(3)]
        public string CartType { get; set; }
        [Key(4)]
        public string Organization { get; set; }
        [Key(5)]
        public bool IsCommercialCard { get; set; }
        [Key(6)]
        public bool IsSupportInstallment { get; set; }
        [Key(7)]
        public bool IsActive { get; set; }

        public static BinNumberCacherModel ToCacheModel(BinNumber model)
        {
            if (model == null)
                return null;
            return new BinNumberCacherModel
            {
                BankCode = model.BankCode,
                BinCode = model.BinCode,
                Organization = model.Organization,
                CartType = model.CartType,
                BankName = model.BankName,
                IsSupportInstallment = model.IsSupportInstallment,
                IsActive = model.IsActive,
                IsCommercialCard = model.IsCommercialCard,
            };
        }

        public static BinNumber ToModel(BinNumberCacherModel model)
        {
            if (model == null)
                return null;
            return new BinNumber
            {
                BankCode = model.BankCode,
                BinCode = model.BinCode,
                Organization = model.Organization,
                CartType = model.CartType,
                BankName = model.BankName,
                IsSupportInstallment = model.IsSupportInstallment,
                IsActive = model.IsActive,
                IsCommercialCard = model.IsCommercialCard,
            };
        }
    }
}
