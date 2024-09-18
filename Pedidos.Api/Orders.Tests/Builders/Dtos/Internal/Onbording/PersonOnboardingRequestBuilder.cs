using Bogus;
using Bogus.Extensions.Brazil;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class PersonOnboardingRequestBuilder
    {
        private readonly Faker<PersonOnboardingRequest> instance;

        public PersonOnboardingRequestBuilder(PersonType type = PersonType.Individual)
        {
            instance = new AutoFaker<PersonOnboardingRequest>()
                .RuleFor(t => t.Type, type)
                .RuleFor(t => t.Emails, faker => faker.Make(2, new PersonEmailRequestBuilder().Build))
                .RuleFor(t => t.Phones, faker => faker.Make(2, new PersonPhoneRequestBuilder().Build))
                .RuleFor(t => t.Addresses, faker => faker.Make(3, new PersonAddressRequestBuilder().Build))
                .RuleFor(t => t.Documents, faker => faker.Make(3, new PersonDocumentRequestBuilder().Build))
                .RuleFor(t => t.Contacts, faker => faker.Make(1, new PersonContactRequestBuilder().Build))
                .RuleFor(t => t.BankingReferences, faker => faker.Make(2, new PersonBankingReferenceRequestBuilder().Build))
                .RuleFor(t => t.InsuranceSegments, faker => faker.Make(1, new PersonInsuranceSegmentRequestBuilder().Build));

            if (type == PersonType.Individual)
            {
                instance.RuleFor(t => t.DocumentNumber, faker => faker.Person.Cpf(false));
                instance.RuleFor(t => t.Name, faker => faker.Person.FullName);
                instance.RuleFor(t => t.SocialName, faker => faker.Person.FullName);
                instance.RuleFor(t => t.Gender, faker => faker.PickRandom<Gender>());
                instance.RuleFor(t => t.BirthDate, faker => faker.Date.Past());
            }
            else
            {
                instance.RuleFor(t => t.DocumentNumber, faker => faker.Company.Cnpj(false));
                instance.RuleFor(t => t.Name, faker => faker.Company.CompanyName());
            }
        }

        public PersonOnboardingRequest Build() => instance;
    }
}
