using Bogus;
using FluentAssertions;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Measurement.Validators;
using System;
using Xunit;

namespace Microservice.Tests.Unit.Measurement
{
    public class CreateMeasurementValidatorTests
    {
        private static readonly CreateMeasurementValidator Validator = new();

        private static readonly Faker<CreateMeasurement> Faker = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.NodeId, f => f.Random.Utf16String(11, 11))
                .RuleFor(x => x.MeasuredAt, f => DateTimeOffset.UtcNow)
                .RuleFor(x => x.AirTemperatureC, f => f.Random.Double(-20, 50))
                .RuleFor(x => x.AirHumidity, f => f.Random.Double(0, 100))
                .RuleFor(x => x.SoilMoisture, f => f.Random.Double(0, 100))
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(-21.00, -0.01, -0.01)]
        [InlineData(50.01, 100.01, 100.01)]
        public void Should_Fail_Validation(double airTemperatureC, double airHumidity, double soilMoisture)
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.NodeId, f => f.Random.Utf16String(0, 10))
                .RuleFor(x => x.MeasuredAt, f => default)
                .RuleFor(x => x.AirTemperatureC, () => airTemperatureC)
                .RuleFor(x => x.AirHumidity, () => airHumidity)
                .RuleFor(x => x.SoilMoisture, () => soilMoisture)
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(5);
        }
    }
}