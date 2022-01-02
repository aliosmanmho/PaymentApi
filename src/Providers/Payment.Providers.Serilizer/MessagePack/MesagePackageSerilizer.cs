using MessagePack;
using MessagePack.Resolvers;
using System.Runtime.CompilerServices;
using System.Text;

namespace Payment.Providers.Serilizer
{
    public class MesagePackageSerilizer : ISerilizer
    {
        //TODO:Configuration Read
        private readonly MessagePackSerializerOptions messagePackSerializerOptions = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        public TValue DeSerilize<TValue, TResult>(TResult value)
        {
            if (value.Equals(default(TValue)))
                throw new ArgumentNullException($"{nameof(DeSerilizeAsycn)} : value");
          
            return MessagePackSerializer.Deserialize<TValue>(value as byte[], messagePackSerializerOptions);
        }

        public async Task<TValue> DeSerilizeAsycn<TValue, TResult>(TResult value)
        {
            if (value.Equals(default(TValue)))
                throw new ArgumentNullException($"{nameof(DeSerilizeAsycn)} : value");

            using (var stream = new MemoryStream(value as byte[]))
                return await MessagePackSerializer.DeserializeAsync<TValue>(stream, messagePackSerializerOptions);
        }

        public TResult Serilize<TValue, TResult>(TValue value)
        {
            if (value.Equals(default(TValue)))
                throw new ArgumentNullException($"{nameof(SerilizeAsync)} : value");

            var data = MessagePackSerializer.Serialize<TValue>(value, messagePackSerializerOptions);
            return Unsafe.As<byte[], TResult>(ref data);
        }

        public async Task<TResult> SerilizeAsync<TValue, TResult>(TValue value)
        {
            if (value.Equals(default(TValue)))
                throw new ArgumentNullException($"{nameof(SerilizeAsync)} : value");

            using (var byteStream = new MemoryStream())
            {
                await MessagePackSerializer.SerializeAsync<TValue>(byteStream, value, messagePackSerializerOptions);
                var data = byteStream.ToArray();
                return Unsafe.As<byte[], TResult>(source: ref data);
            }
        }
    }
}
