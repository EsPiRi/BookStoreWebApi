using System;
using System.Linq;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator(IBookStoreDbContext context)
        {
            if (context.Books.ToList().Count > 0)
            {
                RuleFor(command => command.BookId).InclusiveBetween(context.Books.FirstOrDefault().Id, context.Books.LastOrDefault().Id).WithMessage("Girilen id ile bir kitap yok! Girilebilecek aralık: " + context.Books.FirstOrDefault().Id + "-" + context.Books.LastOrDefault().Id);
            }
            else
            {
                throw new InvalidOperationException("Kitap Listesi Boş! Boş Listeden Kitap Silinemez");
            }
        }
    }
}