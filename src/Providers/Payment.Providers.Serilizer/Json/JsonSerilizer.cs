using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payment.Providers.Serilizer
{
    public class JsonSerilizer : ISerilizer
    {
        public TValue DeSerilize<TValue, TResult>(TResult value)
        {
            if (value.Equals(default(TResult)))
                throw new ArgumentNullException($"{nameof(DeSerilizeAsycn)} : value");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(value as string)))
                return JsonSerializer.Deserialize<TValue>(stream);
        }

        public async Task<TValue> DeSerilizeAsycn<TValue, TResult>(TResult value)
        {
            if(value.Equals(default(TResult)))
                throw new ArgumentNullException($"{nameof(DeSerilizeAsycn)} : value");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(value as string)))
                return await JsonSerializer.DeserializeAsync<TValue>(stream);
        }

        public TResult Serilize<TValue, TResult>(TValue value)
        {
            if (value.Equals(default(TValue)))
                throw new ArgumentNullException($"{nameof(SerilizeAsync)} : value");

            string json = string.Empty;
            using (var stream = new MemoryStream())
            {
                JsonSerializer.Serialize<TValue>(stream, value);
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                json =  reader.ReadToEnd();
            }
            return Unsafe.As<string, TResult>(ref json);
        }

        public async Task<TResult> SerilizeAsync<TValue, TResult>(TValue value)
        {
             if (value.Equals(default(TValue)))
                throw new ArgumentNullException($"{nameof(SerilizeAsync)} : value");

            string json = string.Empty;
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync<TValue>(stream, value);
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                json = await reader.ReadToEndAsync();
            }
            return Unsafe.As<string, TResult>(ref json);
        }
    }
}
