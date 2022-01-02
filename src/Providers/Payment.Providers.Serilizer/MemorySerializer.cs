using System.ComponentModel;

namespace Payment.Providers.Serilizer
{
    [DefaultValue(MemorySerializer.Json)]
    public enum MemorySerializer
    {
        Json,
        MesagePackage,
    }
}