using LetItGrow.NodeIot.Common;
using LetItGrow.NodeIot.Workers.Internal;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.NodeIot.Workers.Internal
{
    /// <summary>
    /// Print config pin every 2 seconds.
    /// </summary>
    internal class Test1Worker : Worker
    {
        protected override string Id { get; } = nameof(Test1Worker);

        protected override Task WorkAsync(CancellationToken stop)
        {
            int pin = Config.Led.Power;

            return new RepeatedTask(
                token =>
                {
                    Log.Information("Led Pin = {pin}", pin);
                    return Task.CompletedTask;
                },
                period: TimeSpan.FromSeconds(5),
                stoppingToken: stop)
                .Start();
        }
    }
}