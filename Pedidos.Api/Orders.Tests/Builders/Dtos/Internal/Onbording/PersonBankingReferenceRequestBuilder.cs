using Bogus;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class PersonBankingReferenceRequestBuilder
    {
        private readonly Faker<PersonBankingReferenceRequest> instance;

        public PersonBankingReferenceRequestBuilder()
        {
            instance = new Faker<PersonBankingReferenceRequest>()
                .RuleFor(p => p.FinancialInstitutionId, f => f.Random.Guid())
                .RuleFor(p => p.Branch, f => f.Finance.Account(4))
                .RuleFor(p => p.AccountNumber, f => f.Finance.Account(10))
                .RuleFor(p => p.AccountDigit, f => f.Random.Number(0, 9).ToString())
                .RuleFor(p => p.AccountExtension, f => f.Finance.Account(3))
                .RuleFor(p => p.Type, f => f.PickRandom<BankingReferenceType>());
        }

        public PersonBankingReferenceRequest Build() => instance.Generate();
    }
}
