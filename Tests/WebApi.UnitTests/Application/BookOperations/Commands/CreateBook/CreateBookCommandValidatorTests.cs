using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Theory]
        [InlineData("Lord of The Rings", 0, 0, 1)]
        [InlineData("Lord of The Rings", 0, 1, 0)]
        [InlineData("Lord of The Rings", 100, 0, 5)]
        [InlineData("", 0, 0, 4)]
        [InlineData("", 100, 1, 1)]
        [InlineData("", 0, 1, 1)]
        [InlineData("lor", 100, 1, 1)]
        [InlineData("lord", 100, 0, 1)]
        [InlineData("lord", 0, 1, 1)]
        [InlineData(" ", 100, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string Title, int PageCount, int GenreId, int AuthorId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = Title,
                PageCount = PageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = GenreId,
                AuthorId = AuthorId
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualFutureIsGiven_Validator_ShouldReturnError()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Title",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddDays(1),
                GenreId = 1,
                AuthorId = 1
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Title",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1,
                AuthorId = 1
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }



    }
}
