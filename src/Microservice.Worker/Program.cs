using Microservice.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await new HostBuilder()
.ConfigureCoreHost(args)
.ConfigureServices((context, services) =>
{
    var conf = context.Configuration;

    services.AddCoreHostServices();
    services.AddMicroserviceCore(conf);
    services.AddMicroserviceInfrastructure(conf);

    services.AddMemoryCache();

    services.AddSingleton<IIrrigator>(s => s.GetRequiredService<Worker>());
    services.AddHostedService<Worker>();
})
.Build()
.RunCoreHostAsync("Worker");
