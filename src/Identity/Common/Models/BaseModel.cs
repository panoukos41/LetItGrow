using System.Text.Json.Serialization;

namespace LetItGrow.Identity.Common.Models
{
    public abstract record BaseModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("rev")]
        public string Rev { get; set; } = string.Empty;
    }
}