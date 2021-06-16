using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Irrigations.Models;
using LetItGrow.Microservice.Data.Irrigations.Requests;
using MediatR;
using System;
using System.Threading.Tasks;
using Xunit;
using static MainFixture;

namespace Microservice.Tests.Integration.Irrigations.Requests
{
    [Collection(Collections.Main)]
    public class CreateIrrigationTests
    {
        [Fact]
        public void Should_Succeed()
        {
            // Arrange
            var request = new CreateIrrigation
            {
                NodeId = NewId(),
                Type = IrrigationType.Start,
                IssuedAt = GetNowUtc()
            };

            // Act
            Func<Task<Unit>> act = () => SendAsync(request);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        public void Should_Fail_Validation(int type)
        {
            // Arrange
            var request = new CreateIrrigation
            {
                NodeId = NewId(),
                Type = (IrrigationType)type,
                IssuedAt = GetNowUtc()
            };

            // Act
            Func<Task<Unit>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>()
                .And.Errors.Should().HaveCount(1)
                .And.Contain(x => x.PropertyName == nameof(CreateIrrigation.Type));
        }
    }
}