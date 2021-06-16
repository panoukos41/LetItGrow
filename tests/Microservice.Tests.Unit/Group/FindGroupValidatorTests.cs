using Bogus;
using FluentAssertions;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Group.Validators;
using Xunit;

namespace Microservice.Tests.Unit.Group
{
    public class FindGroupValidatorTests
    {
        private static readonly FindGroupValidator Validator = new();

        private static readonly Faker<FindGroup> Faker = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.Id, f => f.Random.Utf16String(11, 11))
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
                .RuleFor(x => x.Id, f => f.Random.Utf16String(0, 10))
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(1);
        }
    }
}