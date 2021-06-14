using LetItGrow.Microservice.Common;
using System.Text.Json;

namespace MQTTnet
{
    public static class MqttExtensions
    {
        public static JsonSerializerOptions? JsonOptions { get; set; }

        public static bool IsProto(this MqttApplicationMessage message) =>
            message.ContentType == ContentTypes.Proto;

        public static bool IsJson(this MqttApplicationMessage message) =>
            message.ContentType == ContentTypes.Json;

        public static T GetProtoPayload<T>(this MqttApplicationMessage message)
            where T : class =>
            ProtoSerializer.Deserialize<T>(message.Payload);

        public static T GetJsonPayload<T>(this MqttApplicationMessage message)
            where T : class =>
            JsonSerializer.Deserialize<T>(message.Payload, JsonOptions)!;

        public static T? GetPayload<T>(this MqttApplicationMessage message) where T : class
        {
            if (message.IsProto()) return message.GetProtoPayload<T>();

            if (message.IsJson()) return message.GetJsonPayload<T>();

            return null;
        }

        public static MqttApplicationMessageBuilder WithProtoPayload<T>(this MqttApplicationMessageBuilder builder, T payload)
            where T : class =>
            builder
                .WithContentType(ContentTypes.Proto)
                .WithPayload(ProtoSerializer.Serialize(payload));

        public static MqttApplicationMessageBuilder WithJsonPayload<T>(this MqttApplicationMessageBuilder builder, T payload)
            where T : class =>
            builder
                .WithContentType(ContentTypes.Json)
                .WithPayload(JsonSerializer.Serialize(payload));
    }
}