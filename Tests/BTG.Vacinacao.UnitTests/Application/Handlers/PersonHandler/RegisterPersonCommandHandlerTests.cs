using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.Handlers.PersonHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.CrossCutting.Exceptions;
using FluentValidation;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.PersonHandler
{
    public class RegisterPersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _mockPersonRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly RegisterPersonCommandHandler _handler;

        public RegisterPersonCommandHandlerTests()
        {
            _mockPersonRepo = new Mock<IPersonRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork
                .Setup(u => u.Person)
                .Returns(_mockPersonRepo.Object);

            _handler = new RegisterPersonCommandHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Register_Person_Successfully()
        {
            var command = new RegisterPersonCommand("John Doe", "12312312312");

            _mockPersonRepo
                .Setup(r => r.ExistsByCpfAsync(command.Cpf))
                .ReturnsAsync(false);

            _mockPersonRepo
                .Setup(r => r.AddAsync(It.IsAny<Person>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(u => u.CommitAsync())
                .ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            _mockPersonRepo.Verify(r => r.AddAsync(It.IsAny<Person>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Cpf, result.Cpf);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Should_Throw_When_Cpf_Already_Exists()
        {
            var command = new RegisterPersonCommand("João", "12312312312");

            _mockPersonRepo
                .Setup(r => r.ExistsByCpfAsync(command.Cpf))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<GlobalException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
