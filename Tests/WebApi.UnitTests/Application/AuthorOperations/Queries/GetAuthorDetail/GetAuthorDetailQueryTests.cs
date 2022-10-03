using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorIsNotExist_GetAuthorDetailQuery_ShouldThrowInvalidOperationException()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 88;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aranılan yazar bulunmamaktadır");
        }

        [Fact]
        public void WhenAuthorIsExist_GetAuthorDetailQuery_ShouldNotThrowInvalidOperationException()
        {   // arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 1;
            
            // act&assert
            FluentActions.Invoking(() => query.Handle()).Should().NotThrow<InvalidOperationException>();
            
            // act
            var result = FluentActions.Invoking(() => query.Handle()).Invoke();
            // assert
            result.Name.Should().Be(_context.Authors.SingleOrDefault(author => author.Id == query.AuthorId).Name);
            result.Surname.Should().Be(_context.Authors.SingleOrDefault(author => author.Id == query.AuthorId).Surname);
            result.Genre.Should().Be(_context.Authors.SingleOrDefault(author => author.Id == query.AuthorId).Genre.Name);
        }

    }
}