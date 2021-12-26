using Providers.Payments.Serilizer.Json;
using Providers.Payments.Serilizer.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Payments.Serilizer
{
    public class SerializerFactory
    {
        public static ISerilizer GetSerilizer(SerializerEnum val)
        {
            switch (val)
            {
                case SerializerEnum.JsonSerilize:
                    return new JsonSerilizer();
                case SerializerEnum.MesagePack:
                    return new MesagePackageSerilizer();
                default:
                    throw new NotImplementedException(nameof(val));
            }
        }
    }
}
