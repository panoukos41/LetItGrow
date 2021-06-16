using Bogus;
using FluentAssertions;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Group.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Unit.Group
{
    public class DeleteGroupValidatorTests
    {
        private static readonly DeleteGroupValidator Validator = new();

        private static readonly Faker<DeleteGroup> Faker = new();

        [Fact]
        public void Should_Pass_Validation()
        {
            // Arrange
            var request = Faker
                .RuleFor(x => x.Id, f => f.Random.Utf16String(11, 11))
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
                .RuleFor(x => x.ConcurrencyStamp, () => string.Empty)
                .Generate();

            // Act
            var validation = Validator.Validate(request);

            // Assert
            validation.Errors.Count.Should().Be(2);
        }
    }
}
