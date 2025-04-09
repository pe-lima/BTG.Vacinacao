using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.Handlers.VaccineHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.VaccineHandler
{
    public class RegisterVaccineCommandHandlerTests
    {
        [Fact]
        public async Task Should_Register_Vaccine_Successfully()
        {
            // Arrange
            var command = new RegisterVaccineCommand("COVID-19 Vaccine", "121234");

            var mockVaccineRepo = new Mock<IVaccineRepository>();
            mockVaccineRepo.Setup(r => r.ExistsByCodeAsync(command.Code)).ReturnsAsync(false);
            mockVaccineRepo.Setup(r => r.AddAsync(It.IsAny<Vaccine>())).Returns(Task.CompletedTask);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Vaccine).Returns(mockVaccineRepo.Object);
            mockUnitOfWork.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            var handler = new RegisterVaccineCommandHandler(mockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockVaccineRepo.Verify(r => r.AddAsync(It.IsAny<Vaccine>()), Times.Once);
            mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("COVID-19 Vaccine", result.Name);
            Assert.Equal("121234", result.Code);
            Assert.NotEqual(Guid.Empty, result.Id);
        }
    }
}
