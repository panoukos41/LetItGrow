using LetItGrow.Microservice.Common.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Common.Requests
{
    public record BaseUpdate : BaseUpdate<ModelUpdate>
    {
    }

    public record BaseUpdate<T> : IRequest<T>
    {
        public BaseUpdate() { }

        /// <summary>
        /// The id of the entity to update.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The concurrency stamp of the entity.
        /// </summary>
        [JsonPropertyName("concurrency_stamp")]
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}