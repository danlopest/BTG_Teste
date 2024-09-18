using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Orders.Borders.Dtos.Internal.Configurations;
using Orders.Borders.Enums;
using Orders.Borders.Repositories.Internal;
using Orders.Borders.Shared;
using Orders.Tests.Builders.Entities.Configurations;
using Orders.UseCases.Configurations;
using Xunit;

namespace Orders.Tests.UseCases.Configurations
{
    public class ListConfigurationsUseCaseTests
    {
        private readonly AutoMocker mocker = new();

        private ListOrderByClientIdUseCase GetSut() => mocker.CreateInstance<ListOrderByClientIdUseCase>();

        [Fact]
        public async Task Execute_ReturnsConfigurationsResponses_WhenHasConfigurationsForProductId()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var repository = mocker.GetMock<IConfigurationRepository>();
            var repositoryResult = Enum.GetValues<Role>().Select(t => new ConfigurationBuilder(productId, t).Build()).ToList();
            repository.Setup(t => t.List(productId)).ReturnsAsync(repositoryResult);

            //Act
            var result = await GetSut().Execute(productId);

            //Assert
            repository.VerifyAll();
            result.Should().NotBeNull();
            result.Status.Should().Be(UseCaseResponseKind.Success);
            result.Result.Should().BeEquivalentTo(repositoryResult.Select(t => new ConfigurationResponse(t)));
        }
    }
}
