using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using Xunit;

namespace Application.TokenOperations.Commands.CreateToken
{
    public class RefreshTokenCommandTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public RefreshTokenCommandTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Theory]
        [InlineData("testuser@test.com")]
        [InlineData("testuser@asd.com")]

        public void WhenInvalidRefreshTokenIsGiven_RefreshTokenCommand_ShouldThrowInvalidOperationException(string refreshToken)
        {   
            RefreshTokenCommand command=new RefreshTokenCommand(_context,null);
            command.RefreshToken=refreshToken;
            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Geçersiz Refresh Token verildi.");
        }
        
    }
}