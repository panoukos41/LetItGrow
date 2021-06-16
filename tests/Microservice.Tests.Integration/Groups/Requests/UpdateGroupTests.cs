using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Common;
using LetItGrow.Microservice.Data.Groups.Requests;
using LetItGrow.Microservice.Logic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Groups.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class UpdateGroupTests
    {
        [Fact]
        public async Task Should_Update()
        {
            // Arrange
            var newName = "test01";
            var group = await SendAsync(CreateGroup());
            var request = UpdateGroup(group, newName);

            // Act
            await SendAsync(request);

            // Assert
            var updatedGroup = await SendAsync(new FindGroup { Id = group.Id });

            updatedGroup.Name.Should().Be(newName);
            updatedGroup.Description.Should().BeNull();
        }

        [Fact]
        public void Should_Fail_Not_Found()
        {
            // Arrange
            var request = new UpdateGroup(NewId(), "1");

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
            var group = await SendAsync(CreateGroup());
            var request = UpdateGroup(group, newName);
            await SendAsync(request);

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ErrorException>()
                .And.Error.Should().Be(Errors.Conflict);
        }

        [Theory]
        [MemberData(nameof(FailValidationData), DisableDiscoveryEnumeration = true)]
        public void Should_Fail_Validation(string id, string stamp, string name, string desc, int errorCount)
        {
            // Arrange
            var request = new UpdateGroup
            {
                Id = id,
                ConcurrencyStamp = stamp,
                Name = name,
                Description = desc
            };

            // Act
            Func<Task<ModelUpdate>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>()
                .Which.Errors.Should().HaveCount(errorCount);
        }

        public static readonly List<object[]> FailValidationData = new()
        {
            //           ID    STAMP    NAME    DESC    ERRORCOUNT
            new object[] { String(12), "", String(51), String(251), 4 },
            new object[] { "", "", "", "", 3 },
            new object[] { String(11), "", "a", "", 1 },
            new object[] { String(11), "1", "", "", 1 },
        };
    }
}