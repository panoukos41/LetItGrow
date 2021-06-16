using FluentAssertions;
using LetItGrow.Microservice.Data.Groups.Models;
using LetItGrow.Microservice.Data.Groups.Requests;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Groups.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class FindGroupsTests
    {
        // todo: Node groups tests.
        [Fact]
        public async Task Should_Get_All()
        {
            // Arrange
            await SendAsync(CreateGroup());
            await SendAsync(CreateGroup());

            // Act
            GroupModel[] result = null!;
            Func<Task<GroupModel[]>> act = async () => result = await SendAsync(new FindGroups());

            // Assert
            await act.Should().NotThrowAsync();
            result.Should().NotBeNull()
                .And.HaveCountGreaterThan(2);
        }
    }
}