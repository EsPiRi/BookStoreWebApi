using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(author=>author.AuthorId).GreaterThan(0);
            RuleFor(author=>author.Model.GenreId).GreaterThan(0);
            RuleFor(author=>author.Model.Name).MinimumLength(4).When(x => x.Model.Name.Trim() != string.Empty);
            RuleFor(author=>author.Model.Surname).MinimumLength(4).When(x => x.Model.Surname.Trim() != string.Empty);
            RuleFor(author=>author.Model.DateOfBirth).LessThanOrEqualTo(DateTime.Now.AddYears(-3));
        }
    }
}