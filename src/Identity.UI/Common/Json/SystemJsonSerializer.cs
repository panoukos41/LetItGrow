using Flurl.Http.Configuration;
using System;
using System.IO;
using System.Text.Json;

namespace LetItGrow.Identity.Common.Json
{
    public class SystemJsonSerializer : ISerializer
    {
        private readonly JsonSerializerOptions? _options;

        public SystemJsonSerializer(JsonSerializerOptions? options = null)
        {
            _options = options;
        }

        /// <inheritdoc/>
        public T Deserialize<T>(string s)
        {
            return JsonSerializer.Deserialize<T>(s, _options)!;
        }

        /// <inheritdoc/>
        public T Deserialize<T>(Stream stream)
        {
            using var reader = new StreamReader(stream);
            return Deserialize<T>(reader.ReadToEnd());
        }

        /// <inheritdoc/>
        public string Serialize(object obj)
        {
            Console.WriteLine("Called Serialize");
            return JsonSerializer.Serialize(obj, _options);
        }
    }
}