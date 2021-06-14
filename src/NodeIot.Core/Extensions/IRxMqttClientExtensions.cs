using LetItGrow.Microservice.Common;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Reactive.Linq;

namespace MQTTnet.Extensions.External.RxMQTT.Client
{
    public static class IRxMqttClientExtensions
    {
        /// <summary>
        /// Publish a payload that will be serialized using the <see cref="ProtoSerializer"/>.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="client">The client to use.</param>
        /// <param name="topic">The topic to publish to.</param>
        /// <param name="payload">The payload to publish.</param>
        /// <param name="retain">Flag indicating if the server should retain this message or not.</param>
        public static IObservable<RxMqttClientPublishResult> Publish<TPayload>(this IRxMqttClient client,
            string topic, TPayload payload, bool retain = false) where TPayload : class
        {
            return client.Publish(
                Observable
                .Return(new ManagedMqttApplicationMessageBuilder()
                .WithApplicationMessage(msg => msg
                    .WithAtLeastOnceQoS()
                    .WithTopic(topic)
                    .WithContentType(ContentTypes.Proto)
                    .WithPayload(ProtoSerializer.Serialize(payload))
                    .WithRetainFlag(retain)
                    .Build())
                .Build()));
        }
    }
}