
using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Theory]
        [InlineData("a", "sam", 1)]
        [InlineData("sam", "a", 1)]
        [InlineData("asa", "sam", -1)]
        [InlineData("a", "m", 1)]
        public void WhenInvalidNameSurnameAndGenreIdIsGiven_CreateAuthorCommandValidator_ShouldReturnErrors(string TestAuthorName, string TestAuthorSurname, int TestGenreId)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = TestAuthorName,
                Surname = TestAuthorSurname,
                DateOfBirth = new DateTime(2000, 1, 6),
                GenreId = TestGenreId
            };
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenInvalidDateTimeIsGiven_CreateAuthorCommandValidator_ShouldReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "TestAuthorName",
                Surname = "TestAuthorSurname",
                //DateOfBirth = new DateTime(2000, 1, 6),
                GenreId = 1
            };
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidModelIsGiven_CreateAuthorCommandValidator_ShouldNotReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "TestAuthorName",
                Surname = "TestAuthorSurname",
                DateOfBirth = new DateTime(2000, 1, 6),
                GenreId = 1
            };
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}