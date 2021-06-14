using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace System.Text.Json
{
    public static class JsonExtensions
    {
        public static JObject ToJObject<T>(this JsonDocument? document)
        {
            return JObject.Parse(document?.RootElement.GetRawText() ?? "{}");
        }

        public static string GetJsonString(this JsonDocument document)
        {
            return document.RootElement.GetRawText();
        }
        
        public static int GetJsonHashCode(this JsonDocument document)
        {
            return document.GetJsonString().GetHashCode();
        }
    }
}

namespace Newtonsoft.Json
{
    public static class JsonExtensions
    {
        public static JsonDocument ToJsonDocument(this JObject? obj)
        {
            return JsonDocument.Parse(obj?.ToString(Formatting.None) ?? "{}");
        }
    }
}