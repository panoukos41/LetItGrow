using LetItGrow.NodeIot;
using LetItGrow.NodeIot.Workers;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.External.RxMQTT.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MqttServiceConfiguration
    {
        /// <summary>
        /// Registers:<br/>
        /// <br/>
        /// - Singleton <see cref="IManagedMqttClientOptions"/><br/>
        /// - Singleton <see cref="IRxMqttClient"/><br/>
        /// - HostedService <see cref="MqttWorker"/><br/>
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The ServiceCollection for further configuration.</returns>
        public static IServiceCollection AddMqtt(this IServiceCollection services)
        {
            services.AddHostedService<MqttWorker>();
            services.AddSingleton<IRxMqttClient>(new MqttFactory().CreateRxMqttClient());

            services.AddSingleton<IManagedMqttClientOptions>(new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(15))
                //.WithStorage() todo: MqttClient Storage.
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithProtocolVersion(MqttProtocolVersion.V500)
                    .WithKeepAlivePeriod(TimeSpan.FromMinutes(1))
                    .WithCredentials(Config.ClientId, Config.Token)
                    .WithClientId(Config.ClientId)
                    .WithWebSocketServer($"{Config.Server}/mqtt")
                    .Build())
                .Build());

            return services;
        }
    }
}