using Bogus;
using Orders.Borders.Dtos.External.Person;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.External.Person
{
    public class PersonBankingReferenceResponseBuilder
    {
        private readonly Faker<PersonBankingReferenceResponse> instance;

        public PersonBankingReferenceResponseBuilder()
        {
            instance = new AutoFaker<PersonBankingReferenceResponse>()
                .RuleFor(p => p.Branch, f => f.Finance.AccountName())
                .RuleFor(p => p.AccountNumber, f => f.Finance.Account(10))
                .RuleFor(p => p.AccountDigit, f => f.Random.Number(0, 9).ToString())
                .RuleFor(p => p.AccountExtension, f => f.Finance.Account(3))
                .RuleFor(p => p.Type, f => f.PickRandom<BankingReferenceType>());
        }

        public PersonBankingReferenceResponse Build() => instance;
    }
}
