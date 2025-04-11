using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Application.Handlers.VaccineHandler;
using BTG.Vacinacao.Application.Queries.VaccineQuery;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using Moq;
using Xunit;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.VaccineHandler
{
    public class GetAllVaccinesQueryHandlerTests
    {
        private readonly Mock<IVaccineRepository> _mockVaccineRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetAllVaccinesQueryHandler _handler;

        public GetAllVaccinesQueryHandlerTests()
        {
            _mockVaccineRepository = new Mock<IVaccineRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.Vaccine).Returns(_mockVaccineRepository.Object);

            _handler = new GetAllVaccinesQueryHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_Return_All_Vaccines()
        {
            var vaccines = new List<Vaccine>
            {
                new Vaccine("BCG", "111111"),
                new Vaccine("Hepatite B", "222222")
            };

            _mockVaccineRepository
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(vaccines);

            var query = new GetAllVaccinesQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, v => v.Name == "BCG");
            Assert.Contains(result, v => v.Code == "222222");
        }
    }
}
