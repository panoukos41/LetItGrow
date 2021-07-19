using LetItGrow.NodeIot.Common;
using LetItGrow.NodeIot.Services;
using LetItGrow.NodeIot.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CA1031 // Do not catch general exception types

namespace LetItGrow.NodeIot
{
    public sealed class App
    {
        private readonly IHost host;

        private App(IHost host) => this.host = host;

        /// <summary>
        /// Initializes a new host that:<br/>
        /// <br/>
        /// - Loads configuration from 'appsettings.yaml' files.<br/>
        /// - Configures Logging using Serilog(<see cref="Serilog.Log"/>).<br/>
        /// - Configures Base services.<br/>
        /// - Configures Worker services.<br/>
        /// - Sets the <see cref="Config.Configuration"/> to the host configuration.<br/>
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <param name="configureLogging">Action to further configure serilog.</param>
        /// <param name="configureAppConfig">Action to further configure app configuration.</param>
        /// <param name="configureServices">Action to further configure app services.</param>
        /// <returns>An initialized host.</returns>
        /// <remarks>
        /// ConfigureLogging Action is run after the default configuration has taken place.<br/>
        /// ConfigureAppConfig Action is run after the files have been loaded but before env
        /// variables and command line arguments are loaded.<br/>
        /// configureServices Action is run after all default services have been added including worker services.
        /// </remarks>
        public static App Create(string[] args,
            Action<HostBuilderContext, LoggerConfiguration>? configureLogging = null,
            Action<HostBuilderContext, IConfigurationBuilder>? configureAppConfig = null,
            Action<HostBuilderContext, IServiceCollection>? configureServices = null)
        {
            var host = new HostBuilder()
                .UseSerilog((c, b) =>
                {
                    b.MinimumLevel.Information();
                    b.WriteTo.Console();
                    b.WriteTo.File(path: $"{c.RootPath()}/logs/log-.txt", rollingInterval: RollingInterval.Day);
                    configureLogging?.Invoke(c, b);
                })
                .ConfigureAppConfiguration((c, b) =>
                {
                    b.AddYamlFile($"appsettings.yaml", optional: false, reloadOnChange: false);
                    b.AddYamlFile($"{c.CurrentPath()}/appsettings.yaml", optional: true, reloadOnChange: false);
#if DEBUG
                    b.AddYamlFile($"appsettings.dev.yaml", optional: true, reloadOnChange: false);
#else
                    b.AddYamlFile($"appsettings.prod.yaml", optional: true, reloadOnChange: false);
#endif
                    configureAppConfig?.Invoke(c, b);

                    b.AddEnvironmentVariables();
                    b.AddCommandLine(args);
                })
                .ConfigureServices((c, services) =>
                {
                    // Make configuration available to the app through static class.
                    Config.Configuration = c.Configuration;

                    services.AddSingleton<GpioController>();
                    services.AddMqtt();

                    switch (Config.Type)
                    {
                        case "irrigation":
                            if (Config.Test)
                                services.AddSingleton<IIrrigationService, TestIrrigationService>();
                            else
                                services.AddSingleton<IIrrigationService, GpioIrrigationService>();
                            services.AddHostedService<IrrigationWorker>();
                            break;

                        case "measurement":
                            if (Config.Test)
                                services.AddSingleton<IMeasurementService, TestMeasurementService>();
                            else
                                services.AddSingleton<IMeasurementService, GpioMeasurementService>();
                            services.AddHostedService<MeasurementWorker>();
                            break;
                    }

                    configureServices?.Invoke(c, services);
                })
                .Build();

            return new App(host);
        }

        /// <summary>
        /// Runs the provided host and completes only when the token is triggered.
        /// </summary>
        /// <param name="token">A token to trigger shutdown.</param>
        /// <returns>The <see cref="Task"/> that represents asynchronous operation.</returns>
        /// <remarks>If you only need the services and the config you don't have to use this method.</remarks>
        public async Task<int> RunAsync(CancellationToken token = default)
        {
            var power = new Pin(Config.Led.Power, host.GetRequiredService<GpioController>());
            power.Open(PinMode.Output);
            try
            {
                Log.Information("Node '{type}' with Id '{id}' is starting.", Config.Type, Config.ClientId);
                power.Value = PinValue.High;
                await host.RunAsync(token);
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Node '{type}' with Id '{id}' terminated unexpectedly.", Config.Type, Config.ClientId);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}