using CouchDB.Driver;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Services;
using LetItGrow.Microservice.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds Microservice Infrastructure services:<br/>
        /// <br/>
        /// - AddSingleton(<see cref="IEntityManager"/>, <see cref="EntityManager"/>)<br/>
        /// - AddSingleton(<see cref="ICouchClient"/>, <see cref="CouchClient"/>)<br/>
        /// - AddSingleton(<see cref="INodeStore"/>, <see cref="NodeStore"/>)<br/>
        /// - AddSingleton(<see cref="IGroupStore"/>, <see cref="GroupStore"/>)<br/>
        /// - AddSingleton(<see cref="IIrrigationStore"/>, <see cref="IrrigationStore"/>)<br/>
        /// - AddSingleton(<see cref="IMeasurementStore"/>, <see cref="MeasurementStore"/>)<br/>
        /// </summary>
        public static IServiceCollection AddMicroserviceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEntityManager, EntityManager>();

            services.AddSingleton<ICouchClient>(new CouchClient(o => o
                //.ConfigureFlurlClient(s =>
                //{
                //    var jsonSettings = new JsonSerializerSettings
                //    {
                //        NullValueHandling = NullValueHandling.Ignore,
                //        DefaultValueHandling = DefaultValueHandling.Ignore,
                //    }
                //    s.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
                //})
                .Configure(configuration)));

            services.AddSingleton<INodeStore, NodeStore>();
            services.AddSingleton<IGroupStore, GroupStore>();
            services.AddSingleton<IIrrigationStore, IrrigationStore>();
            services.AddSingleton<IMeasurementStore, MeasurementStore>();

            return services;
        }
    }
}