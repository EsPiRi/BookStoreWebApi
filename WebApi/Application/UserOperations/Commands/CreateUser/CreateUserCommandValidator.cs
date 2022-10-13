using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(user=>user.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(user=>user.Model.Email).NotEmpty().MinimumLength(7);
            RuleFor(user=>user.Model.Surname).NotEmpty().MinimumLength(2);
            RuleFor(user=>user.Model.Password).NotEmpty().MinimumLength(8);

        }
    }
}