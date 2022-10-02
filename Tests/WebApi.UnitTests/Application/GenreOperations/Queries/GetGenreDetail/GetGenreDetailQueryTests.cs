using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailsQueryTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailsQueryTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistGenreIdIsGiven_GetGenreDetailQuery_ShouldThrowInvalidOperationException()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 15;
            FluentActions.Invoking(()=> query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_GetGenreDetailQuery_ShouldNotThrowInvalidOperationException()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 1;
            FluentActions.Invoking(()=> query.Handle()).Should().NotThrow<InvalidOperationException>();
        }
    }
}