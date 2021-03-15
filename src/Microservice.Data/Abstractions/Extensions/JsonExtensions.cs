namespace System.Text.Json
{
    public static class JsonExtensions
    {
        public static T To<T>(this JsonDocument? document, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<T>(document?.RootElement.GetRawText() ?? "{}", options)!;
        }

        public static JsonDocument ToJsonDocument<T>(this T? obj, JsonSerializerOptions? options = null, JsonDocumentOptions documentOptions = default)
        {
            return obj is null
                ? JsonDocument.Parse("{}")
                : JsonDocument.Parse(JsonSerializer.Serialize(obj, options), documentOptions);
        }
    }
}