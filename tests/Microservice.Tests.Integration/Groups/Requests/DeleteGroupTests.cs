using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Common;
using LetItGrow.Microservice.Data.Groups.Requests;
using LetItGrow.Microservice.Logic;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Groups.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class DeleteGroupTests
    {
        [Fact]
        public async Task Should_Delete()
        {
            // Arrange
            var group = await SendAsync(CreateGroup());

            // Act
            Func<Task<Unit>> act = () => SendAsync(new DeleteGroup(group));

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Should_Fail_NotFound()
        {
            // Arrange
            // Act
            Func<Task<Unit>> act = () => SendAsync(new DeleteGroup(NewId(), "1"));

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.NotFound);
        }

        [Fact]
        public async Task Should_Fail_Confict_When_Changed_Before_Delete()
        {
            // Arrange
            var group = await SendAsync(CreateGroup());
            var request = UpdateGroup(group, "new-test-name");
            await SendAsync(request);

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.Conflict);
        }

        [Theory]
        [MemberData(nameof(FailValidationData))]
        public void Should_Fail_Validation(string id, string stamp, int errorCount)
        {
            // Arrange
            // Act
            Func<Task<Unit>> act = () => SendAsync(new DeleteGroup(id, stamp));

            // Assert
            act.Should().Throw<ValidationException>()
                .Which.Errors.Should().HaveCount(errorCount);
        }

        public static readonly List<object[]> FailValidationData = new()
        {
            //           ID    STAMP    ERRORCOUNT
            new object[] { String(12), "", 2 },
            new object[] { String(11), "", 1 },
            new object[] { String(00), "1", 1 },
        };
    }
}