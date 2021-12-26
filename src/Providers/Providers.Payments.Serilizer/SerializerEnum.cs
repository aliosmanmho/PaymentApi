using System.ComponentModel;

namespace Providers.Payments.Serilizer
{
    [DefaultValue(SerializerEnum.None)]
    public enum SerializerEnum
    {
        None,
        JsonSerilize,
        MesagePack,
    }
}