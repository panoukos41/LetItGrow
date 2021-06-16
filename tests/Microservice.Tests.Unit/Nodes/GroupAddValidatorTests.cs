using Bogus;
using FluentAssertions;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.Node.Validators;
using Xunit;

namespace Microservice.Tests.Unit.Node
{
    public class GroupAddValidatorTests
    {
        private static readonly GroupAddValidator Validator = new();

        private static readonly Faker<GroupAdd> Faker = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.Id, f => f.Random.Utf16String(11, 11))
                .RuleFor(x => x.GroupId, f => f.Random.Utf16String(11, 11))
                .RuleFor(x => x.ConcurrencyStamp, f => f.Random.Utf16String(1, 40))
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
                .RuleFor(x => x.GroupId, f => f.Random.Utf16String(11, 20))
                .RuleFor(x => x.ConcurrencyStamp, () => string.Empty)
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(3);
        }
    }
}