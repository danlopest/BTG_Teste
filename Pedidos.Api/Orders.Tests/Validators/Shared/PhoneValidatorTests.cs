using FluentAssertions;
using Orders.Borders.Dtos.Shared.Interfaces;
using Orders.Borders.Enums;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Shared;
using Orders.Tests.Builders.Dtos.Internal.Onbording;
using Xunit;

namespace Orders.Tests.Validators.Shared
{
    public class PhoneValidatorTests : BaseValidatorTests<PhoneValidator, IPhone>
    {
        [Fact]
        public void Validate_WhenPhoneTypeIsValid_IsValid()
        {
            var request = new PersonPhoneRequestBuilder().Build();

            ActAndAssertSuccess(request);
        }

        [Fact]
        public void Validate_WhenPhoneTypeIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.InvalidPhoneType;
            var request = new PersonPhoneRequestBuilder().Build() with
            {
                Type = (PhoneType)2000
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenPhoneNumberIsEmpty_IsInvalid()
        {
            var expectedError = ErrorMessages.ValidPhoneNumberIsRequired;
            var request = new PersonPhoneRequestBuilder().Build() with
            {
                Number = ""
            };

            ActAndAssertFail(request, expectedError);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("123456789012345678901")]
        public void Validate_WhenPhoneNumberLengthIsInvalid_IsInvalid(string phoneNumber)
        {
            var expectedError = phoneNumber.Length < 3 ? ErrorMessages.PhoneNumberMinLength : ErrorMessages.PhoneNumberMaxLength;
            var request = new PersonPhoneRequestBuilder()
                .Build() with
            {
                Number = phoneNumber
            };

            ActAndAssertFail(request, expectedError);
        }
    }
}
