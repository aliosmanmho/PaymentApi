using System.ComponentModel;

namespace Payment.Providers.Serilizer
{
    [DefaultValue(SerializerEnum.Json)]
    public enum SerializerEnum
    {
        Json,
        MesagePackage,
    }
}