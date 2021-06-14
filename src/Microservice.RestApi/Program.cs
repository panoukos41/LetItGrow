using LetItGrow.Microservice.RestApi.Hubs;
using LetItGrow.Microservice.RestApi.Hubs.Internal;
using LetItGrow.Microservice.Workers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;
using Serilog;
using System;

await new HostBuilder()
.ConfigureCoreHost(args)
.ConfigureWebHostDefaults(options => options
.ConfigureServices((c, services) =>
{
    var configuration = c.Configuration;

    services.AddAuthentication(options =>
    {
        options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    });

    services.AddOpenIddict()
        // Configure validation.
        .AddValidation(options =>
        {
            // Note: the validation handler uses OpenID Connect discovery
            // to retrieve the issuer signing keys used to validate tokens.
            options.SetIssuer(configuration.GetService("identity").TrimEnd('/'));

            // Register the encryption credentials.
            options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String(configuration.GetSecret("jwt"))));

            // Register the System.Net.Http integration.
            options.UseSystemNetHttp();

            // Register the ASP.NET Core host.
            options.UseAspNetCore();
        });
    services.AddControllers();
    services.AddSignalR();

    services.AddCoreHostServices();
    services.AddMicroserviceCore(configuration);
    services.AddMicroserviceInfrastructure(configuration);

    // Add notification handlers.
    services.AddMediatR(c => c.AsSingleton(), typeof(HubBase).Assembly);

    // Add Extra services
    services.AddMemoryCache();

    services.AddHostedService<CouchDbNodeChangesWorker>();
    services.AddHostedService<CouchDbGroupChangesWorker>();

    services.TryAddSwagger("RestApi", "1", configuration);
})
.Configure((c, app) =>
{
    var configuration = c.Configuration;
    var enviroment = c.HostingEnvironment;

    if (enviroment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    // All the calls after this will always try to redirct to https.
    // For better security modify the Urls in appsettings to use only https endpoints.
    app.UseHttpsRedirection();

    // Enable static files.
    app.UseStaticFiles();
    app.TryUseSwagger();

    // Add logging for all the following services.
    app.UseSerilogRequestLogging(options =>
    {
    });

    app.UseRouting();
    app.UseCors(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers(); // v1/...
        endpoints.MapHub<ApiHub>("v1/dashboard");
    });
}))
.Build()
.RunCoreHostAsync("RestApi");