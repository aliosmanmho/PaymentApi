using System.ComponentModel;

namespace Providers.Payments.Serilizer
{
    [DefaultValue(SerializerEnum.Json)]
    public enum SerializerEnum
    {
        Json,
        MesagePackage,
    }
}