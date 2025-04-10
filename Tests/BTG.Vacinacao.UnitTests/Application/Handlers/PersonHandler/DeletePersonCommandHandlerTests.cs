using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.Handlers.PersonHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Enums;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using Moq;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.PersonHandler
{
    public class DeletePersonCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPersonRepository> _personRepoMock;
        private readonly Mock<IVaccinationRepository> _vaccinationRepoMock;
        private readonly DeletePersonCommandHandler _handler;
        public DeletePersonCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _personRepoMock = new Mock<IPersonRepository>();
            _vaccinationRepoMock = new Mock<IVaccinationRepository>();

            _unitOfWorkMock
                .Setup(x => x.Person)
                .Returns(_personRepoMock.Object);
            
            _unitOfWorkMock
                .Setup(x => x.Vaccination)
                .Returns(_vaccinationRepoMock.Object);

            _handler = new DeletePersonCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Should_Delete_Person_And_Vaccinations_When_Exists()
        {
            var cpf = "12345678901";
            var person = new Person("João", cpf);
            var vaccinations = new List<Vaccination> { new(person.Id, Guid.NewGuid(), DoseType.FirstDose, DateTime.Today) };

            _personRepoMock
                .Setup(r => r.GetByCpfAsync(cpf))
                .ReturnsAsync(person);
            
            _vaccinationRepoMock
                .Setup(r => r.GetByPersonIdAsync(person.Id))
                .ReturnsAsync(vaccinations);

            var command = new DeletePersonCommand(cpf);

            var result = await _handler.Handle(command, CancellationToken.None);

            _vaccinationRepoMock.Verify(v => v.RemoveRange(vaccinations), Times.Once);
            _personRepoMock.Verify(p => p.Remove(person), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Person_Not_Found()
        {
            var cpf = "00000000000";
            _personRepoMock.Setup(p => p.GetByCpfAsync(cpf)).ReturnsAsync((Person?)null);

            var command = new DeletePersonCommand(cpf);

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
