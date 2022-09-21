using FluentValidation;

namespace WebApi.Application.BookOperations.GetBooks
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(query => query.BookId).NotEmpty().GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır");
        }
    }
}