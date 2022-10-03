using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Fact]
        public void WhenGivenAuthorIdIsLessThanZero_DeleteAuthorCommandValidator_ShouldReturnError()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = -1;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();

            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenGivenAuthorIdIsGreaterThanZero_DeleteAuthorCommandValidator_ShouldNotReturnError()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = 6;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();

            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}