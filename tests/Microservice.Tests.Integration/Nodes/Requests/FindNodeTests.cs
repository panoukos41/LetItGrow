using FluentAssertions;
using FluentValidation;
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
    public class FindNodeTests
    {
        [Fact]
        public async Task Should_Find_Node()
        {
            // Arrange
            var settings = new IrrigationSettings { PollInterval = 100 };
            var request = CreateIrrigationNode(settings: settings);

            // Act
            var result = await SendAsync(request);
            var node = await SendAsync(new FindNode(result.Id));

            // Assert
            node.Id.Should().Be(result.Id);
            node.Type.Should().Be(NodeType.Irrigation);
            node.Settings!.RootElement.GetRawText().Should().Be(settings.ToJsonDocument().RootElement.GetRawText());
        }

        [Fact]
        public void Should_Fail_NotFound()
        {
            // Arrange
            var request = new FindNode { Id = "awepgslwaxm" };

            // Act
            Func<Task<NodeModel>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.NotFound);
        }

        [Theory]
        [MemberData(nameof(FailValidationData))]
        public void Should_Fail_Validation(string id)
        {
            // Arrange
            // Act
            Func<Task<NodeModel>> act = () => SendAsync(new FindNode(id));

            // Assert
            act.Should().Throw<ValidationException>()
                .And.Errors.Should().HaveCount(1)
                .And.Contain(x => x.PropertyName == nameof(FindNode.Id));
        }

        public static readonly List<object[]> FailValidationData = new()
        {
            new object[] { String(12) },
            new object[] { String(01) }
        };
    }
}