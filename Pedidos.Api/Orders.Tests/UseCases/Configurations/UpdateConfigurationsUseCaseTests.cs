using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Orders.Borders.Dtos.Internal.Configurations;
using Orders.Borders.Entities.Configurations;
using Orders.Borders.Enums;
using Orders.Borders.Repositories.Internal;
using Orders.Borders.Shared;
using Orders.Tests.Builders.Entities.Configurations;
using Orders.UseCases.Configurations;
using Xunit;

namespace Orders.Tests.UseCases.Configurations
{
    public class UpdateConfigurationsUseCaseTests
    {
        private readonly AutoMocker mocker = new();

        private UpdateConfigurationUseCase GetSut() => mocker.CreateInstance<UpdateConfigurationUseCase>();

        [Fact]
        public async Task Execute_WhenDoesNotFindConfiguration_ReturnsNotFound()
        {
            // Arrange
            var useCase = GetSut();
            var configurationId = Guid.NewGuid();
            var expectedError = ErrorMessages.ConfigurationNotFoundWithId.Build(configurationId);

            // Act
            var response = await useCase.Execute((configurationId, new UpdateConfigurationRequest()));

            // Assert
            response.Should().NotBeNull();
            response.Success().Should().BeFalse();
            response.Status.Should().Be(UseCaseResponseKind.NotFound);
            response.Errors.Should().Contain(t => t.Message == expectedError.Message && t.Code == expectedError.Code);
        }

        [Fact]
        public async Task Execute_WhenConfigurationExists_UpdatesConfiguration()
        {
            // Arrange
            var configurationId = Guid.NewGuid();

            var existingConfiguration = new ConfigurationBuilder(role: Role.Beneficiary)
                                                .WithId(configurationId)
                                                .WithOnboardingSteps([OnboardingStep.Legacy])
                                                .WithOnboardingCorporateRequirements([OnboardingRequirement.Address])
                                                .WithOnboardingIndividualRequirements([OnboardingRequirement.Address])
                                                .Build();

            var request = new UpdateConfigurationRequest()
            {
                OnboardingSteps = new OnboardingStep[] { OnboardingStep.Appointments, OnboardingStep.CommercialConditions },
                OnboardingCorporateRequirements = new OnboardingRequirement[] { OnboardingRequirement.Email, OnboardingRequirement.BankingReference },
                OnboardingIndividualRequirements = new OnboardingRequirement[] { OnboardingRequirement.SusepBrokerRegisterActive, OnboardingRequirement.IndividualSocialName, OnboardingRequirement.IndividualBirthDate },
                DefaultAppointments = new AppointmentConfigurationRequest[] { new() { PersonId = Guid.NewGuid(), Role = Role.Broker } }
            };

            var repositoryMock = mocker.GetMock<IConfigurationRepository>();
            repositoryMock.Setup(r => r.Get(configurationId))
                          .ReturnsAsync(existingConfiguration);

            Configuration? sentConfig = null;

            repositoryMock.Setup(r => r.Update(It.IsAny<Configuration>(), It.IsAny<UpdateConfigurationRequest>())).Callback<Configuration, UpdateConfigurationRequest>((t, i) => sentConfig = t);

            var useCase = GetSut();

            // Act
            var response = await useCase.Execute((configurationId, request));

            // Assert
            repositoryMock.VerifyAll();
            response.Should().NotBeNull();
            response.Success().Should().BeTrue();
            sentConfig.Should().NotBeNull();
            sentConfig!.OnboardingSteps.Should().BeEquivalentTo(request.OnboardingSteps);
            sentConfig!.OnboardingCorporateRequirements.Should().BeEquivalentTo(request.OnboardingCorporateRequirements);
            sentConfig!.OnboardingIndividualRequirements.Should().BeEquivalentTo(request.OnboardingIndividualRequirements);
        }
    }
}
