using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Application.Handlers.VaccineHandler;
using BTG.Vacinacao.Application.Queries.VaccineQuery;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using FluentValidation;
using Moq;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.VaccineHandler
{
    public class GetVaccineByCodeQueryHandlerTests
    {
        private readonly Mock<IVaccineRepository> _mockVaccineRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetVaccineByCodeQueryHandler _handler;

        public GetVaccineByCodeQueryHandlerTests()
        {
            _mockVaccineRepository = new Mock<IVaccineRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(u => u.Vaccine).Returns(_mockVaccineRepository.Object);

            _handler = new GetVaccineByCodeQueryHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Return_Vaccine_When_Code_Is_Valid()
        {
            var vaccine = new Vaccine("Tétano", "654321");
            _mockVaccineRepository
                .Setup(r => r.GetByCodeAsync("654321"))
                .ReturnsAsync(vaccine);

            var query = new GetVaccineByCodeQuery("654321");

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Tétano", result.Name);
            Assert.Equal("654321", result.Code);
        }

        [Fact]
        public async Task Should_Throw_When_Vaccine_Not_Found()
        {
            _mockVaccineRepository
                .Setup(r => r.GetByCodeAsync("000000"))
                .ReturnsAsync((Vaccine?)null);

            var query = new GetVaccineByCodeQuery("000000");

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
