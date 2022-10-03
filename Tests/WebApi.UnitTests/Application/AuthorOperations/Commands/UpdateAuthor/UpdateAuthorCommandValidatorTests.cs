using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Theory]
        [InlineData(0, 1, "sami", "sezg")]
        [InlineData(1, 0, "sami", "sezg")]
        [InlineData(1, 1, "sam", "sezg")]
        [InlineData(0, 1, "sami", "sez")]
        public void WhenInvalidInputsAreGiven_UpdateAuthorValidator_ShouldReturnErrors(int AuthorId, int GenreId, string AuthorName, string AuthorSurname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
            command.AuthorId = AuthorId;
            command.Model = new UpdateAuthorModel()
            {
                Name = AuthorName,
                Surname = AuthorSurname,
                GenreId = GenreId,
                DateOfBirth = new DateTime(2000, 1, 1)
            };
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenInvalidBirthDateIsGiven_UpdateAuthorValidator_ShouldReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                Name="testn",
                Surname="testS",
                DateOfBirth = DateTime.Now
            };
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_UpdateAuthorValidator_ShouldNotReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                Name="test",
                Surname="testS",
                GenreId=2,
                DateOfBirth = DateTime.Now.AddYears(-4)
            };
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}