using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.AspNetCore.AttributeRouting;
using System;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Mqtt.MqttControllers.Internal
{
    [MqttController]
    public abstract class MqttControllerBase : MqttBaseController
    {
        private readonly ISender _sender;
        private readonly ILoggerFactory _loggerFactory;

        public MqttControllerBase(IServiceProvider provider)
        {
            _loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            _sender = provider.GetRequiredService<ISender>();
        }

        protected string ClientId => MqttContext.ClientId;

        protected string Topic => Message.Topic;

        protected ILogger<T> GetLogger<T>() => _loggerFactory.CreateLogger<T>();

        protected Task Send<TRequest, TResult>()
            where TRequest : class, IRequest<TResult>
        {
            return Send(Message.GetPayload<TRequest>()!);
        }

        protected async Task Send<TResult>(IRequest<TResult> request)
        {
            if (request is null)
            {
                await BadMessage().ConfigureAwait(false);
                return;
            }

            try
            {
                await _sender.Send(request).ConfigureAwait(false);
                await Ok().ConfigureAwait(false);
            }
            catch // Errors are handled/reported on mediatr level
            {
                await BadMessage().ConfigureAwait(false);
            }
        }
    }
}