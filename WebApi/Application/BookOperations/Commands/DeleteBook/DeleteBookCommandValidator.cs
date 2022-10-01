using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {

            RuleFor(command => command.BookId).GreaterThan(0);

        }


    }


}