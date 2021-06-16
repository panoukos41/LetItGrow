using FluentAssertions;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Validators;
using Xunit;

namespace Microservice.Tests.Unit.Node
{
    public class MeasurementSettingsValidatorTests
    {
        private static readonly MeasurementSettingsValidator Validator = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var settings = new MeasurementSettings();

            // Act
            var validation = Validator.Validate(settings);

            // Assert
            validation.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(59)]
        [InlineData(3601)]
        public void Should_Fail_Validation(int pollInterval)
        {
            // Arrange
            var settings = new MeasurementSettings(pollInterval);

            // Act
            var validation = Validator.Validate(settings);

            // Assert
            validation.Errors.Count.Should().Be(1);
        }
    }
}