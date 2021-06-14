using Microsoft.Extensions.Logging;
using MQTTnet.Server;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Mqtt.Services
{
    public class MqttServerConnectionValidator : IMqttServerConnectionValidator
    {
        private readonly INodeTokenAuthenticator authenticator;
        private readonly ILogger<MqttServerConnectionValidator> logger;

        public MqttServerConnectionValidator(INodeTokenAuthenticator authenticator, ILogger<MqttServerConnectionValidator> logger)
        {
            this.authenticator = authenticator;
            this.logger = logger;
        }

        public async Task ValidateConnectionAsync(MqttConnectionValidatorContext context)
        {
            if (await authenticator.Authenticate(context.Username, context.Password))
            {
                logger.LogInformation("Client '{id}' authorized using '{username}'", context.ClientId, context.Username);
                return;
            }

            logger.LogWarning("Client '{id}' not authorized using '{username}'", context.ClientId, context.Username);
            context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.NotAuthorized;
        }
    }
}