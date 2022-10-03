using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteAuthorCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenGivenAuthorIdIsNotExist_DeleteAuthorCommand_ShouldThrowInvalidOperationException()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 6;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Mevcut Değil");
        }
        [Fact]
        public void WhenGivenAuthorHasAtLeastOneBook_DeleteAuthorCommand_ShouldThrowInvalidOperationException()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu yazara ait en az bir kitap var, yazar silinemez");
        }

        [Fact]
        public void WhenGivenAuthorIdExists_DeleteAuthorCommand_ShouldNotThrowInvalidOperationException()
        {
            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(_context, _mapper);
            createAuthorCommand.Model = new CreateAuthorModel()
            {
                Name = "TestAuthorName",
                Surname = "TestAuthorSurname",
                DateOfBirth = new DateTime(1994, 4, 17),
                GenreId = 1
            };
            createAuthorCommand.Handle();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = _context.Authors.SingleOrDefault(author => author.Name.Equals(createAuthorCommand.Model.Name)).Id;
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow<InvalidOperationException>();
        }
    }
}