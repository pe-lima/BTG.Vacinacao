using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.Handlers.VaccineHandler;
using BTG.Vacinacao.Core.Entities;
using BTG.Vacinacao.Core.Interfaces.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.UnitTests.Application.Handlers.VaccineHandler
{
    public class RegisterVaccineCommandHandlerTests
    {
        [Fact]
        public async Task Should_Register_Vaccine_Successfully()
        {
            // Arrange
            var command = new RegisterVaccineCommand("COVID-19 Vaccine", "121234");

            var mockRepo = new Mock<IVaccineRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Vaccine>()))
                    .Returns(Task.CompletedTask);
            
            var handler = new RegisterVaccineCommandHandler(mockRepo.Object);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Vaccine>()), Times.AtLeastOnce());
            Assert.NotNull(result);
            Assert.Equal("COVID-19 Vaccine", result.Name);
            Assert.Equal("121234", result.Code);
            Assert.NotEqual(Guid.Empty, result.Id);
        }
    }
}
