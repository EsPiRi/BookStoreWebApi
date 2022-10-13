using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using Xunit;

namespace Application.TokenOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidatorTests:IClassFixture<CommonTextFixture>
    {
        [Fact]
        public void WhenEmptyRefreshTokenIsGiven_RefreshTokenCommandValidator_ShouldReturnErrors()
        {
            RefreshTokenCommand command=new RefreshTokenCommand(null,null);
            command.RefreshToken="";
            RefreshTokenCommandValidator validator=new RefreshTokenCommandValidator();
            var result=validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}