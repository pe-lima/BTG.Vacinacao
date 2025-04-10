using BTG.Vacinacao.Application.Handlers.VaccinationCardHandler;
using BTG.Vacinacao.Application.Queries.VaccinationCardQuery;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Enums;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using Moq;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.VaccinationCardHandler
{
    public class GetVaccinationCardQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPersonRepository> _mockPersonRepo;
        private readonly Mock<IVaccinationRepository> _mockVaccinationRepo;

        private readonly GetVaccinationCardByCpfQueryHandler _handler;

        public GetVaccinationCardQueryHandlerTests()
        {
            _mockPersonRepo = new Mock<IPersonRepository>();
            _mockVaccinationRepo = new Mock<IVaccinationRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork
                .Setup(u => u.Person)
                .Returns(_mockPersonRepo.Object);

            _mockUnitOfWork
                .Setup(u => u.Vaccination)
                .Returns(_mockVaccinationRepo.Object);

            _handler = new GetVaccinationCardByCpfQueryHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Throw_When_Person_Not_Found()
        {
            var query = new GetVaccinationCardByCpfQuery("12345678901");

            _mockPersonRepo
                .Setup(r => r.GetByCpfAsync(query.Cpf))
                .ReturnsAsync((Person?)null);

            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Should_Return_Empty_List_When_Person_Has_No_Vaccinations()
        {
            var query = new GetVaccinationCardByCpfQuery("12345678901");
            
            var person = new Person("João", query.Cpf);

            _mockPersonRepo
                .Setup(r => r.GetByCpfAsync(query.Cpf))
                .ReturnsAsync(person);

            _mockVaccinationRepo
                .Setup(r => r.GetByPersonIdWithVaccineAsync(person.Id))
                .ReturnsAsync(new List<Vaccination>());

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(person.Name, result.Name);
            Assert.Equal(person.Cpf, result.Cpf);
            Assert.Empty(result.Vaccinations);
        }

        [Fact]
        public async Task Should_Return_VaccinationCardDto_With_Vaccinations()
        {
            var query = new GetVaccinationCardByCpfQuery("12345678901");
            var person = new Person("João", query.Cpf);
            var vaccine = new Vaccine("Covid-19", "123456");

            var vaccination = new Vaccination(person.Id, vaccine.Id, DoseType.FirstDose, DateTime.Today)
            {
                Vaccine = vaccine
            };

            _mockPersonRepo
                .Setup(r => r.GetByCpfAsync(query.Cpf))
                .ReturnsAsync(person);

            _mockVaccinationRepo
                .Setup(r => r.GetByPersonIdWithVaccineAsync(person.Id))
                .ReturnsAsync(new List<Vaccination>() { vaccination });

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(person.Name, result.Name);
            Assert.Equal(person.Cpf, result.Cpf);
            Assert.Single(result.Vaccinations);

            var record = result.Vaccinations[0];
            Assert.Equal(vaccination.Id, record.Id);
            Assert.Equal("Covid-19", record.VaccineName);
            Assert.Equal(DoseType.FirstDose.ToString(), record.DoseType);
            Assert.Equal(DateTime.Today, record.ApplicationDate);
        }
    }
}
