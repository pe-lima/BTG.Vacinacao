using BTG.Vacinacao.Application.Handlers.PersonHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.Application.Queries.PersonQuery;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.PersonHandler
{
    public class GetAllPersonsQueryHandlerTests
    {
        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly GetAllPersonsQueryHandler _handler;

        public GetAllPersonsQueryHandlerTests()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork
                .Setup(u => u.Person)
                .Returns(_mockPersonRepository.Object);

            _handler = new GetAllPersonsQueryHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllPersons()
        {
            var persons = new List<Person>
            {
                new Person("John Doe", "12345678901"),
                new Person("Jane Doe", "98765432100")
            };
            
            _mockPersonRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(persons);
            
            var query = new GetAllPersonsQuery();
            
            var result = await _handler.Handle(query, CancellationToken.None);
            
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].Name);
            Assert.Equal("Jane Doe", result[1].Name);
        }

    }
}
