using LetItGrow.NodeIot.Common;
using LetItGrow.NodeIot.Workers.Internal;
using Serilog;
using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using static System.Device.Gpio.PinValue;

namespace LetItGrow.NodeIot.Workers.Internal
{
    /// <summary>
    /// Turn on and off a led at the configured pin every second.
    /// </summary>
    internal class Test2Worker : Worker
    {
        public Test2Worker(GpioController gpio)
        {
            pin = new Pin(Config.Dht.Led, gpio);
            pin.Open();
            pin.Mode = PinMode.Output;
        }

        protected override string Id { get; } = nameof(Test2Worker);

        protected readonly Pin pin;

        protected PinValue value;

        protected override Task WorkAsync(CancellationToken stop)
        {
            return new RepeatedTask(
                stoppingToken: stop,
                period: TimeSpan.FromSeconds(5),
                task: token =>
                {
                    value = value != High;
                    Log.Information("'{Id}', changing pin '{pin}' to '{value}'", Id, pin, value);
                    pin.Value = value;

                    return Task.CompletedTask;
                })
                .Start();
        }

        protected override Task Cleanup()
        {
            pin.Close();
            return Task.CompletedTask;
        }
    }
}