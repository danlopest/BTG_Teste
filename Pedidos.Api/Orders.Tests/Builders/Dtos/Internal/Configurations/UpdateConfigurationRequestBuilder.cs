using Orders.Borders.Dtos.Internal.Configurations;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Configurations
{
    public class UpdateConfigurationRequestBuilder
    {
        private UpdateConfigurationRequest instance;

        public UpdateConfigurationRequestBuilder()
        {
            instance = new UpdateConfigurationRequest()
            {
                UpdatedBy = "test.test",
                OnboardingSteps = new OnboardingStep[] { OnboardingStep.Legacy },
                OnboardingCorporateRequirements = new OnboardingRequirement[] { OnboardingRequirement.Address },
                OnboardingIndividualRequirements = new OnboardingRequirement[] { OnboardingRequirement.IndividualSocialName },
            };
        }

        public UpdateConfigurationRequest Build() => instance;

        public UpdateConfigurationRequestBuilder WithOnboardingSteps(OnboardingStep[]? onboardingSteps)
        {
            instance = instance with { OnboardingSteps = onboardingSteps };
            return this;
        }

        public UpdateConfigurationRequestBuilder WithDefaultAppointments(Role[] roles)
        {
            instance = instance with { DefaultAppointments = roles.Select(x => new AppointmentConfigurationRequest() { PersonId = Guid.NewGuid(), Role = x }) };
            return this;
        }
    }
}
