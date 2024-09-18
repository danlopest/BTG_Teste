using Bogus;
using Orders.Borders.Dtos.External.Person;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.External.Person
{
    public class PersonAddressResponseBuilder
    {
        private readonly Faker<PersonAddressResponse> instance;

        public PersonAddressResponseBuilder()
        {
            instance = new AutoFaker<PersonAddressResponse>()
                .RuleFor(p => p.Type, f => f.PickRandom<AddressType>())
                .RuleFor(p => p.Street, f => f.Address.StreetAddress())
                .RuleFor(p => p.Number, f => f.Address.BuildingNumber())
                .RuleFor(p => p.Complement, f => f.Address.SecondaryAddress())
                .RuleFor(p => p.District, f => f.Address.County())
                .RuleFor(p => p.City, f => f.Address.City())
                .RuleFor(p => p.State, f => f.Address.State())
                .RuleFor(p => p.ZipCode, f => f.Address.ZipCode())
                .RuleFor(p => p.Country, f => f.Address.Country());
        }

        public PersonAddressResponse Build() => instance;
    }
}
