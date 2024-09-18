using Bogus;
using Orders.Borders.Entities.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Entities.Onboarding
{
    public class OnboardingProcessStepBuilder
    {
        private readonly Faker<OnboardingProcessStep> instance;

        public OnboardingProcessStepBuilder()
        {
            instance = new AutoFaker<OnboardingProcessStep>()
                .RuleFor(x => x.Appointments, new List<OnboardingProcessStepAppointment> { new OnboardingProcessStepAppointmentBuilder().Build() })
                .RuleFor(x => x.Errors, new List<OnboardingProcessStepError> { new OnboardingProcessStepErrorBuilder().Build() })
                .RuleFor(x => x.Step, OnboardingStep.Appointments);
        }

        public OnboardingProcessStepBuilder WithStatus(OnboardingStatus onboardingStatus)
        {
            instance.RuleFor(x => x.Status, onboardingStatus);
            return this;
        }

        public OnboardingProcessStepBuilder WithStep(OnboardingStep onboardingStep)
        {
            instance.RuleFor(x => x.Step, onboardingStep);
            return this;
        }

        public OnboardingProcessStepBuilder WithAppointments(IEnumerable<OnboardingProcessStepAppointment> onboardingProcessStepAppointments)
        {
            instance.RuleFor(x => x.Appointments, onboardingProcessStepAppointments);
            return this;
        }

        public OnboardingProcessStep Build() => instance;
    }
}