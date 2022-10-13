using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(token=>token.RefreshToken).NotEmpty();
        }
    }
}