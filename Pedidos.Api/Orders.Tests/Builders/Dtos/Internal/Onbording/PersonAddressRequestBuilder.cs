using Bogus;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class PersonAddressRequestBuilder
    {
        private readonly Faker<PersonAddressRequest> instance;

        public PersonAddressRequestBuilder()
        {
            instance = new AutoFaker<PersonAddressRequest>()
                .RuleFor(t => t.Street, f => f.Address.StreetAddress())
                .RuleFor(t => t.Complement, f => f.Address.SecondaryAddress())
                .RuleFor(t => t.Number, f => f.Address.BuildingNumber())
                .RuleFor(t => t.District, f => f.Address.County())
                .RuleFor(t => t.City, f => f.Address.City())
                .RuleFor(t => t.State, f => f.Address.StateAbbr())
                .RuleFor(t => t.ZipCode, f => f.Address.ZipCode())
                .RuleFor(t => t.Country, "BRA")
                .RuleFor(t => t.Type, f => f.PickRandom<AddressType>());
        }

        public PersonAddressRequest Build() => instance.Generate();
    }
}
