using Bogus;
using Orders.Borders.Entities.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Entities.Onboarding
{
    public class OnboardingProcessStepAppointmentBuilder
    {
        private readonly Faker<OnboardingProcessStepAppointment> instance;

        public OnboardingProcessStepAppointmentBuilder()
        {
            instance = new AutoFaker<OnboardingProcessStepAppointment>()
                .RuleFor(x => x.DocumentNumber, "00000000000191");
        }

        public OnboardingProcessStepAppointmentBuilder WithStatus(OnboardingStatus onboardingStatus)
        {
            instance.RuleFor(x => x.Status, onboardingStatus);
            return this;
        }

        public OnboardingProcessStepAppointmentBuilder WithPersonId(Guid? personId)
        {
            instance.RuleFor(x => x.PersonId, personId);
            return this;
        }

        public OnboardingProcessStepAppointmentBuilder WithDocumentNumber(string? documentNumber)
        {
            instance.RuleFor(x => x.DocumentNumber, documentNumber);
            return this;
        }

        public OnboardingProcessStepAppointmentBuilder WithRole(Role role)
        {
            instance.RuleFor(x => x.Role, role);
            return this;
        }

        public OnboardingProcessStepAppointment Build() => instance;
    }
}
