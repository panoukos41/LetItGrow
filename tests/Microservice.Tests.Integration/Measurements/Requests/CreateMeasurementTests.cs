using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Measurements.Requests;
using MediatR;
using System;
using System.Threading.Tasks;
using Xunit;
using static MainFixture;

namespace Microservice.Tests.Integration.Measurements.Requests
{
    [Collection(Collections.Main)]
    public class CreateMeasurementTests
    {
        [Fact]
        public void Should_Succeed()
        {
            // Arrange
            var request = new CreateMeasurement
            {
                NodeId = NewId(),
                AirTemperatureC = 32.5,
                AirHumidity = 10.0,
                SoilMoisture = 21.5,
                MeasuredAt = GetNowUtc()
            };

            // Act
            Func<Task<Unit>> act = () => SendAsync(request);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(-21.0, -1.0, -1.0, 3)] // Everything is at wrong values.
        [InlineData(110.0, 10.0, 10.0, 1)] // Temp is too high.
        [InlineData(10.0, 110.0, 10.0, 1)] // Humidity is too high.
        [InlineData(10.0, 10.0, 110.0, 1)] // Moisture is too high.
        public void Should_Fail_Validation(double airTempC, double airHumidity, double soilMoisture, int errorCount)
        {
            // Arrange
            var request = new CreateMeasurement
            {
                NodeId = NewId(),
                AirTemperatureC = airTempC,
                AirHumidity = airHumidity,
                SoilMoisture = soilMoisture,
                MeasuredAt = GetNowUtc()
            };

            // Act
            Func<Task<Unit>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>()
                .Which.Errors.Should().HaveCount(errorCount);
        }
    }
}