using Bogus;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class PersonInsuranceSegmentRequestBuilder
    {
        private readonly Faker<PersonInsuranceSegmentRequest> instance;

        public PersonInsuranceSegmentRequestBuilder()
        {
            instance = new AutoFaker<PersonInsuranceSegmentRequest>()
                .RuleFor(p => p.Segment, f => f.PickRandom<InsuranceSegment>());
        }

        public PersonInsuranceSegmentRequest Build() => instance.Generate();
    }
}
