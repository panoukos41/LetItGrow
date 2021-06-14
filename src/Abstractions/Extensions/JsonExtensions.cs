namespace System.Text.Json
{
    public static partial class JsonExtensions
    {
        public static string ToJson<T>(this T obj, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(obj, options);
        }

        public static T FromJson<T>(this string json, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<T>(json, options)!;
        }
    }
}