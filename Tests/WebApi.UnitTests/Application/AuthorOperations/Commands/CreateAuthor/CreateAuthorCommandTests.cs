using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_CreateAuthorCommand_ShouldThrowInvalidOperationException()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = "Sami",
                Surname = "Sezgin",
                DateOfBirth = new System.DateTime(1994, 4, 17),
                GenreId = 1
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu isimde bir yazar bulunmaktadır!");
        }

        [Fact]
        public void WhenNotExistGenreIdIsGiven_CreateAuthorCommand_ShouldThrowInvalidOperationException()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = "TestName",
                Surname = "TestSurname",
                DateOfBirth = new System.DateTime(1994, 4, 17),
                GenreId = 6
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunmadığı için yazar eklenememiştir!");
        }

        [Fact]
        public void WhenValidModelIsGiven_CreateAuthorCommand_ShouldNotThrowInvalidOperationException()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = "TestAuthorName",
                Surname = "TestAuthorSurname",
                DateOfBirth = new System.DateTime(1994, 4, 17),
                GenreId = 1
            };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow<InvalidOperationException>();
        }
    }
}