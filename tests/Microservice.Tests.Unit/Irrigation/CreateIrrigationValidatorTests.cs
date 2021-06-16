using Bogus;
using FluentAssertions;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Irrigation.Validators;
using System;
using Xunit;

namespace Microservice.Tests.Unit.Irrigation
{
    public class CreateIrrigationValidatorTests
    {
        private static readonly CreateIrrigationValidator Validator = new();

        private static readonly Faker<CreateIrrigation> Faker = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.NodeId, f => f.Random.Utf16String(11, 11))
                .RuleFor(x => x.Type, () => IrrigationType.Start)
                .RuleFor(x => x.IssuedAt, () => DateTimeOffset.UtcNow)
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void Should_Fail_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.NodeId, f => f.Random.Utf16String(0, 10))
                .RuleFor(x => x.Type, () => IrrigationType.Invalid)
                .RuleFor(x => x.IssuedAt, () => default)
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(3);
        }
    }
}