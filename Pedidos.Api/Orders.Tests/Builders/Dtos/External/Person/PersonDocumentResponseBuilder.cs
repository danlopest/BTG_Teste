using Bogus;
using Orders.Borders.Dtos.External.Person;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.External.Person
{
    public class PersonDocumentResponseBuilder
    {
        private readonly Faker<PersonDocumentResponse> instance;

        public PersonDocumentResponseBuilder()
        {
            instance = new AutoFaker<PersonDocumentResponse>()
                .RuleFor(p => p.DocumentNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(p => p.ExpirationAt, f => f.Date.Future())
                .RuleFor(p => p.Type, f => f.PickRandom<DocumentType>())
                .RuleFor(p => p.IssuanceCountry, f => f.Address.Country());
        }

        public PersonDocumentResponse Build() => instance;
    }
}
