using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
         [Fact]
        public void WhenBookIdIsNotValid_UpdateBookCommandValidator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "asdf",
                AuthorId=1,
                GenreId=1,
                PageCount=100
            };
            command.Model = model;
            command.BookId = 0;
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenTitleLengthIsNotValid_UpdateBookCommandValidator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = ""
            };
            command.Model = model;
            command.BookId = 1;
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

         [Fact]
        public void WhenGenreIdIsNotValid_UpdateBookCommandValidator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
           UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "asdf",
                AuthorId=1,
                GenreId=0,
                PageCount=100
            };
            command.Model = model;
            command.BookId = 1;
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
         [Fact]
        public void WhenPageCountIsNotValid_UpdateBookCommandValidator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "asdf",
                AuthorId=1,
                GenreId=1,
                PageCount=-1
            };
            command.Model = model;
            command.BookId = 1;
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
         [Fact]
        public void WhenAuthorIdIsNotValid_UpdateBookCommandValidator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "asdf",
                AuthorId=-1,
                GenreId=1,
                PageCount=100
            };
            command.Model = model;
            command.BookId = 1;
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

         [Fact]
        public void WhenBookInfoIsValid_UpdateBookCommandValidator_ShouldNotReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "asdf",
                AuthorId=1,
                GenreId=1,
                PageCount=100
            };
            command.Model = model;
            command.BookId = 1;
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}