using LetItGrow.Microservice.Mqtt.MqttControllers.Internal;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore.AttributeRouting;
using System;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.MqttControllers
{
    public class DefaultMqttController : MqttControllerBase
    {
        public DefaultMqttController(IServiceProvider provider) : base(provider)
        {
        }

        [MqttRoute("test")]
        public Task Test()
        {
            GetLogger<DefaultMqttController>().LogInformation(
                "Client '{clientId}' published on topic '{topic}'",
                ClientId, Topic);

            return Ok();
        }
    }
}