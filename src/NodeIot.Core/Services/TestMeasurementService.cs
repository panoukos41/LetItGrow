using Serilog;
using System;

namespace LetItGrow.NodeIot.Services
{
    public sealed class TestMeasurementService : IMeasurementService
    {
        private static readonly Random random = new();

        public (double TemperatureC, double Humidity, double SoilMoisture) Get()
        {
            double tempC = 30.0;
            double humidity = 50.0;
            double soilMoisture = random.Next(1, 3) % 2 == 0 ? 45.0 : 55.0;

            Log.Information("Test Measurement returns tempC: '{temp}', humidity: '{humidity}' and soilMoisture: '{moisture}'", tempC, humidity, soilMoisture);

            return (tempC, humidity, soilMoisture);
        }
    }
}