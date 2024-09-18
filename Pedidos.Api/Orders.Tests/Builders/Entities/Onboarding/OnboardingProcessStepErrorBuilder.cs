using Bogus;
using Orders.Borders.Entities.Onboarding;

namespace Orders.Tests.Builders.Entities.Onboarding
{
    public class OnboardingProcessStepErrorBuilder
    {
        private readonly Faker<OnboardingProcessStepError> instance;

        public OnboardingProcessStepErrorBuilder()
        {
            instance = new AutoFaker<OnboardingProcessStepError>();
        }

        public OnboardingProcessStepError Build() => instance;
    }
}
