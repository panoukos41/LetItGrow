using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Common.Requests
{
    public record BaseDelete : IRequest<Unit>
    {
        /// <summary>
        /// The id of the entity.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The entity's concurrency stamp.
        /// </summary>
        [JsonPropertyName("concurrency_stamp")]
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}