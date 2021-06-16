using FluentAssertions;
using FluentValidation;
using LetItGrow.Microservice.Data.Groups.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microservice.Tests.Integration.Groups.Requests
{
    using static MainFixture;

    [Collection(Collections.Main)]
    public class CreateGroupTests : IClassFixture<MainFixture>
    {
        [Fact]
        public async void Should_Create()
        {
            // Arrange
            var name = "grooup";

            // Act
            var result = await SendAsync(CreateGroup(name));

            // Assert
            result.Id.Length.Should().Be(11);
            result.ConcurrencyStamp.Should().NotBeEmpty();

            result.CreatedAt.Should().NotBe(default);
            result.UpdatedAt.Should().NotBe(default);

            result.CreatedBy.Should().NotBeEmpty();
            result.UpdatedBy.Should().NotBeEmpty();

            result.Name.Should().Be(name);
            result.Description.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Should_Fail_Validation(string name, string desc, int errorCount)
        {
            // Arrange
            var request = CreateGroup(name, desc);

            // Act
            Func<Task<GroupModel>> act = () => SendAsync(request);

            // Assert
            act.Should().Throw<ValidationException>()
               .And.Errors.Count().Should().Be(errorCount);
        }

        public static readonly List<object[]> TestData = new()
        {
            //           NAME    DESCRIPTION    ERRORCOUNT
            new object[] { String(51), String(251), 2 },
            new object[] { "", "", 1 }
        };
    }
}