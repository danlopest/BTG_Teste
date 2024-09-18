using FluentAssertions;
using Moq;
using Orders.Borders.Dtos.HealthCheck;
using Orders.Borders.Repositories;
using Orders.Borders.Shared;
using Orders.UseCases.HealthCheck;
using Xunit;

namespace Orders.Tests.UseCases.HealthCheck
{
    public class GetHealthCheckStatusUseCaseTests
    {
        private readonly List<Mock<IRepository>> repositories;
        private readonly GetHealthCheckStatusUseCase useCase;
        private const string buildId = "TesteBuildId";

        public GetHealthCheckStatusUseCaseTests()
        {
            repositories = [new Mock<IRepository>(), new Mock<IRepository>(), new Mock<IRepository>()];

            repositories[0].Setup(x => x.Name).Returns("Database");

            foreach (var repository in repositories.Skip(1))
            {
                repository.Setup(x => x.Name).Returns($"ExternalService_{Guid.NewGuid()}");
                repository.Setup(x => x.External).Returns(true);
            }

            var applicationConfig = new ApplicationConfig { BuildId = buildId };

            useCase = new GetHealthCheckStatusUseCase(
                repositories.Select(x => x.Object),
                applicationConfig
            );
        }

        [Fact]
        public async Task Execute_ReturnsSuccess_WhenIsNotFullCheckAndInternalRepositoriesAreHealth()
        {
            // Arrange
            var expectedHealthCheckActivities = new List<HealthCheckActivity>();

            foreach (var repository in repositories.Where(x => !x.Object.External))
            {
                repository.Setup(it => it.CheckHealth()).ReturnsAsync(true);
                expectedHealthCheckActivities.Add(new HealthCheckActivity(repository.Object.Name, true));
            }

            // Act
            var response = await useCase.Execute(false);

            // Assert
            AssertResult(response, UseCaseResponseKind.Success, expectedHealthCheckActivities);
        }

        [Fact]
        public async Task Execute_ReturnsSuccess_WhenIsFullCheckAndAllExternalRepositoriesAreHealth()
        {
            // Arrange
            var expectedHealthCheckActivities = new List<HealthCheckActivity>();

            foreach (var repository in repositories)
            {
                repository.Setup(it => it.CheckHealth()).ReturnsAsync(true);
                expectedHealthCheckActivities.Add(new HealthCheckActivity(repository.Object.Name, true));
            }

            // Act
            var response = await useCase.Execute(true);

            // Assert
            AssertResult(response, UseCaseResponseKind.Success, expectedHealthCheckActivities);
        }

        [Fact]
        public async Task Execute_ReturnsUnavailable_WhenInternalRepositoriesAreUnhealthy()
        {
            // Arrange
            var expectedHealthCheckActivities = repositories.Where(x => !x.Object.External).Select(x => new HealthCheckActivity(x.Object.Name, false));

            // Act
            var response = await useCase.Execute(false);

            // Assert
            AssertResult(response, UseCaseResponseKind.Unavailable, expectedHealthCheckActivities);
        }

        [Fact]
        public async Task Execute_ReturnsUnavailable_WhenIsFullCheckAndAnyExternalRepositoryAreUnhealthy()
        {
            // Arrange
            var expectedHealthCheckActivities = new List<HealthCheckActivity>();

            foreach (var repository in repositories)
            {
                repository.Setup(it => it.CheckHealth()).ReturnsAsync(!repository.Object.External);
                expectedHealthCheckActivities.Add(new HealthCheckActivity(repository.Object.Name, !repository.Object.External));
            }

            // Act
            var response = await useCase.Execute(true);

            // Assert
            AssertResult(response, UseCaseResponseKind.Unavailable, expectedHealthCheckActivities);
        }

        private static void AssertResult(UseCaseResponse<HealthCheckStatus> response, UseCaseResponseKind useCaseResponseKind, IEnumerable<HealthCheckActivity> expectedHealthCheckActivities)
        {
            response.Status.Should().Be(useCaseResponseKind);
            response.Result!.Activities.Should().HaveCount(expectedHealthCheckActivities.Count());
            response.Result.Activities.Should().BeEquivalentTo(expectedHealthCheckActivities);
            response.Result.BuildId.Should().Be(buildId);
            response.Result.Version.Should().NotBe(null);
        }
    }
}