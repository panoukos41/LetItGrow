namespace System.Text.Json
{
    public static partial class JsonExtensions
    {
        public static string ToJson(this object obj, JsonSerializerOptions? options = null)
        {
            return obj.ToJson<object>(options);
        }

        public static string ToJson<T>(this T obj, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(obj, options);
        }
    }
}