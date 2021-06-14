using LetItGrow.NodeIot.Common;
using System;
using System.Device.Gpio;

namespace LetItGrow.NodeIot.Services
{
    public sealed class IrrigationService : IIrrigationService, IDisposable
    {
        public IrrigationService(GpioController gpio)
        {
            RelayPin = new Pin(Config.Pump.Pin, gpio);
            RelayPin.Open();
            RelayPin.Mode = PinMode.Output;
        }

        public Pin RelayPin { get; }

        public void Set(bool state)
        {
            RelayPin.Value = state;
        }

        public void Dispose()
        {
            RelayPin.Close();
        }
    }
}