using FluentAssertions;
using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Nodes.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class FindNodesTests
    {
        [Fact]
        public async Task Should_Get_All()
        {
            // Arrange
            await SendAsync(CreateIrrigationNode(settings: new() { PollInterval = 400 }));
            await SendAsync(CreateMeasurementNode(settings: new() { PollInterval = 400 }));

            // Act
            NodeModel[] result = null!;
            Func<Task<NodeModel[]>> act = async () => result = await SendAsync(new FindNodes());

            // Assert
            await act.Should().NotThrowAsync();
            result.Should().NotBeNull()
                .And.HaveCountGreaterThan(2);
        }

        //[Theory]
        //[MemberData(nameof(ValidationFailData))]
        //public void Should_Fail_Validation(string type, int errorCount)
        //{
        //    // Arrange
        //    // Act
        //    var result = SendAsync(new FindNodes
        //    {
        //        Type = type
        //    });

        //    // Assert
        //    var ex = result.ShouldThrow<ValidationException>();
        //    ex.Errors.Count().ShouldBe(errorCount);
        //}

        //public static readonly List<object[]> ValidationFailData = new()
        //{
        //    //           TYPE
        //    new object[] { "", 1 },
        //    new object[] { "a", 1 },
        //};
    }
}