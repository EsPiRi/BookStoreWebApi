using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_CreateBookCommand_ShouldThrowInvalidOperationException()
        {
            //arrange {Hazırlık}
            var book = new Book()
            {
                Title = "Test_WhenAlreadyExistBookTitleIsGiven_CreateBookCommand_ShouldThrowInvalidOperationException",
                PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel()
            {
                Title = book.Title
            };

            //act & assert {Çalıştırma - Doğrulama} 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");


        }

        [Fact]
        public void WhenValidInputsAreGiven_CreateBookCommand_ShouldCreateTheBook()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 1,
                AuthorId = 1
            };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }


    }
}
