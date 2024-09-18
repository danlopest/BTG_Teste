using Bogus;
using Orders.Borders.Dtos.External.Person;

namespace Orders.Tests.Builders.Dtos.External.Person
{
    public class PersonContactResponseBuilder
    {
        private readonly Faker<PersonContactResponse> instance;

        public PersonContactResponseBuilder()
        {
            instance = new AutoFaker<PersonContactResponse>()
                .RuleFor(p => p.Name, f => f.Person.FullName)
                .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.CellPhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Office, f => f.Company.CompanyName())
                .RuleFor(p => p.Email, f => f.Internet.Email());
        }

        public PersonContactResponse Build() => instance;
    }
}
