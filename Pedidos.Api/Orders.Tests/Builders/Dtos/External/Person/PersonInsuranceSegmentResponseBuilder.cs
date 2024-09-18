using Bogus;
using Orders.Borders.Dtos.External.Person;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.External.Person
{
    public class PersonInsuranceSegmentResponseBuilder
    {
        private readonly Faker<PersonInsuranceSegmentResponse> instance;

        public PersonInsuranceSegmentResponseBuilder()
        {
            instance = new AutoFaker<PersonInsuranceSegmentResponse>().RuleFor(t => t.Segment, faker => faker.PickRandom<InsuranceSegment>());
        }

        public PersonInsuranceSegmentResponse Build() => instance;
    }
}
