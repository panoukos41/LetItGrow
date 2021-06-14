using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Common.Requests
{
    public abstract record BaseFind<TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// The id for the entity to find.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}