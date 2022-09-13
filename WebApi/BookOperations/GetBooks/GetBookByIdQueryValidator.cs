using FluentValidation;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(query => query.BookId).NotEmpty().GreaterThan(0);
        }
    }
}