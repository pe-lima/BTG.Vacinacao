using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.Handlers.PersonHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.PersonHandler
{
    public class RegisterPersonCommandHandlerTests
    {
        [Fact]
        public async Task Should_Register_Person_Successfully()
        {
            // Arrange
            var command = new RegisterPersonCommand("John Doe", "12312312312");

            var mockPersonRepo = new Mock<IPersonRepository>();
            mockPersonRepo.Setup(r => r.ExistsByCpfAsync(command.Cpf)).ReturnsAsync(false);
            mockPersonRepo.Setup(r => r.AddAsync(It.IsAny<Person>())).Returns(Task.CompletedTask);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Person).Returns(mockPersonRepo.Object);
            mockUnitOfWork.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            var handler = new RegisterPersonCommandHandler(mockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockPersonRepo.Verify(r => r.AddAsync(It.IsAny<Person>()), Times.Once);
            mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Cpf, result.Cpf);
            Assert.NotEqual(Guid.Empty, result.Id);
        }
    }
}
