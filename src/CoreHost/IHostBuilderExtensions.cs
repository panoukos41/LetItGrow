using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting
{
    public static partial class IHostBuilderExtensions
    {
        private static string CurrentDir => AppDomain.CurrentDomain.BaseDirectory;

        private static string CurrentEnv(this HostBuilderContext context) =>
            context.HostingEnvironment.EnvironmentName;

        private static IConfigurationBuilder AddYaml(this IConfigurationBuilder builder,
            string path, bool optional = true, bool reload = true) =>
            builder.AddYamlFile(path, optional: optional, reloadOnChange: reload);

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
            .UseContentRoot(Environment.CurrentDirectory)
            // Configure the Core Host.
            .ConfigureHostConfiguration(options => options
                .AddEnvironmentVariables("DOTNET_")
                .AddCommandLine(args))
            // Configure the Core App.
            .ConfigureAppConfiguration((context, options) => options
                // Settings from where app assembly exists.
                .AddYaml($"{CurrentDir}/appsettings.yaml")
                .AddYaml($"{CurrentDir}/appsettings.{context.CurrentEnv()}.yaml")
                // Settings from where app executes.
                .AddYaml($"appsettings.yaml")
                .AddYaml($"appsettings.{context.CurrentEnv()}.yaml")
                .AddEnvironmentVariables()
                .AddCommandLine(args))
            // Set the default logging provider.
            .UseSerilog((context, options) => options
                .ReadFrom.Configuration(context.Configuration, "Serilog"));

        /// <summary>
        /// Run the Core host adding logging before, after and
        /// crash information when the application runs.
        /// </summary>
        /// <param name="host">The host to run.</param>
        /// <param name="name">The name of the application running.</param>
        /// <returns>0 for successful run, 1 for crash.</returns>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types.", Justification = "Log any exception.")]
        public static async Task<int> RunCoreHostAsync(this IHost host, string name)
        {
            try
            {
                Log.Information("Application '{name}' is starting.", name);
                await host.RunAsync();
                Log.Information("Application '{name}' shut down.", name);
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application '{name}' terminated unexpectedly.", name);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}