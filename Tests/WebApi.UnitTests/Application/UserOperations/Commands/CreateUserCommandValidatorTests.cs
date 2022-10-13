using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using Xunit;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Theory]
        [InlineData("", "surname", "test@test.org", "password")]
        [InlineData("name", "", "test@test.org", "password")]
        [InlineData("name", "surname", "", "password")]
        [InlineData("name", "surname", "test@test.org", "passwor")]
        public void WhenInvalidInputIsGiven_CreateUserCommandValidator_ShouldReturnErrors(string name, string surname, string email, string password)
        {
            CreateUserCommand command = new CreateUserCommand(null, null);
            command.Model = new CreateUserModel()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password

            };
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_CreateUserCommandValidator_ShouldNotReturnError()
        {
            CreateUserCommand command = new CreateUserCommand(null, null);
            command.Model = new CreateUserModel()
            {
                Name = "name",
                Surname = "surname",
                Email = "email@mailprovider.com",
                Password = "password"

            };
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}