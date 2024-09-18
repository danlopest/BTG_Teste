using Bogus;
using Orders.Borders.Dtos.External.Person;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.External.Person
{
    public class PersonPhoneResponseBuilder
    {
        private readonly Faker<PersonPhoneResponse> instance;

        public PersonPhoneResponseBuilder()
        {
            instance = new AutoFaker<PersonPhoneResponse>()
                .RuleFor(p => p.Type, f => f.PickRandom<PhoneType>())
                .RuleFor(p => p.Number, f => f.Phone.PhoneNumber());
        }

        public PersonPhoneResponse Build() => instance;
    }
}
