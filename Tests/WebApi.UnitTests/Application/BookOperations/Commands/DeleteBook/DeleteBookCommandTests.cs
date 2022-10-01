using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;

        }

        [Fact]
        public void WhenNotExistBookIdIsGiven_DeleteBookCommand_ShouldThrowInvalidOperationException()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 99;

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil!");

        }
        [Fact]
        public void WhenValidBookIdIsGiven_DeleteBookCommand_ShouldNotThrowInvalidOperationException()
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;

            // act & assert
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow<InvalidOperationException>();

        }
        [Fact]
        public void WhenValidBookIdIsGiven_DeleteBookCommand_ShouldDeleteBook()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 3;
            command.Handle();
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().BeNull();
        }
    }
}