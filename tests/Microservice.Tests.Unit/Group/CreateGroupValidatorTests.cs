using Bogus;
using FluentAssertions;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Group.Validators;
using Xunit;

namespace Microservice.Tests.Unit.Group
{
    public class CreateGroupValidatorTests
    {
        private static readonly CreateGroupValidator Validator = new();

        private static readonly Faker<CreateGroup> Faker = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.Name, f => f.Name.FindName())
                .RuleFor(x => x.Description, f => f.Random.Utf16String(250, 250))
                .RuleFor(x => x.Type, () => GroupType.None)
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
                .RuleFor(x => x.Type, () => GroupType.None)
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(2);
        }
    }
}