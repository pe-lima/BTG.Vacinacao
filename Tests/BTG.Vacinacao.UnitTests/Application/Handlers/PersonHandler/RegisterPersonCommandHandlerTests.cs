
using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.Handlers.PersonHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repository;
using Moq;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.PersonHandler
{
    public class RegisterPersonCommandHandlerTests
    {
        [Fact]
        public async Task Should_Register_Person_Successfully()
        {
            // Arrange
            var command = new RegisterPersonCommand("John Doe", "12312312312");

            var mockRepo = new Mock<IPersonRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Person>()))
                    .Returns(Task.CompletedTask);

            var handler = new RegisterPersonCommandHandler(mockRepo.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Person>()), Times.AtLeastOnce());
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("12312312312", result.Cpf);
            Assert.NotEqual(Guid.Empty, result.Id);
        }
    }
}
