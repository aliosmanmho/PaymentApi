using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payment.Providers.Serilizer
{
    public class JsonSerilizer : ISerilizer
    {
        public T DeSerilize<T>(object value)
        {
            if (value == null || value.ToString() == string.Empty)
                throw new ArgumentNullException($"{nameof(DeSerilizeAsycn)} : value");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes((string)value)))
                return JsonSerializer.Deserialize<T>(stream);
        }

        public async Task<T> DeSerilizeAsycn<T>(object value)
        {
            if(value == null || value.ToString() == string.Empty)
                throw new ArgumentNullException($"{nameof(DeSerilizeAsycn)} : value");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes((string)value)))
                return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        public object Serilize<T>(T value) where T : class
        {
            if (value == default(T))
                throw new ArgumentNullException($"{nameof(SerilizeAsync)} : value");

            string json = string.Empty;
            using (var stream = new MemoryStream())
            {
                JsonSerializer.Serialize<T>(stream, value);
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                json =  reader.ReadToEnd();
            }
            return json;
        }

        public async Task<object> SerilizeAsync<T>(T value) where T : class
        {
            if(value == default(T))
                throw new ArgumentNullException($"{nameof(SerilizeAsync)} : value");

            string json = string.Empty;
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync<T>(stream, value);
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                json = await reader.ReadToEndAsync();
            }
            return json;
        }
    }
}
