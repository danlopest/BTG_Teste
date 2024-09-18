using FluentAssertions;
using Orders.Borders.Dtos.Shared.Interfaces;
using Orders.Borders.Enums;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Shared;
using Orders.Tests.Builders.Dtos.Internal.Onbording;
using Xunit;

namespace Orders.Tests.Validators.Shared
{
    public class EmailValidatorTests : BaseValidatorTests<EmailValidator, IEmail>
    {
        [Fact]
        public void Validate_WhenEmailTypeIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.InvalidEmailType;
            var request = new PersonEmailRequestBuilder().Build() with
            {
                Type = (EmailType)2000
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenEmailAddressIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.InvalidEmail;
            var request = new PersonEmailRequestBuilder().Build() with
            {
                EmailAddress = "joao do caminhao"
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenEmailAddressMaxLengthExceeded_IsInvalid()
        {
            var expectedError = ErrorMessages.EmailAddressMaxLengthExceeded;
            var request = new PersonEmailRequestBuilder().Build() with
            {
                EmailAddress = new string('x', 101)
            };

            ActAndAssertFail(request, expectedError);
        }

    }
}
