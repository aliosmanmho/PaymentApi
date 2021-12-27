namespace Payment.Providers.Serilizer
{
    public class SerializerFactory
    {
        public static SerializerFactory Instance { get; private set; } = new SerializerFactory();
     
        private Dictionary<SerializerEnum, ISerilizer> _serilizers = new Dictionary<SerializerEnum, ISerilizer>();
        private SerializerFactory()
        {
            foreach (SerializerEnum serilize in Enum.GetValues(typeof(SerializerEnum)))
            {
                Type? t = Type.GetType($"Payment.Providers.Serilizer.{Enum.GetName(typeof(SerializerEnum), serilize)}Serilizer");
                if (t != null)
                {
                    var factory = Activator.CreateInstance(t);
                    if (factory != null)
                        _serilizers.Add(serilize, (ISerilizer)factory);
                }
            }
        }
        public ISerilizer GetSerilizer(SerializerEnum serializer)
        {
            if (!_serilizers.TryGetValue(serializer, out var value))
                throw new NotImplementedException($"Not İmplement Payment Provider : {Enum.GetName(typeof(SerializerEnum), serializer)}");
            return value;
        }
    }
}
