using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Mqtt.MqttControllers.Internal;
using LetItGrow.Microservice.Node.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore.AttributeRouting;
using System;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.MqttControllers
{
    public class NodeMqttController : MqttControllerBase
    {
        public NodeMqttController(IServiceProvider provider) : base(provider)
        {
        }

        [MqttRoute("node/connection/{nodeId}")]
        public Task Connection(string nodeId)
        {
            GetLogger<NodeMqttController>().LogDebug("Publish Connection");
            return ClientId is null ? Ok() : BadMessage();
        }

        [MqttRoute("node/settings/{nodeId}")]
        public Task Settings(string nodeId)
        {
            GetLogger<NodeMqttController>().LogDebug("Publish Settings");
            return ClientId is null ? Ok() : BadMessage();
        }

        [MqttRoute("node/irrigation/{nodeId}")]
        public Task Irrigate(string nodeId)
        {
            GetLogger<NodeMqttController>().LogDebug("Publish Irrigation");
            return ClientId is null ? Ok() : BadMessage();
        }

        [MqttRoute("node/measurement/{nodeId}")]
        public Task Measure(string nodeId) =>
            VerifyClientIdEqualsNodeId(nodeId)
                ? Send<CreateMeasurement, Unit>()
                : BadMessage();

        private bool VerifyClientIdEqualsNodeId(string nodeId)
        {
            if (ClientId == nodeId) return true;

            GetLogger<NodeModel>().LogInformation(
                "Client '{clientId}' tried to publish for node '{nodeId}'",
                ClientId, nodeId);
            return false;
        }
    }
}