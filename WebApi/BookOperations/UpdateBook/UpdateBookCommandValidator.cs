using System.Data;
using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command=>command.BookId).NotEmpty().WithMessage("Kitap id'si boş olamaz").GreaterThan(0).WithMessage("Kitap id'si 0'dan büyük olmalıdır.");
            RuleFor(command=>command.Model.GenreId).NotEmpty().WithMessage("Kitabın Genre id'si boş olamaz").GreaterThan(0).WithMessage("Kitabın Genre id'si 0'dan büyük olmalıdır.");
            RuleFor(command=>command.Model.PageCount).NotEmpty().WithMessage("Kitabın sayfa sayısı boş olamaz").GreaterThan(0).WithMessage("Kitabın sayfa sayısı sıfırdan büyük olmalıdır.");
            RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(4).WithMessage("Kitabın ismi 4 karakterden uzun olmalıdır").MaximumLength(20).WithMessage("Kitabın ismi 21 karakterden az olmalıdır");
        }
    }
}