using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Groups.Models;
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
    public class FindGroupTests
    {
        [Fact]
        public async Task Should_Get()
        {
            // Arrange
            var node = await SendAsync(CreateGroup());

            // Act
            var result = await SendAsync(new FindGroup { Id = node.Id });

            // Assert
            result.Should().Be(node);
        }

        [Fact]
        public void Should_Fail_NotFound()
        {
            // Arrange
            // Act
            Func<Task<GroupModel>> act = () => SendAsync(new FindGroup(NewId()));

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
            var result = SendAsync(new FindGroup { Id = id });
            Func<Task<GroupModel>> act = () => SendAsync(new FindGroup(id));

            // Assert
            act.Should().Throw<ValidationException>()
                .Which.Errors.Should().HaveCount(1)
                .And.Contain(x => x.PropertyName == nameof(FindGroup.Id));
        }

        public static readonly List<object[]> FailValidationData = new()
        {
            new object[] { String(12) },
            new object[] { "" }
        };
    }
}