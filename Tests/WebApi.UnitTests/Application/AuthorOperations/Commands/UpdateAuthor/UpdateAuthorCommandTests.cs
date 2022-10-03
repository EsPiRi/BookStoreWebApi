using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenGivenAuthorIdIsNotExist_UpdateAuthorCommand_ShouldThrowInvalidOperationException()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, null);
            command.AuthorId = 7;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istenilen yazar bulunmamaktadır!");
        }

        [Fact]
        public void WhenGivenAuthorNameAlreadyExist_UpdateAuthorCommand_ShouldThrowInvalidOperationException()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, null);
            command.AuthorId = 2;
            command.Model = new UpdateAuthorModel()
            {
                Name = "Sami",
                Surname = "Sezgin"
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu isimde bir yazar zaten bulunmaktadır. Güncelleme yapılamadı.");
        }

        [Fact]
        public void WhenGivenGenreIsNotExist_UpdateAuthorCommand_ShouldThrowInvalidOperationException()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
        {
                Name = "AuthorName",
                Surname = "AuthorSurname",
                GenreId = 8
            };
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istenilen kitap türü mevcut değildir!");
        }

        [Fact]
        public void WhenValidUpdateModelIsGiven_UpdateAuthorCommand_ShouldNotThrowInvalidOperationException()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                Name = "AuthorName",
                Surname = "AuthorSurname",
                GenreId = 1
            };
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow<InvalidOperationException>();
        }
    }
}