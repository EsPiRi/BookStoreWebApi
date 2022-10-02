using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteGenreCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_DeleteGenreCommand_ShouldThrowInvalidOperationException()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = -1;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinmek istenen kitap türü mevcut değil!");
        }

        [Fact]
        public void WhenGenreTypeHasBook_DeleteGenreCommand_ShouldThrowInvalidOperationException()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinmek istenen kitap türüne ait en az bir kitap bulunmaktadır. Tür silinemez!");
        }
        
        [Fact]
        public void WhenGenreTypeHasAuthor_DeleteGenreCommand_ShouldThrowInvalidOperationException()
        {
            CreateGenreCommand createCommand = new CreateGenreCommand(_context, _mapper);
            createCommand.Model = new CreateGenreModel()
            {
                Name = "Newest Genre"
            };
            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(_context, _mapper);
            createAuthorCommand.Model = new CreateAuthorModel()
            {
                DateOfBirth = new DateTime(1994, 4, 17),
                GenreId = 4,
                Name = "New",
                Surname = "Author"

            };

            createCommand.Handle();
            createAuthorCommand.Handle();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 4;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinmek istenen kitap türüne ait en az bir yazar bulunmaktadır. Tür silinemez!");

        }

        [Fact]
        public void WhenGenreHasNoDependency_DeleteGenreCommand_ShouldNotThrowInvalidOperationException()
        {
            CreateGenreCommand createCommand = new CreateGenreCommand(_context, _mapper);
            createCommand.Model = new CreateGenreModel()
            {
                Name = "New Genre"
            };
            createCommand.Handle();

            DeleteGenreCommand deleteCommand = new DeleteGenreCommand(_context);
            deleteCommand.GenreId = _context.Genres.SingleOrDefault(genre => genre.Name == createCommand.Model.Name).Id;
            FluentActions.Invoking(() => deleteCommand.Handle()).Should().NotThrow<InvalidOperationException>();

        }
    }
}