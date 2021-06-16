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
    public class AddNodeToGroupTests
    {
        [Fact]
        public async Task Should_Add()
        {
            // Arrange
            var node = await SendAsync(CreateIrrigationNode());
            var group = await SendAsync(CreateGroup());

            // Act
            await SendAsync(new AddNodeToGroup
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp,
                GroupId = group.Id
            });

            node = await SendAsync(new FindNode(node.Id));

            // Assert
            node.GroupId.Should().Be(group.Id);
        }

        [Fact]
        public async Task Should_Fail_Node_NotFound()
        {
            // Arrange
            var group = await SendAsync(CreateGroup());

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(new AddNodeToGroup
            {
                Id = NewId(),
                ConcurrencyStamp = "1",
                GroupId = group.Id
            });

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.NotFound);
        }

        [Fact]
        public async Task Should_Fail_Group_NotFound()
        {
            // Arrange
            var node = await SendAsync(CreateIrrigationNode());

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(new AddNodeToGroup
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp,
                GroupId = NewId()
            });

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.NotFound);
        }

        [Fact]
        public async Task Should_Fail_Conflict()
        {
            // Arrange
            var node = await SendAsync(CreateIrrigationNode());
            var group = await SendAsync(CreateGroup());

            // Act
            await SendAsync(UpdateNode(node, "new-test-name"));

            Func<Task<ModelUpdate>> act = () => SendAsync(new AddNodeToGroup
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp,
                GroupId = group.Id
            });

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.Conflict);
        }

        [Theory]
        [MemberData(nameof(FailValidationData))]
        public void Should_Fail_Validation(string id, string stamp, string groupId, int errorCount)
        {
            // Arrange
            var request = new AddNodeToGroup
            {
                Id = id,
                ConcurrencyStamp = stamp,
                GroupId = groupId
            };

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>()
                .And.Errors.Should().HaveCount(errorCount);
        }

        public static readonly List<object[]> FailValidationData = new()
        {
            ///          ID    STAMP    GROUPID    ERRORCOUNT
            new object[] { String(12), String(0), String(12), 3 },
            new object[] { String(11), String(1), String(00), 1 },
            new object[] { String(00), String(1), String(11), 1 },
            new object[] { String(01), String(1), String(01), 2 }
        };
    }
}