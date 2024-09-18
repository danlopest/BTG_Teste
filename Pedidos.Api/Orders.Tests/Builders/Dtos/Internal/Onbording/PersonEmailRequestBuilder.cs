using Bogus;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class PersonEmailRequestBuilder
    {
        private readonly Faker<PersonEmailRequest> instance;

        public PersonEmailRequestBuilder()
        {
            instance = new AutoFaker<PersonEmailRequest>()
                .RuleFor(t => t.EmailAddress, f => f.Internet.Email())
                .RuleFor(t => t.Type, f => f.PickRandom<EmailType>());
        }

        public PersonEmailRequest Build() => instance.Generate();
    }
}
