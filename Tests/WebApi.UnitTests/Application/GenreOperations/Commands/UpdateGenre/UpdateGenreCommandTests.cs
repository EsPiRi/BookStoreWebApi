using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistGenreIdIsGiven_UpdateGenreCommand_ShouldThrowInvalidOperationException()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel()
            {
                Name = "Updated Genre"
            };
            command.GenreId = 15;
            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı!");
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_UpdateGenreCommand_ShouldThrowInvalidOperationException()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel()
            {
                Name = "Romance"
            };
            command.GenreId = 1;
            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir kitap türü zaten mevcut!");
        }

        [Fact]
        public void WhenValidGenreNameIsGiven_UpdateGenreCommand_ShouldNotThrowInvalidOperationException()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel()
            {
                Name = "Updated Genre"
            };
            command.GenreId = 1;
            FluentActions.Invoking(()=>command.Handle()).Should().NotThrow<InvalidOperationException>();
        }
    }
}