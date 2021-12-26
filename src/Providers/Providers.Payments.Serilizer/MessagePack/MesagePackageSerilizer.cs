using MessagePack;
using MessagePack.Resolvers;
using System.Text;

namespace Providers.Payments.Serilizer
{
    public class MesagePackageSerilizer : ISerilizer
    {
        //TODO:Configuration Read
        private readonly MessagePackSerializerOptions messagePackSerializerOptions = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        public T DeSerilize<T>(object value) 
        {
            if (value == null || value.ToString() == string.Empty)
                throw new ArgumentNullException($"{nameof(DeSerilizeAsycn)} : value");

            return MessagePackSerializer.Deserialize<T>((byte[])value, messagePackSerializerOptions);
        }

        public async Task<T> DeSerilizeAsycn<T>(object value)
        {
            if (value == null || value.ToString() == string.Empty)
                throw new ArgumentNullException($"{nameof(DeSerilizeAsycn)} : value");

            using (var stream = new MemoryStream((byte[])value))
                return await MessagePackSerializer.DeserializeAsync<T>(stream, messagePackSerializerOptions);
        }

        public object Serilize<T>(T value) where T : class
        {
            if (value == default(T))
                throw new ArgumentNullException($"{nameof(SerilizeAsync)} : value");

            return MessagePackSerializer.Serialize<T>(value, messagePackSerializerOptions);
        }

        public async Task<object> SerilizeAsync<T>(T value) where T : class
        {
            if (value == default(T))
                throw new ArgumentNullException($"{nameof(SerilizeAsync)} : value");

            using (var byteStream = new MemoryStream())
            {
                await MessagePackSerializer.SerializeAsync<T>(byteStream, value, messagePackSerializerOptions);
                return byteStream.ToArray();
            }
        }
    }
}
