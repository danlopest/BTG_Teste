using Bogus;
using Orders.Borders.Entities.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Entities.Onboarding
{
    public class OnboardingProcessBuilder
    {
        private readonly Faker<OnboardingProcess> instance;

        public OnboardingProcessBuilder()
        {
            instance = new AutoFaker<OnboardingProcess>()
                .RuleFor(x => x.Steps, new List<OnboardingProcessStep> { new OnboardingProcessStepBuilder().Build() })
                .RuleFor(x => x.DocumentNumber, "00000000000191");
        }

        public OnboardingProcessBuilder WithSteps(IEnumerable<OnboardingProcessStep>? steps)
        {
            instance.RuleFor(x => x.Steps, steps);
            return this;
        }

        public OnboardingProcessBuilder WithRole(Role role)
        {
            instance.RuleFor(x => x.Role, role);
            return this;
        }

        public OnboardingProcessBuilder WithStatus(OnboardingStatus status)
        {
            instance.RuleFor(x => x.Steps, new List<OnboardingProcessStep> { new OnboardingProcessStepBuilder().WithStatus(status).Build() });
            return this;
        }

        public OnboardingProcess Build() => instance;
    }
}