using FluentAssertions;
using Orders.Borders.Dtos.Shared.Interfaces;
using Orders.Borders.Enums;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Shared;
using Orders.Tests.Builders.Dtos.Internal.Onbording;
using Xunit;

namespace Orders.Tests.Validators.Shared
{
    public class InsuranceSegmentValidatorTests : BaseValidatorTests<InsuranceSegmentValidator, IInsuranceSegment>
    {
        [Fact]
        public void Validate_WhenInsuranceSegmentIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.InvalidInsuranceSegment;
            var request = new PersonInsuranceSegmentRequestBuilder().Build() with
            {
                Segment = (InsuranceSegment)2000
            };

            ActAndAssertFail(request, expectedError);
        }

    }
}
