using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Common.Behaviours
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<TRequest> logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (ErrorException ex)
            {
                logger.LogWarning("Handled {Error} at Request {Request}.", ex.Error, request);

                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled Error at Request {Request} {@Details} {@InnerDetails} ", request, ex.Message, ex.InnerException?.Message);

                throw;
            }
        }
    }
}