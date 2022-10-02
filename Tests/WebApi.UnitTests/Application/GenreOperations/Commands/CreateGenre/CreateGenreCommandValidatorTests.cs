using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Fact]
        public void WhenGenreNameLengthIsLessThanFourCharacters_CreateGenreCommandValidator_ShouldReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            command.Model = new CreateGenreModel()
            {
                Name = "a"
            };
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGenreNameLengthIsValid_CreateGenreCommandValidator_ShouldNotReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            command.Model = new CreateGenreModel()
            {
                Name = "Test Genre"
            };
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}