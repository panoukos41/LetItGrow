using LetItGrow.Microservice.Node.Models;
using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Notifications
{
    public record NodeSettingsUpdated : INotification
    {
        // todo: Improve class

        [JsonPropertyName("id")]
        public string Id { get; }

        [JsonPropertyName("type"), JsonConverter(typeof(JsonStringEnumConverter))]
        public NodeType Type { get; }

        [JsonPropertyName("settings")]
        public JsonDocument Settings { get; }

        public NodeSettingsUpdated(string id, NodeType type, JsonDocument settings)
        {
            Id = id;
            Type = type;
            Settings = settings;
        }

        public void Deconstruct(out string id, out NodeType type, out JsonDocument settings)
        {
            id = Id;
            type = Type;
            settings = Settings;
        }

        public static NodeSettingsUpdated FromJson(string json, JsonSerializerOptions? options = null) =>
            JsonSerializer.Deserialize<NodeSettingsUpdated>(json, options)!;
    }
}