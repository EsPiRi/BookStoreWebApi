using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailsQueryTests : IClassFixture<CommonTextFixture>
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailsQueryTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidBookIdIsGiven_GetBookDetailQuery_ShouldThrowInvalidOperationException()
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = 66;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil");
        }
        [Fact]
        public void WhenValidBookIdIsGiven_GetBookDetailQuery_ShouldNotThrowInvalidOperationException()
        {
            GetBookByIdQuery query=new GetBookByIdQuery(_context,_mapper);
            query.BookId=1;
            FluentActions.Invoking(() => query.Handle()).Should().NotThrow<InvalidOperationException>();
     
        }
    }
}