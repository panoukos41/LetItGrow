using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Identity.Common.Commands
{
    public record BaseDelete : IRequest<Unit>
    {
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("rev")]
        public string Rev { get; init; } = string.Empty;
    }
}