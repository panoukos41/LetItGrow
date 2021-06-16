using Bogus;
using FluentAssertions;
using LetItGrow.Microservice.NodeAuth.Requests;
using LetItGrow.Microservice.NodeAuth.Validators;
using Xunit;

namespace Microservice.Tests.Unit.NodeAuth
{
    public class DeleteNodeAuthValidatorTests
    {
        private static readonly DeleteNodeAuthValidator Validator = new();

        private static readonly Faker<DeleteNodeAuth> Faker = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.NodeId, f => f.Random.Utf16String(11, 11))
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
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(1);
        }
    }
}