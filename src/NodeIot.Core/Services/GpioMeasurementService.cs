using Iot.Device.DHTxx;
using LetItGrow.NodeIot.Common;
using System;
using System.Device.Gpio;
using System.Threading;

namespace LetItGrow.NodeIot.Services
{
    public sealed class GpioMeasurementService : IMeasurementService, IDisposable
    {
        public GpioMeasurementService(GpioController gpio)
        {
            int pin = Config.Dht.Pin;
            DhtPin = new Pin(pin, gpio);
            Dht = Config.Dht.Version switch
            {
                11 => new Dht11(pin, gpio),
                12 => new Dht12(pin, gpio),
                21 => new Dht21(pin, gpio),
                22 => new Dht22(pin, gpio),
                _ => throw new ArgumentException("Invalid Dht version")
            };
        }

        public Pin DhtPin { get; }

        public DhtBase Dht { get; }

        public (double TemperatureC, double Humidity, double SoilMoisture) Get()
        {
            double tempC;
            double humidity;
            double moisture;

            while (true)
            {
                if (Dht.TryMeasure(out var measurement))
                {
                    tempC = measurement.Temperature.DegreesCelsius;
                    humidity = measurement.Humidity.Percent;
                    break;
                }
                Thread.Sleep(1000);
            }

            // todo: Read moisture sensor.

            moisture = 50;
            return (tempC, humidity, moisture);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}