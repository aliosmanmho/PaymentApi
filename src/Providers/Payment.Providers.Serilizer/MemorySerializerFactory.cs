namespace Payment.Providers.Serilizer
{
    public class MemorySerializerFactory
    {
        public static MemorySerializerFactory Instance { get; private set; } = new MemorySerializerFactory();
     
        private Dictionary<MemorySerializer, ISerilizer> _serilizers = new Dictionary<MemorySerializer, ISerilizer>();
        private MemorySerializerFactory()
        {
            foreach (MemorySerializer serilize in Enum.GetValues(typeof(MemorySerializer)))
            {
                Type? t = Type.GetType($"Payment.Providers.Serilizer.{Enum.GetName(typeof(MemorySerializer), serilize)}Serilizer");
                if (t != null)
                {
                    var factory = Activator.CreateInstance(t);
                    if (factory != null)
                        _serilizers.Add(serilize, (ISerilizer)factory);
                }
            }
        }
        public ISerilizer GetSerilizer(MemorySerializer serializer)
        {
            if (!_serilizers.TryGetValue(serializer, out var value))
                throw new NotImplementedException($"Not İmplement Payment Provider : {Enum.GetName(typeof(MemorySerializer), serializer)}");
            return value;
        }
    }
}
