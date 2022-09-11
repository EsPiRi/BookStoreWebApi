using System.Data;
using System.Linq;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator(BookStoreDbContext context)
        {
            RuleFor(command=>command.BookId).InclusiveBetween(context.Books.FirstOrDefault().Id,context.Books.LastOrDefault().Id).WithMessage("Girilen id ile bir kitap yok! Girilebilecek aralık: "+context.Books.FirstOrDefault().Id+"-"+context.Books.LastOrDefault().Id);
        }
    }
}