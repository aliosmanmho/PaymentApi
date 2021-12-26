using MessagePack;
using Payment.Providers.Cache.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Models
{
    [MessagePackObject]
    public class CountryCacherModel : BaseCacherModel
    {
        [Key(0)]
        public string Code { get; set; }
        [Key(1)]
        public string Name { get; set; }
    }
}
