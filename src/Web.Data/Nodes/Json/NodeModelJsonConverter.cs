using LetItGrow.Web.Data.Nodes.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetItGrow.Web.Data.Nodes.Json
{
    /// <summary>
    /// Json converter for node model. This determines the child underlying object
    /// to convert it to and from json.
    /// </summary>
    public class NodeModelJsonConverter : JsonConverter<NodeModel>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(NodeModel).IsAssignableFrom(typeToConvert);
        }

        public override NodeModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);

            if (doc.RootElement.TryGetProperty("type", out var type))
            {
                return type.GetString() switch
                {
                    "irrigation" => Deserialize<IrrigationNodeModel>(),
                    "measurement" => Deserialize<MeasurementNodeModel>(),
                    _ => throw new JsonException("Discriminator parameter 'type' has an invalid value.")
                };
            }
            else
            {
                throw new JsonException("Could not find discriminator parameter 'type'");
            }

            T? Deserialize<T>()
            {
                return JsonSerializer.Deserialize<T>(doc.RootElement.GetRawText(), options);
            }
        }

        public override void Write(Utf8JsonWriter writer, NodeModel value, JsonSerializerOptions options)
        {
            if (value is not { Type: "irrigation" or "measurement" })
            {
                throw new JsonException($"Don't know how to serialize NodeModel of type: {value.Type}");
            }

            JsonSerializer.Serialize<object>(writer, value, options);
        }
    }
}