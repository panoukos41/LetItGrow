using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CA1031 // Do not catch general exception types but

namespace LetItGrow.NodeIot.Workers.Internal
{
    /// <summary>
    /// A base class to create workers. A worker
    /// runs all the time until the application is stopped.
    /// If the worker fails with an exception the it is restarted.
    /// </summary>
    public abstract class Worker : BackgroundService
    {
        /// <summary>
        /// The workers unique identity eg: nameof(worker).
        /// </summary>
        protected abstract string Id { get; }

        /// <summary>
        /// A task that executes before WorkAsync to perform initialization logic.
        /// </summary>
        /// <param name="stoppingToken">A token that marks the application is stopping.</param>
        public virtual Task Initialize(CancellationToken stoppingToken) =>
            Task.CompletedTask;

        /// <summary>
        /// Exexcute work. This will be called until the application stops.
        /// </summary>
        /// <param name="stoppingToken">A token that marks the application is stopping.</param>
        /// <returns>A <see cref="Task"/> that represents the long running opperation.</returns>
        protected abstract Task WorkAsync(CancellationToken stoppingToken);

        /// <summary>
        /// A task that executes to cleanup resources before the application shutsdown.
        /// </summary>
        protected virtual Task Cleanup() =>
            Task.CompletedTask;

        /// <inheritdoc/>
        protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await Task.Delay(1500, stoppingToken).Ignore().ConfigureAwait(false);

            try
            {
                await Initialize(stoppingToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "'{Id}' Initialization '{status}', worker will be ignored", Id, Status.Failed);
                return;
            }

            Log.Information("'{Id}' '{status}'", Id, Status.Started);
            while (stoppingToken.IsCancellationRequested is false)
            {
                try
                {
                    await WorkAsync(stoppingToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "'{Id}' '{status}'", Id, Status.Failed);
                    Log.Information("'{Id}' '{status}'", Id, Status.Restarting);

                    await Task.Delay(500, stoppingToken).Ignore().ConfigureAwait(false);
                }
            }

            await Cleanup()
                .Ignore(ex => Log.Warning(ex, "'{Id}' Cleanup '{status}'", Id, Status.Failed))
                .ConfigureAwait(false);

            Log.Information("'{Id}' '{status}'", Id, Status.Stopped);
        }

        private class Status
        {
            public const string Started = "started";
            public const string Stopped = "stopped";
            public const string Failed = "failed";
            public const string Restarting = "restarting";
        }
    }
}