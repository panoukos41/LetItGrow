using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests;
using LetItGrow.Microservice.Logic;
using MediatR;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Nodes.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class DeleteNodeTests
    {
        [Fact]
        public async Task Should_Delete()
        {
            // Arrange
            var node = await SendAsync(new CreateNode { Name = "a", Type = NodeType.Measurement });
            var request = new DeleteNode { Id = node.Id, ConcurrencyStamp = node.ConcurrencyStamp };

            // Act
            Func<Task<Unit>> act = () => SendAsync(request);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData("awesfgawa", "5", 1)] // Id must be 11 characters.
        [InlineData("wesfgaweasg", "", 1)] // Stamp can't be empty (or default).
        [InlineData("wesfgaweasgg", "", 2)] // Id must be 11 characters and Stamp can't be empty (or default).
        public void Should_Fail_Validation(string id, string stamp, int errorCount)
        {
            // Arrange
            var request = new DeleteNode { Id = id, ConcurrencyStamp = stamp };

            // Act
            Func<Task<Unit>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>()
                .And.Errors.Should().HaveCount(errorCount);
        }

        [Fact]
        public void Should_Fail_Not_Found()
        {
            // Arrange
            var request = new DeleteNode { Id = NewId(), ConcurrencyStamp = "1" };

            // Act
            Func<Task<Unit>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.NotFound);
        }

        // todo: create test Should_Fail_Concurrency_Conflict_When_Changed.
        [Fact]
        public async Task Should_Fail_Conflict_When_Changed_Before_Delete()
        {
            // Arrange
            var node = await SendAsync(new CreateNode { Name = "test", Type = NodeType.Irrigation });

            // Act
            await SendAsync(new UpdateNode(node)
            {
                Name = "test01"
            });

            Func<Task<Unit>> act = () => SendAsync(new DeleteNode(node));

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.Conflict);
        }
    }
}