using LetItGrow.Microservice.Common.Models;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Models
{
    public record RefreshModel : ModelUpdate
    {
        /// <summary>
        /// The token that was generated.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}