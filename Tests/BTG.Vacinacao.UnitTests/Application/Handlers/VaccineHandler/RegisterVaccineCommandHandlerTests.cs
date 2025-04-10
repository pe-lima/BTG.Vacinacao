using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.Handlers.VaccineHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.CrossCutting.Exceptions;
using FluentValidation;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.VaccineHandler
{
    public class RegisterVaccineCommandHandlerTests
    {
        private readonly Mock<IVaccineRepository> _mockVaccineRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly RegisterVaccineCommandHandler _handler;

        public RegisterVaccineCommandHandlerTests()
        {
            _mockVaccineRepo = new Mock<IVaccineRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork
                .Setup(u => u.Vaccine)
                .Returns(_mockVaccineRepo.Object);

            _handler = new RegisterVaccineCommandHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Register_Vaccine_Successfully()
        {
            var command = new RegisterVaccineCommand("COVID-19 Vaccine", "121234");

            _mockVaccineRepo
                .Setup(r => r.ExistsByCodeAsync(command.Code))
                .ReturnsAsync(false);

            _mockVaccineRepo
                .Setup(r => r.AddAsync(It.IsAny<Vaccine>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(u => u.CommitAsync())
                .ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            _mockVaccineRepo.Verify(r => r.AddAsync(It.IsAny<Vaccine>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Code, result.Code);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Should_Throw_When_Vaccine_Code_Already_Exists()
        {
            var command = new RegisterVaccineCommand("COVID-19", "121234");

            _mockVaccineRepo
                .Setup(r => r.ExistsByCodeAsync(command.Code))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<GlobalException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
