using LetItGrow.Microservice.Mqtt.Services;
using LetItGrow.Microservice.NotificationHandlers;
using LetItGrow.Microservice.Services;
using LetItGrow.Microservice.Workers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.AttributeRouting;
using MQTTnet.AspNetCore.Extensions;
using MQTTnet.Server;

await new HostBuilder()
.ConfigureCoreHost(args)
.ConfigureWebHostDefaults(options => options
.ConfigureServices((c, services) =>
{
    var configuration = c.Configuration;

    // Add core services etc.
    services.AddCoreHostServices();
    services.AddMicroserviceCore(configuration);
    services.AddMicroserviceInfrastructure(configuration);

    // Add custom services
    services.AddSingleton<MqttServerConnectionHandler>();

    services.AddSingleton<INodeTokenAuthenticator, NodeAuthenticationHandler>();
    services.AddSingleton<INodeConnections, MqttServerConnectionHandler>();
    services.AddSingleton<IMqttServerClientConnectedHandler>(s => s.GetRequiredService<MqttServerConnectionHandler>());
    services.AddSingleton<IMqttServerClientDisconnectedHandler>(s => s.GetRequiredService<MqttServerConnectionHandler>());
    services.AddSingleton<IMqttServerConnectionValidator, MqttServerConnectionValidator>();
    services.AddSingleton<IMqttServerStorage, MqttServerRetainedMessageStorage>();

    // Add MQTT Services
    services.AddMqttControllers();
    services.AddMqttConnectionHandler();
    services.AddConnections();
    services.AddMqttTcpServerAdapter();
    services.AddMqttWebSocketServerAdapter();
    services.AddHostedMqttServerWithServices(b =>
    {
        b.WithoutDefaultEndpoint();
        b.WithStorage(b.GetRequiredService<IMqttServerStorage>());
        b.WithConnectionValidator(b.GetRequiredService<IMqttServerConnectionValidator>());
        b.WithAttributeRouting();
    });

    // Add Extra services
    services.AddMediatR(c => c.AsSingleton(), typeof(NodeAuthenticationHandler).Assembly);
    services.AddMemoryCache();
    services.AddControllers();

    // Add Worker services
    services.AddHostedService<CouchDbNodeChangesWorker>();
    services.AddHostedService<CouchDbIrrigationChangesWorker>();
})
.Configure((c, app) =>
{
    app.UseRouting();
    //app.UseHttpsRedirection();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/", async c => await c.Response.WriteAsync(
            "To access the mqtt server use the /mqtt endoint."));

        endpoints.MapControllers();
        endpoints.MapMqtt("/mqtt");
    });

    app.UseMqttServer(server =>
    {
        server.ClientConnectedHandler = app.GetRequiredService<IMqttServerClientConnectedHandler>();
        server.ClientDisconnectedHandler = app.GetRequiredService<IMqttServerClientDisconnectedHandler>();
    });
}))
.Build()
.RunCoreHostAsync("Mqtt");