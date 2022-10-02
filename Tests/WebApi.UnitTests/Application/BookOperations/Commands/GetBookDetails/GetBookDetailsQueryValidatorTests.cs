using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBooks;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailsQueryValidatorTests : IClassFixture<CommonTextFixture>
    {
        [Fact]
        public void WhenInvalidBookIdIsGiven_GetBookDetailValidator_ShouldReturnError()
        {
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.BookId = -1;
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidBookIdIsGiven_GetBookDetailValidator_ShouldNotReturnError()
        {
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.BookId = 2;
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}