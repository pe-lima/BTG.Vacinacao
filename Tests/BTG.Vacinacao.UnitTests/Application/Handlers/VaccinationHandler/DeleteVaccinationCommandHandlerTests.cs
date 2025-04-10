using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.Application.Handlers.VaccinationHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Enums;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using Moq;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.VaccinationHandler
{
    public class DeleteVaccinationCommandHandlerTests
    {
        private readonly Mock<IVaccinationRepository> _vaccinationRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeleteVaccinationCommandHandler _handler;

        public DeleteVaccinationCommandHandlerTests()
        {
            _vaccinationRepoMock = new Mock<IVaccinationRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock
                .Setup(u => u.Vaccination)
                .Returns(_vaccinationRepoMock.Object);

            _handler = new DeleteVaccinationCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Should_Delete_Vaccination_When_Found()
        {
            var vaccinationId = Guid.NewGuid();
            var vaccination = new Vaccination(Guid.NewGuid(), Guid.NewGuid(), DoseType.FirstDose, DateTime.Today);

            _vaccinationRepoMock
                .Setup(r => r.GetByIdAsync(vaccinationId))
                .ReturnsAsync(vaccination);

            var command = new DeleteVaccinationCommand(vaccinationId);

            await _handler.Handle(command, CancellationToken.None);

            _vaccinationRepoMock.Verify(r => r.Remove(vaccination), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Vaccination_Not_Found()
        {
            var vaccinationId = Guid.NewGuid();

            _vaccinationRepoMock
                .Setup(r => r.GetByIdAsync(vaccinationId))
                .ReturnsAsync((Vaccination?)null);

            var command = new DeleteVaccinationCommand(vaccinationId);

            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
