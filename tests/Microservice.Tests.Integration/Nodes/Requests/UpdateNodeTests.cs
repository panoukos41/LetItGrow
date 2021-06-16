using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Common;
using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests;
using LetItGrow.Microservice.Logic;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Nodes.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class UpdateNodeTests
    {
        [Fact]
        public async Task Should_Update()
        {
            // Arrange
            var newName = "test01";
            var newDesc = "Desc";
            var newSettings = new IrrigationSettings { PollInterval = 60 };

            var node = await SendAsync(CreateIrrigationNode());
            var request = new UpdateNode(node)
            {
                Name = newName,
                Description = newDesc,
                Settings = newSettings.ToJsonDocument()
            };

            // Act
            await SendAsync(request);
            var updatedNode = await SendAsync(new FindNode(node.Id));

            // Assert
            updatedNode.Name.Should().Be(newName);
            updatedNode.Description.Should().Be(newDesc);
            updatedNode.Settings.RootElement.GetRawText().Should().Be(newSettings.ToJsonDocument().RootElement.GetRawText());
        }

        [Fact]
        public void Should_Fail_Not_Found()
        {
            // Arrange
            var request = new UpdateNode { Id = NewId(), ConcurrencyStamp = "1", Type = NodeType.Irrigation };

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.NotFound);
        }

        [Fact]
        public async Task Should_Fail_Conflict_When_Changed_Second_Time()
        {
            // Arrange
            var newName = "test01";
            var newDesc = "Desc";
            var newSettings = new IrrigationSettings { PollInterval = 60 };

            var node = await SendAsync(CreateIrrigationNode());
            var request = new UpdateNode(node)
            {
                Name = newName,
                Description = newDesc,
                Settings = newSettings.ToJsonDocument()
            };

            // Act
            await SendAsync(request);
            Func<Task<ModelUpdate>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.Conflict);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Should_Fail_Validation(string id, string stamp, string name, string desc, int pollInterval, int errorCount)
        {
            // Arrange
            var request = new UpdateNode
            {
                Id = id,
                ConcurrencyStamp = stamp,
                Type = NodeType.Irrigation,
                Name = name,
                Description = desc,
                Settings = new IrrigationSettings { PollInterval = pollInterval }.ToJsonDocument()
            };

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>()
                .And.Errors.Should().HaveCount(errorCount);
        }

        public static readonly List<object[]> TestData = new()
        {
            //           ID    STAMP    NAME    DESC    POLLINTERVAL    ERRORCOUNT
            new object[] { String(12), String(0), String(51), String(251), 21601, 5 },
            new object[] { String(00), String(0), String(00), String(000), 59, 4 },
            new object[] { String(11), String(0), String(01), String(000), 60, 1 },
            new object[] { String(11), String(1), String(01), String(000), 59, 1 },
            new object[] { String(11), String(1), String(00), String(000), 600, 1 },
        };
    }
}