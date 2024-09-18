using Bogus;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders
{
    public class AutoFaker<T> : Faker<T> where T : class
    {
        public AutoFaker() : base("pt_BR")
        {
            RuleForType(typeof(Guid), faker => faker.Random.Guid())
            .RuleForType(typeof(string), faker => faker.Random.Word())
            .RuleForType(typeof(Role), faker => faker.PickRandom<Role>())
            .RuleForType(typeof(DateTime), faker => faker.Date.Past())
            .RuleForType(typeof(OnboardingStatus), faker => faker.PickRandom<OnboardingStatus>());
        }
    }
}
