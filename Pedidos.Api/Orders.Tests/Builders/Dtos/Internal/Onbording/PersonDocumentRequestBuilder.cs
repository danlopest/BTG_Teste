using Bogus;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class PersonDocumentRequestBuilder
    {
        private readonly Faker<PersonDocumentRequest> instance;

        public PersonDocumentRequestBuilder()
        {
            instance = new AutoFaker<PersonDocumentRequest>()
                .RuleFor(t => t.Type, f => f.PickRandom<DocumentType>())
                .RuleFor(t => t.DocumentNumber, f => f.Random.ReplaceNumbers("##############"))
                .RuleFor(t => t.IssuanceCountry, f => f.Address.CountryCode());
        }

        public PersonDocumentRequest Build() => instance.Generate();
    }
}
