using Bogus;
using Bogus.Extensions.Brazil;
using Orders.Borders.Dtos.External.Person;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.External.Person
{
    public class PersonResponseBuilder
    {
        private readonly Faker<PersonResponse> instance;

        public PersonResponseBuilder(PersonType personType = PersonType.Individual, bool isExistingPerson = false)
        {
            instance = new AutoFaker<PersonResponse>()
                .RuleFor(p => p.Id, f => f.Random.Guid())
                .RuleFor(p => p.Name, f => f.Person.FullName)
                .RuleFor(p => p.Nickname, f => f.Person.UserName)
                .RuleFor(p => p.AbbreviatedName, f => f.Person.FirstName)
                .RuleFor(p => p.Documents, f => f.Make(2, () => new PersonDocumentResponseBuilder().Build()))
                .RuleFor(p => p.Addresses, f => f.Make(2, () => new PersonAddressResponseBuilder().Build()))
                .RuleFor(p => p.Emails, f => f.Make(2, () => new PersonEmailResponseBuilder().Build()))
                .RuleFor(p => p.Phones, f => f.Make(2, () => new PersonPhoneResponseBuilder().Build()))
                .RuleFor(p => p.BankingReferences, f => f.Make(2, () => new PersonBankingReferenceResponseBuilder().Build()))
                .RuleFor(p => p.Contacts, f => f.Make(2, () => new PersonContactResponseBuilder().Build()))
                .RuleFor(p => p.PersonsInsuranceSegments, f => f.Make(2, () => new PersonInsuranceSegmentResponseBuilder().Build()))
                .RuleFor(p => p.CreatedAt, f => f.Date.Past());

            instance.RuleFor(p => p.DocumentNumber, f => personType == PersonType.Individual ? f.Person.Cpf(false) : f.Company.Cnpj(false));

            if (!isExistingPerson)
                instance.RuleFor(p => p.Id, (object?)null);
        }

        public PersonResponse Build() => instance.Generate();
    }
}
