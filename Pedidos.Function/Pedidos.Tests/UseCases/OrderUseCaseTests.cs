using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Borders.Dtos.Messages;
using Pedidos.Borders.Repositories;
using Pedidos.Tests.Extensions;
using Pedidos.UseCases;
using Xunit;

namespace Pedidos.Tests.UseCases
{
    public class OrderUseCaseTests
    {
        private readonly Mock<ILogger<ProcessOrderUseCase>> logger = new();
        private readonly Mock<IManagementRepository> managementRepository = new();
        private readonly ProcessOrderUseCase useCase;

        public OrderUseCaseTests() => useCase = new(logger.Object, managementRepository.Object);

        [Fact]
        public async Task Execute_WhenProcessWithSuccess_LogSuccessMessage()
        {
            //Arrange
            var request = new OrderRequest(Guid.NewGuid(), Guid.NewGuid(), new List<ItemsRequest> { new ItemsRequest("Teste", 10, 200) });

            //Act
            await useCase.Execute(request);

            //Assert
            managementRepository.Verify(x => x.ProcessOrders(request), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenRepositoryThrowsException_LogErrorMessageAndThrowsException()
        {
            //Arrange
            var request = new OrderRequest(Guid.NewGuid(), Guid.NewGuid(), new List<ItemsRequest> { new ItemsRequest("Teste", 10, 200) });
            var exception = new Exception("Error calling repository");
            managementRepository.Setup(x => x.ProcessOrders(request)).Throws(exception);

            //Act
            var action = async () => await useCase.Execute(request);

            //Assert
            await action.Should().ThrowAsync<Exception>().WithMessage(exception.Message);
        }
    }
}
