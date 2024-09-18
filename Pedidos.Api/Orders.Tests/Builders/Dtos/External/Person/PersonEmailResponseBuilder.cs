using Bogus;
using Orders.Borders.Dtos.External.Person;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.External.Person
{
    public class PersonEmailResponseBuilder
    {
        private readonly Faker<PersonEmailResponse> instance;

        public PersonEmailResponseBuilder()
        {
            instance = new AutoFaker<PersonEmailResponse>()
                .RuleFor(x => x.Type, faker => faker.PickRandom<EmailType>())
                .RuleFor(x => x.EmailAddress, faker => faker.Internet.Email());
        }

        public PersonEmailResponse Build() => instance;
    }
}
