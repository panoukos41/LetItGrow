using FluentValidation;
using LetItGrow.Microservice.Common.Behaviours;
using LetItGrow.Microservice.Common.Validators;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microsoft.Extensions.Hosting
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add Microservice Core services:<br/>
        /// <br/>
        /// - AddMediatR()<br/>
        /// - AddValidatorsFromAssembly()<br/>
        /// - AddTransient(<see cref="IPipelineBehavior{TRequest, TResponse}"/>, <see cref="RequestValidationBehavior{TRequest, TResponse}"/>)<br/>
        /// - AddTransient(<see cref="IPipelineBehavior{TRequest, TResponse}"/>, <see cref="UnhandledExceptionBehavior{TRequest, TResponse}"/>)<br/>
        /// </summary>
        public static IServiceCollection AddMicroserviceCore(this IServiceCollection services, IConfiguration configuration)
        {
            // Add MediatR.
            services.AddMediatR(c => c.AsSingleton(), typeof(DependencyInjection).Assembly);

            // Loads validators.
            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ValidationExtensions)));

            // MediatR pipeline implementations.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

            return services;
        }
    }
}