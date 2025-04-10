using BTG.Vacinacao.Application.Handlers.PersonHandler;
using BTG.Vacinacao.Application.Queries.PersonQuery;
using BTG.Vacinacao.Core.Interfaces.Repositories;
using BTG.Vacinacao.CrossCutting.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.PersonHandler
{
    public class GetPersonByCpfQueryHandlerTests
    {
        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly GetPersonByCpfQueryHandler _handler;

        public GetPersonByCpfQueryHandlerTests()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            _mockUnitOfWork
                .Setup(u => u.Person)
                .Returns(_mockPersonRepository.Object);
            
            _handler = new GetPersonByCpfQueryHandler(_mockUnitOfWork.Object);
        }


        [Fact]
        public async Task Should_Return_Person_When_Cpf_Is_Valid()
        {
            var person = new Core.Entities.Person("John Doe", "12345678900");
            _mockPersonRepository
                .Setup(r => r.GetByCpfAsync("12345678900"))
                .ReturnsAsync(person);
            
            var query = new GetPersonByCpfQuery("12345678900");
            
            var result = await _handler.Handle(query, CancellationToken.None);
            
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("12345678900", result.Cpf);
        }

        [Fact]
        public async Task Should_Throw_When_Person_Not_Found()
        {
            _mockPersonRepository
                .Setup(r => r.GetByCpfAsync("00000000000"))
                .ReturnsAsync((Core.Entities.Person?)null);

            var query = new GetPersonByCpfQuery("00000000000");

            await Assert.ThrowsAsync<GlobalException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
