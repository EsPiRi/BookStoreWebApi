using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTextFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void WhenInvalidBookIdIsGiven_DeleteBookCommandValidator_ShouldReturnError(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            command.BookId = bookId;
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenValidBookIdIsGiven_DeleteBookCommandValidator_ShouldNotReturnError(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            command.BookId = bookId;
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

    }
}