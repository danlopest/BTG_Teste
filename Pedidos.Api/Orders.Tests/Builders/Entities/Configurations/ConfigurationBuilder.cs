using Bogus;
using Orders.Borders.Entities.Configurations;
using Orders.Borders.Enums;
using System.Data;

namespace Orders.Tests.Builders.Entities.Configurations
{
    public class ConfigurationBuilder
    {
        private readonly Faker<Configuration> instance;

        public ConfigurationBuilder(Guid? productId = null, Role? role = null)
        {
            instance = new AutoFaker<Configuration>()
                .RuleFor(x => x.OnboardingSteps, faker => faker.Make(1, () => faker.PickRandom<OnboardingStep>()))
                .RuleFor(x => x.OnboardingCorporateRequirements, faker => faker.Make(1, () => faker.PickRandom<OnboardingRequirement>()))
                .RuleFor(x => x.OnboardingIndividualRequirements, faker => faker.Make(1, () => faker.PickRandom<OnboardingRequirement>()));

            if (productId != null)
                instance.RuleFor(x => x.ProductId, productId);

            if (role != null)
                instance.RuleFor(x => x.Role, role);
        }

        public Configuration Build() => instance;

        public ConfigurationBuilder WithOnboardingSteps(OnboardingStep[]? onboardingSteps)
        {
            instance.RuleFor(x => x.OnboardingSteps, onboardingSteps);
            return this;
        }
        public ConfigurationBuilder WithId(Guid id)
        {
            instance.RuleFor(x => x.Id, id);
            return this;
        }

        public ConfigurationBuilder WithOnboardingCorporateRequirements(OnboardingRequirement[]? requirements)
        {
            instance.RuleFor(x => x.OnboardingCorporateRequirements, requirements);
            return this;
        }
        public ConfigurationBuilder WithOnboardingIndividualRequirements(OnboardingRequirement[]? requirements)
        {
            instance.RuleFor(x => x.OnboardingIndividualRequirements, requirements);
            return this;
        }

        public ConfigurationBuilder WithDefaultAppointments(Role[] roles)
        {
            var appointments = roles.Select(t => new AppointmentConfigurationBuilder(role: t).Build());
            instance.RuleFor(x => x.DefaultAppointments, appointments);

            return this;
        }
    }
}
