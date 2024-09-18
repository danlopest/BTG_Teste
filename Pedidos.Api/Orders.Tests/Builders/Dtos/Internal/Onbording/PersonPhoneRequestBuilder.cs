using Bogus;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class PersonPhoneRequestBuilder
    {
        private readonly Faker<PersonPhoneRequest> instance;

        public PersonPhoneRequestBuilder()
        {
            instance = new AutoFaker<PersonPhoneRequest>()
                .RuleFor(t => t.Number, f => f.Phone.PhoneNumber())
                .RuleFor(t => t.Type, f => f.PickRandom<PhoneType>());
        }

        public PersonPhoneRequest Build() => instance.Generate();
    }
}
