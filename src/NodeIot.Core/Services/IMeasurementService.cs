namespace LetItGrow.NodeIot.Services
{
    public interface IMeasurementService
    {
        /// <summary>
        /// Gets a tuple with three elements, the temperature in Celsius,
        /// the Humidity and the Moisture of the soil.
        /// </summary>
        /// <returns>A tuple with three elements, the temperature in Celsius,
        /// the Humidity and the Moisture of the soil.</returns>
        (double TemperatureC, double Humidity, double SoilMoisture) Get();
    }
}