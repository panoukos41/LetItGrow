using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting
{
    public static partial class IHostBuilderExtensions
    {
        /// <summary>
        /// Set the Host settings:<br/>
        /// - AddEnvironmentVariables("DOTNET_")<br/>
        /// - AddCommandLine(args)<br/>
        /// <br/>
        /// Set the App settings:<br/>
        /// - AddYamlFile($"appsettings.yaml", optional: true)<br/>
        /// - AddYamlFile($"appsettings.{EnvironmentName}.yaml", optional: true)<br/>
        /// - AddEnvironmentVariables()<br/>
        /// - AddCommandLine(args)<br/>
        /// <br/>
        /// Set the Logging settings:<br/>
        /// - UseSerilog()<br/>
        /// - ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)<br/>
        /// - ReadFrom.Configuration("Serilog")<br/>
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="args">The application arguments.</param>
        /// <returns>The configured builder.</returns>
        public static IHostBuilder ConfigureCoreHost(this IHostBuilder builder, string[] args) => builder
            // Set default directory for app.
            .UseContentRoot(Directory.GetCurrentDirectory())
            // Configure the Core Host.
            .ConfigureHostConfiguration(options => options
                .AddEnvironmentVariables("DOTNET_")
                .AddCommandLine(args))
            // Configure the Core App.
            .ConfigureAppConfiguration((context, options) => options
                .AddYamlFile($"appsettings.yaml", optional: true, reloadOnChange: true)
                .AddYamlFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.yaml", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args))
            // Set the default logging provider.
            .UseSerilog((context, options) => options
                .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
                .ReadFrom.Configuration(context.Configuration, "Serilog"));

        /// <summary>
        /// Run the Core host adding logging before, after and
        /// crash information when the application runs.
        /// </summary>
        /// <param name="host">The host to run.</param>
        /// <returns>0 for successful run, 1 for crash.</returns>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types.", Justification = "Log any exception.")]
        public static async Task<int> RunCoreHostAsync(this IHost host)
        {
            var conf = host.Services.GetRequiredService<IConfiguration>();
            var name = conf.GetApplicationName();
            var version = conf.GetApplicationVersion();

            try
            {
                Log.Information("Application '{name} {version}' is starting.", name, version);
                await host.RunAsync();
                Log.Information("Application '{name} {version}' shut down.", name, version);
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application '{name} {version}' terminated unexpectedly.", name, version);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}