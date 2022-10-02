using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Fact]
        public void WhenGenreNameLengthIsLessThanFour_UpdateGenreCommandValidator_ShouldReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId=1;
            command.Model=new UpdateGenreModel()
            {
                Name="as"
            };
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result=validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGenreNameLengthIsValid_UpdateGenreCommandValidator_ShouldNotReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId=1;
            command.Model=new UpdateGenreModel()
            {
                Name="Updated Genre"
            };
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result=validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}