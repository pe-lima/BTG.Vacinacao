using BTG.Vacinacao.Application.Commands.AuthCommand;
using BTG.Vacinacao.Application.DTOs.Auth;
using BTG.Vacinacao.Application.Handlers.AuthHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.Core.Interfaces.Services;
using FluentValidation;
using Moq;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.AuthHandler
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(u => u.User).Returns(_userRepoMock.Object);

            _handler = new LoginCommandHandler(_unitOfWorkMock.Object, _jwtServiceMock.Object);
        }

        [Fact]
        public async Task Should_Login_Successfully()
        {
            // Arrange
            var command = new LoginCommand("admin", "Admin@123");
            var user = new User("admin", BCrypt.Net.BCrypt.HashPassword("Admin@123"));
            var expectedToken = "token";
            var expectedExpiration = DateTime.UtcNow.AddHours(1);

            _userRepoMock.Setup(r => r.GetByUsernameAsync(command.Username))
                         .ReturnsAsync(user);

            _jwtServiceMock.Setup(s => s.GenerateToken(user))
                           .Returns((expectedToken, expectedExpiration));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedToken, result.Token);
            Assert.Equal(expectedExpiration, result.Expiration);
        }

        [Fact]
        public async Task Should_Throw_When_User_Not_Found()
        {
            var command = new LoginCommand("notfound", "Admin@123");

            _userRepoMock.Setup(r => r.GetByUsernameAsync(command.Username))
                         .ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_Throw_When_Password_Is_Invalid()
        {
            var command = new LoginCommand("admin", "wrongpass");
            var user = new User("admin", BCrypt.Net.BCrypt.HashPassword("Admin@123"));

            _userRepoMock.Setup(r => r.GetByUsernameAsync(command.Username))
                         .ReturnsAsync(user);

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
