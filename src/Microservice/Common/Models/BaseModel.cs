using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Common.Models
{
    public abstract record BaseModel
    {
        /// <summary>
        /// The id of the model.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The concurrency stamp of the model.
        /// </summary>
        [JsonPropertyName("concurrency_stamp")]
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}