using Orders.Borders.Dtos.Internal.Configurations;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Configurations
{
    public class CreateConfigurationRequestBuilder
    {
        private readonly OrderRequest instance;

        public CreateConfigurationRequestBuilder(Guid? productId = null, Role? role = null)
        {
            if (productId == null)
                productId = Guid.NewGuid();

            if (role == null)
                role = Role.PolicyOwner;

            instance = new OrderRequest()
            {
                CodigoPedido = productId.Value,
                Role = role.Value,
                CreatedBy = "test.test",
                OnboardingSteps = new OnboardingStep[] { OnboardingStep.Legacy, OnboardingStep.Appointments },
                OnboardingCorporateRequirements = new OnboardingRequirement[] { OnboardingRequirement.Address },
                OnboardingIndividualRequirements = new OnboardingRequirement[] { OnboardingRequirement.IndividualSocialName },
                DefaultAppointments = new AppointmentConfigurationRequest[] {
                    new()
                    {
                        PersonId = Guid.NewGuid(),
                        Role = Role.PolicyHolder
                    }
                }
            };
        }

        public OrderRequest Build() => instance;
    }
}
