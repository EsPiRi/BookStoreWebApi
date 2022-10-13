using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.DBOperations;
using Xunit;

namespace Application.TokenOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateTokenCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Theory]
        [InlineData("testuser@test.com","asd12345")]
        [InlineData("testuser@asd.com","test12345")]

        public void WhenInvalidEmailOrPasswordIsGiven_CreateTokenCommand_ShouldThrowInvalidOperationException(string email,string password)
        {   
            CreateTokenCommand command=new CreateTokenCommand(_context,_mapper,null);
            command.Model=new CreateTokenModel(){
                Email=email,
                Password=password
            };

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı adı - şifre hatalı");
        }
        [Fact]
        public void WhenValidInputIsGiven_CreateTokenCommand_ShouldNotThrowInvalidOperationException()
        {
            CreateTokenCommand command=new CreateTokenCommand(_context,_mapper,null);
            command.Model=new CreateTokenModel(){
                Email="testuser@test.com",
                Password="test12345"
            };
           

            FluentActions.Invoking(()=>command.Handle()).Should().NotThrow<InvalidOperationException>();
        
        }
    }
}