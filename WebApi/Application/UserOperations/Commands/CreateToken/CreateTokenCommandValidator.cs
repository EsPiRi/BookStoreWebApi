using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidator:AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(user=>user.Model.Email).NotEmpty().MinimumLength(7).WithMessage("E-mail uzunluğu yetersiz");
            RuleFor(user=>user.Model.Password).NotEmpty().MinimumLength(8).WithMessage("Şifre en az 8 karakter uzunluğunda olmalıdır");
        }
    }
}