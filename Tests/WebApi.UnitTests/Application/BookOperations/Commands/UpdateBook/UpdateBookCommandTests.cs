using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;


        public UpdateBookCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_UpdateBookCommand_ShouldUpdateBook()
        {
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "HappyCase1",
                AuthorId = 3,
                GenreId = 2,
                PageCount = 100

            };
            int bookId=1;

            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            var book = _context.Books.SingleOrDefault(book => book.Id == bookId);

            command.Model = model;
            command.BookId = bookId;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();


            book = _context.Books.SingleOrDefault(book => book.Id == bookId);

            book.PageCount.Should().Be(model.PageCount);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
            book.Title.Should().Be(model.Title);


        }

        [Fact]
        public void WhenBookNotFoundWithGivenId_UpdateBookCommand_ShouldReturnInvalidOperationException()
        {
            UpdateBookViewModel model = new UpdateBookViewModel()
            {
                Title = "newTitle",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 100

            };
            UpdateBookCommand command = new UpdateBookCommand(_context, null);
            command.Model = model;
            command.BookId = 0;
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz kitap mevcut değil");
        }
    }
}