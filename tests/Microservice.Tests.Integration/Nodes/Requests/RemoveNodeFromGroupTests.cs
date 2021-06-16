using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Common;
using LetItGrow.Microservice.Data.Nodes.Requests;
using LetItGrow.Microservice.Logic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Nodes.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class RemoveNodeFromGroupTests
    {
        [Fact]
        public async Task Should_Remove()
        {
            // Arrange
            var node = await SendAsync(CreateMeasurementNode());
            var group = await SendAsync(CreateGroup());

            // Act
            var update = await SendAsync(AddNodeToGroup(node, group));
            node = node with { ConcurrencyStamp = update.ConcurrencyStamp };

            await SendAsync(new RemoveNodeFromGroup
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp
            });

            node = await SendAsync(new FindNode(node.Id));

            // Assert
            node.GroupId.Should().BeNull();
        }

        [Fact]
        public void Should_Fail_Node_NotFound()
        {
            // Arrange
            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(new RemoveNodeFromGroup
            {
                Id = NewId(),
                ConcurrencyStamp = "1"
            });

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.NotFound);
        }

        [Fact]
        public async Task Should_Fail_Conflict()
        {
            // Arrange
            var node = await SendAsync(CreateMeasurementNode());

            // Act
            await SendAsync(UpdateNode(node, "new-test-name"));

            Func<Task<ModelUpdate>> act = () => SendAsync(new RemoveNodeFromGroup
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp
            });

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.Conflict);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Should_Fail_Validation(string id, string stamp, int errorCount)
        {
            // Arrange
            var request = new RemoveNodeFromGroup
            {
                Id = id,
                ConcurrencyStamp = stamp
            };

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>()
                .And.Errors.Should().HaveCount(errorCount);
        }

        public static readonly List<object[]> TestData = new()
        {
            ///          ID    STAMP    ERRORCOUNT
            new object[] { String(12), String(0), 2 },
            new object[] { String(11), String(0), 1 },
            new object[] { String(05), String(1), 1 },
        };
    }
}