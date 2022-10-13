using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;
using Xunit;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistEmailIsGiven_CreateUserCommand_ShouldThrowInvalidOperationException()
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = new CreateUserModel()
            {
                Name = "TestName1",
                Surname = "TestSurname1",
                Email = "testuser@test.com",
                Password = "test12345"

            };
            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı zaten mevcut");
        }

    }
}