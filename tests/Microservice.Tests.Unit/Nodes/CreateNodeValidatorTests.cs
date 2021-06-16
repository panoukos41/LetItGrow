using Bogus;
using FluentAssertions;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.Node.Validators;
using Xunit;

namespace Microservice.Tests.Unit.Node
{
    public class CreateNodeValidatorTests
    {
        private static readonly CreateNodeValidator Validator = new();

        private static readonly Faker<CreateNode> Faker = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.Name, f => f.Name.FindName())
                .RuleFor(x => x.Description, f => f.Random.Utf16String(250, 250))
                .RuleFor(x => x.Type, () => NodeType.Irrigation)
                .RuleFor(x => x.Settings, () => null)
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
                .RuleFor(x => x.Name, f => f.Random.Utf16String(51, 51))
                .RuleFor(x => x.Description, f => f.Random.Utf16String(251, 251))
                .RuleFor(x => x.Type, () => NodeType.Invalid)
                .RuleFor(x => x.Settings, () => null)
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(3);
        }
    }
}