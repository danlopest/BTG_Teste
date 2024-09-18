using Bogus;
using Orders.Borders.Dtos.Internal.Onboarding;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class PersonContactRequestBuilder
    {
        private readonly Faker<PersonContactRequest> instance;

        public PersonContactRequestBuilder()
        {
            instance = new AutoFaker<PersonContactRequest>()
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.CellPhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Office, f => f.Lorem.Word())
                .RuleFor(p => p.Email, f => f.Internet.Email());
        }

        public PersonContactRequest Build() => instance.Generate();
    }
}
