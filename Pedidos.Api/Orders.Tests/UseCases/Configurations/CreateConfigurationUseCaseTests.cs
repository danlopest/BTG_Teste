using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Orders.Borders.Dtos.External.Product;
using Orders.Borders.Dtos.Internal.Configurations;
using Orders.Borders.Entities.Configurations;
using Orders.Borders.Repositories.External;
using Orders.Borders.Repositories.Internal;
using Orders.Borders.Shared;
using Orders.Tests.Builders.Dtos.Internal.Configurations;
using Orders.Tests.Builders.Entities.Configurations;
using Orders.UseCases.Configurations;
using Xunit;

namespace Orders.Tests.UseCases.Configurations
{
    public class CreateConfigurationUseCaseTests
    {
        private readonly AutoMocker mocker = new();

        private CreateOrderUseCase GetSut() => mocker.CreateInstance<CreateOrderUseCase>();

        [Fact]
        public async Task Execute_WhenValidRequest_ReturnsCreated()
        {
            // Arrange
            var request = new CreateConfigurationRequestBuilder().Build();

            Configuration? sentConfiguration = null;
            mocker.GetMock<IConfigurationRepository>().Setup(t => t.Create(It.IsAny<Configuration>())).Callback<Configuration>(r => sentConfiguration = r);

            mocker.GetMock<IProductRepository>().Setup(t => t.Get(request.CodigoPedido)).ReturnsAsync(new ProductResponse());

            var useCase = GetSut();

            // Act
            var response = await useCase.Execute(request);

            // Assert
            response.Should().NotBeNull();
            response.Success().Should().BeTrue();
            response.Result.Should().NotBeNull();
            sentConfiguration.Should().NotBeNull();
            response.Result.Should().BeEquivalentTo(new ConfigurationResponse(sentConfiguration!));
            mocker.VerifyAll();
        }

        [Fact]
        public async Task Execute_WhenConfigurationExistsForRole_ReturnsBadRequest()
        {
            // Arrange
            var request = new CreateConfigurationRequestBuilder().Build();
            var existingConfiguration = new ConfigurationBuilder(role: request.Role).Build();

            mocker.GetMock<IConfigurationRepository>()
                            .Setup(r => r.Get(request.CodigoPedido, request.Role))
                            .ReturnsAsync(existingConfiguration);

            var expectedError = ErrorMessages.CannotCreateConfigurationForRoleThatAlreadyExistsForProduct.Build(request.CodigoPedido, request.Role);

            var useCase = GetSut();

            // Act
            var response = await useCase.Execute(request);

            // Assert
            response.Should().NotBeNull();
            response.Success().Should().BeFalse();
            response.Errors.Should().Contain(t => t.Message == expectedError.Message && t.Code == expectedError.Code);
            mocker.VerifyAll();
        }

        [Fact]
        public async Task Execute_WhenProductWasNotFound_ReturnsBadRequest()
        {
            // Arrange
            var request = new CreateConfigurationRequestBuilder().Build();

            mocker.GetMock<IProductRepository>().Setup(x => x.Get(request.CodigoPedido));

            var expectedError = ErrorMessages.ProductNotFoundWithId.Build(request.CodigoPedido);

            var useCase = GetSut();

            // Act
            var response = await useCase.Execute(request);

            // Assert
            response.Should().NotBeNull();
            response.Success().Should().BeFalse();
            response.Errors.Should().Contain(t => t.Message == expectedError.Message && t.Code == expectedError.Code);
            mocker.VerifyAll();
        }
    }
}
