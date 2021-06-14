using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Identity.Common.Queries
{
    public abstract record BaseGet<TResponse> : IRequest<TResponse>
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}