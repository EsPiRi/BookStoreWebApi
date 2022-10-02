using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_CreateGenreCommand_ShouldThrowInvalidOperationException()
        {
            var genre = new Genre()
            {
                Name = "test"

            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name = genre.Name
            };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Böyle bir kitap türü zaten var!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_CreateGenreCommand_ShouldCreateTheGenre()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name = "New Genre"
            };
            
            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre=_context.Genres.SingleOrDefault(genre=>genre.Name==command.Model.Name);
            
            genre.Should().NotBeNull();            

        }
    }
}