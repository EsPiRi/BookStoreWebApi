using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateToken;
using Xunit;

namespace Application.TokenOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Theory]
        [InlineData("@a.com", "12345678")]
        [InlineData("a@a.com", "1235678")]
        public void WhenInvalidInputIsGiven_CreateTokenCommandValidator_ShouldReturnErrors(string email, string password)
        {
            CreateTokenCommand command = new CreateTokenCommand(null, null, null);
            command.Model = new CreateTokenModel()
            {
                Email = email,
                Password = password
            };
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_CreateTokenCommandValidator_ShouldNotReturnErrors()
        {
            CreateTokenCommand command = new CreateTokenCommand(null, null, null);
            command.Model = new CreateTokenModel()
            {
                Email = "email@provider.com",
                Password = "12345678"
            };
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }

    }
}