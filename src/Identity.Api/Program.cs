using AspNetCore.Identity.CouchDB.Models;
using CouchDB.Driver;
using FluentValidation;
using LetItGrow.CoreHost;
using LetItGrow.Identity;
using LetItGrow.Identity.Common.Behaviours;
using LetItGrow.Identity.Common.Validators;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Reflection;
using static OpenIddict.Abstractions.OpenIddictConstants;

await new HostBuilder()
.ConfigureCoreHost(args)
.ConfigureWebHostDefaults(options => options
.ConfigureServices((c, services) =>
{
    var configuration = c.Configuration;

    services.AddCoreHostServices();
    services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ValidatorExtensions)));
    services.AddMediatR(c => c.AsSingleton(), typeof(Worker).Assembly);
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

    // Configure CouchDb client.
    services.AddSingleton<ICouchClient>(new CouchClient(o => o
        .Configure(configuration)));

    // Configure Identity stores and services.
    services.AddIdentity<CouchDbUser, CouchDbRole>()
        .AddCouchDbStores()
        .SetDatabaseName(configuration.GetCouchDatabase());

    // Configure Identity to use the same JWT claims as OpenIddict instead
    // of the legacy WS-Federation claims it uses by default (ClaimTypes),
    // which saves you from doing the mapping in your authorization controller.
    services.Configure<IdentityOptions>(options =>
    {
        options.ClaimsIdentity.UserNameClaimType = Claims.Name;
        options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
        options.ClaimsIdentity.RoleClaimType = Claims.Role;
    });

    services.AddOpenIddict()
        // Register the OpenIddict core components and to use the CouchDb stores and models.
        .AddCore(options => options
            .UseCouchDb()
            .SetDatabaseName(configuration.GetCouchDatabase()))
        // Register the OpenIddict server components.
        .AddServer(options =>
        {
            // Enable the authorization, logout, token and userinfo endpoints.
            options.SetAuthorizationEndpointUris("/connect/authorize")
                   .SetLogoutEndpointUris("/connect/logout")
                   .SetTokenEndpointUris("/connect/token")
                   .SetUserinfoEndpointUris("/connect/userinfo");

            // Mark the "email", "profile" and "roles" scopes as supported scopes.
            options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

            // Set token lifetime.
            options.SetAccessTokenLifetime(TimeSpan.FromHours(1))
                   .SetIdentityTokenLifetime(TimeSpan.FromHours(24))
                   .SetRefreshTokenLifetime(TimeSpan.FromDays(90));

            // Note: the sample uses the code and refresh token flows but you can enable
            // the other flows if you need to support implicit, password or client credentials.
            options.AllowAuthorizationCodeFlow()
                   .AllowRefreshTokenFlow();

            options.DisableAccessTokenEncryption();

            // Register the signing and encryption credentials.
            options.AddDevelopmentSigningCertificate()
                   .AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String(configuration.GetSecret("jwt"))));

            // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
            options.UseAspNetCore()
                   .EnableAuthorizationEndpointPassthrough()
                   .EnableLogoutEndpointPassthrough()
                   .EnableStatusCodePagesIntegration()
                   .EnableTokenEndpointPassthrough();
        })
        // Register the OpenIddict validation components.
        .AddValidation(options =>
        {
            options.UseLocalServer(); // Import the configuration from the local OpenIddict server instance.
            options.UseAspNetCore();  // Register the ASP.NET Core host.
        });

    services.AddControllersWithViews();
    services.AddRazorPages();

    services.AddWorkers();

    // Register swagger services only if swagger is enabled.
    services.TryAddSwagger("Identity", "1", configuration);
})
.Configure((c, app) =>
{
    var configuration = c.Configuration;
    var enviroment = c.HostingEnvironment;

    if (enviroment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/error");
        app.UseHsts();
    }

    // All the calls after this will always try to redirct to https.
    // For better security modify the Urls in appsettings to use only https endpoints.
    app.UseHttpsRedirection();

    // Enable static files.
    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    // Enable swagger generation and endpoints only when enabled in settings.
    app.TryUseSwagger();

    app.UseSerilogRequestLogging();

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
        endpoints.MapControllers();
        endpoints.MapRazorPages();
        endpoints.MapFallbackToFile("index.html");
    });
}))
.Build()
.RunCoreHostAsync("Identity");