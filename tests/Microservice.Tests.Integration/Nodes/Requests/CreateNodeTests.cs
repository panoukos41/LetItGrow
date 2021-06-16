using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Nodes.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class CreateNodeTests
    {
        [Fact]
        public async void Should_Create()
        {
            var name = "Noice";
            var desc = "A Noice Descriptionn";
            var settings = new IrrigationSettings { PollInterval = 60 }.ToJsonDocument();

            // Arrange
            CreateNode request = new()
            {
                Name = name,
                Description = desc,
                Type = NodeType.Irrigation,
                Settings = settings
            };

            // Act
            NodeModel response = await SendAsync(request);

            // Assert
            response.Id.Length.Should().Be(11);
            response.ConcurrencyStamp.Should().NotBeEmpty();
            response.Type.Should().Be(NodeType.Irrigation);

            response.CreatedAt.Should().NotBe(default);
            response.UpdatedAt.Should().NotBe(default);

            response.CreatedBy.Should().NotBeEmpty();
            response.UpdatedBy.Should().NotBeEmpty();

            response.Name.Should().Be(name);
            response.Description.Should().Be(desc);
            response.Settings!.RootElement.GetRawText().Should().Be(settings.RootElement.GetRawText());

            response.GroupId.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Should_Fail_Validation(string name, string desc, int pollInterval, int errorCount)
        {
            // Arrange
            CreateNode create = new()
            {
                Name = name,
                Description = desc,
                Type = NodeType.Measurement,
                Settings = new MeasurementSettings { PollInterval = pollInterval }.ToJsonDocument()
            };

            // Act
            Func<Task<NodeModel>> act = () => SendAsync(create);

            // Assert
            act.Should().Throw<ValidationException>()
                .And.Errors.Should().HaveCount(errorCount);
        }

        public static readonly List<object[]> TestData = new()
        {
            //           NAME    DESC    POLLINTERVAL    ERRORCOUNTE
            new object[] { String(51), String(251), 21601, 3 },
            new object[] { "", "", 59, 2 },
            new object[] { "", "", 600, 1 },
            new object[] { "a", "", 59, 1 },
        };
    }
}