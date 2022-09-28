using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(author=>author.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(author=>author.Model.Surname).NotEmpty().MinimumLength(2);
            RuleFor(author=>author.Model.DateOfBirth.Date).NotEmpty();
            RuleFor(author=>author.Model.GenreId).GreaterThan(0);
            
        }
    }
}