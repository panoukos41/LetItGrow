using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger<TRequest> logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request {Request}", request);

            return Task.CompletedTask;
        }
    }
}