using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.Application.Handlers.VaccinationHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Enums;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using Moq;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.VaccinationHandler
{
    public class RegisterVaccinationCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _mockPersonRepo;
        private readonly Mock<IVaccineRepository> _mockVaccineRepo;
        private readonly Mock<IVaccinationRepository> _mockVaccinationRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly RegisterVaccinationCommandHandler _handler;

        public RegisterVaccinationCommandHandlerTests()
        {
            _mockPersonRepo = new Mock<IPersonRepository>();
            _mockVaccineRepo = new Mock<IVaccineRepository>();
            _mockVaccinationRepo = new Mock<IVaccinationRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork
                .Setup(u => u.Person)
                .Returns(_mockPersonRepo.Object);

            _mockUnitOfWork
                .Setup(u => u.Vaccine)
                .Returns(_mockVaccineRepo.Object);

            _mockUnitOfWork
                .Setup(u => u.Vaccination)
                .Returns(_mockVaccinationRepo.Object);

            _handler = new RegisterVaccinationCommandHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Register_Vaccination_Successfully()
        {
            var command = new RegisterVaccinationCommand("12345678901", "123456", DoseType.FirstDose, DateTime.Today);
            var person = new Person("John Doe", command.Cpf);
            var vaccine = new Vaccine("Covid-19", command.VaccineCode);

            _mockPersonRepo
                .Setup(r => r.GetByCpfAsync(command.Cpf))
                .ReturnsAsync(person);

            _mockVaccineRepo
                .Setup(r => r.GetByCodeAsync(command.VaccineCode))
                .ReturnsAsync(vaccine);

            _mockVaccinationRepo
                .Setup(r => r.ExistsAsync(person.Id, vaccine.Id, command.DoseType))
                .ReturnsAsync(false);

            _mockVaccinationRepo
                .Setup(r => r.AddAsync(It.IsAny<Vaccination>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork
                .Setup(u => u.CommitAsync())
                .ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(person.Name, result.PersonName);
            Assert.Equal(vaccine.Name, result.VaccineName);
            Assert.Equal(command.DoseType, result.DoseType);
            Assert.Equal(command.ApplicationDate, result.ApplicationDate);
        }

        [Fact]
        public async Task Should_Throw_When_Person_Not_Found()
        {
            var command = new RegisterVaccinationCommand("00000000000", "123456", DoseType.FirstDose, DateTime.Today);

            _mockPersonRepo
                .Setup(r => r.GetByCpfAsync(command.Cpf))
                .ReturnsAsync((Person?)null);

            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_Throw_When_Vaccine_Not_Found()
        {
            var person = new Person("User", "12312312312");
            var command = new RegisterVaccinationCommand(person.Cpf, "000000", DoseType.FirstDose, DateTime.Today);

            _mockPersonRepo
                .Setup(r => r.GetByCpfAsync(command.Cpf))
                .ReturnsAsync(person);

            _mockVaccineRepo
                .Setup(r => r.GetByCodeAsync(command.VaccineCode))
                .ReturnsAsync((Vaccine?)null);

            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_Throw_When_Vaccination_Already_Exists()
        {
            var person = new Person("User", "12345678901");
            var vaccine = new Vaccine("Covid", "123456");
            var command = new RegisterVaccinationCommand(person.Cpf, vaccine.Code, DoseType.FirstDose, DateTime.Today);

            _mockPersonRepo
                .Setup(r => r.GetByCpfAsync(command.Cpf))
                .ReturnsAsync(person);

            _mockVaccineRepo
                .Setup(r => r.GetByCodeAsync(command.VaccineCode))
                .ReturnsAsync(vaccine);

            _mockVaccinationRepo
                .Setup(r => r.ExistsAsync(person.Id, vaccine.Id, command.DoseType))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
