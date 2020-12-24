using LetItGrow.Identity;
using LetItGrow.Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

#pragma warning disable CA1050 // Declare types in namespaces

// We build our server this way so that we can use
// ef core tools if we want/need to.
public static class Program
{
    public static Task<int> Main(string[] args) =>
        CreateHostBuilder(args).Build().RunCoreHostAsync();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        new HostBuilder()
        .ConfigureCoreHost(args)
        .ConfigureWebHostDefaults(options => options
            .UseStartup<Startup>());
}

// This class is used to configure services and the asp net app.
public class Startup
{
    public Startup(IConfiguration configuration) =>
        Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure the Ef Core Database context.
        services.AddDbContext<AppDbContext>(options =>
        {
            (AppDbContext.Connecion, AppDbContext.Schema) = Configuration.GetDatabase();

            options.UseNpgsql(AppDbContext.Connecion, o => o
                   .MigrationsHistoryTable("__EFMigrationsHistory", AppDbContext.Schema));

            options.UseOpenIddict();
        });

        // Configure Identity stores and services.
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

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
            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                options.UseEntityFrameworkCore()
                       .UseDbContext<AppDbContext>();
            })
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
                options.SetAccessTokenLifetime(TimeSpan.FromHours(1));
                options.SetIdentityTokenLifetime(TimeSpan.FromHours(24));

                // Note: the sample uses the code and refresh token flows but you can enable
                // the other flows if you need to support implicit, password or client credentials.
                options.AllowAuthorizationCodeFlow()
                       .AllowRefreshTokenFlow();

                options.DisableAccessTokenEncryption();

                // Register the signing and encryption credentials.
                options.AddDevelopmentSigningCertificate();
                
                options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String(Configuration.GetSecret("jwt"))));

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
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });

        services.AddControllersWithViews();
        services.AddRazorPages();

        services.AddHostedService<Worker>();

        // Register swagger services only if swagger is enabled.
        services.TryAddSwagger(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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
        app.UseStaticFiles();

        // Enable swagger generation and endpoints only when enabled in settings.
        app.TryUseSwagger(Configuration);

        app.UseSerilogRequestLogging();

        app.UseRouting();
        app.UseCors(builder =>
        {
            builder.WithOrigins(Configuration.GetService("blazor").ToString().TrimEnd('/'));
            builder.WithMethods("GET");
            builder.WithHeaders("Authorization");
        });
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(options =>
        {
            options.MapRazorPages();
            options.MapControllers();
        });
    }
}