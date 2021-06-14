using System.Text.Json.Serialization;

namespace LetItGrow.Identity.Common.Models
{
    public record UpdateModel
    {
        [JsonConstructor]
        public UpdateModel(string id, string rev)
        {
            Id = id;
            Rev = rev;
        }

        [JsonPropertyName("id")]
        public string Id { get; }

        [JsonPropertyName("rev")]
        public string Rev { get; }
    }
}