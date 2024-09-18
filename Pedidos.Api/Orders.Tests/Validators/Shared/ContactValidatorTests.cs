using FluentAssertions;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Dtos.Shared.Interfaces;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Shared;
using Orders.Tests.Builders.Dtos.Internal.Onbording;
using Xunit;

namespace Orders.Tests.Validators.Shared
{
    public class ContactValidatorTests : BaseValidatorTests<ContactValidator, IContact>
    {

        [Theory]
        [MemberData(nameof(ValidTestData))]
        public void Validate_WhenIsValid_IsValid(PersonContactRequest request)
        {
            ActAndAssertSuccess(request);
        }

        [Fact]
        public void Validate_WhenContactInformationIsMissing_IsInvalid()
        {
            var expectedError = ErrorMessages.ContactInformationMissing;
            var request = new PersonContactRequestBuilder().Build() with
            {
                Email = null,
                CellPhoneNumber = null,
                PhoneNumber = null
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenNameIsEmpty_IsInvalid()
        {
            var expectedError = ErrorMessages.ContactNameIsRequired;
            var request = new PersonContactRequestBuilder().Build() with
            {
                Name = ""
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenNameExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.ContactNameMaxLengthExceeded;
            var request = new PersonContactRequestBuilder().Build() with
            {
                Name = new string('x', 201)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenPhoneNumberExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.ContactPhoneNumberMaxLengthExceeded;
            var request = new PersonContactRequestBuilder().Build() with
            {
                PhoneNumber = new string('x', 51)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenCellPhoneNumberExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.ContactCellPhoneNumberMaxLengthExceeded;
            var request = new PersonContactRequestBuilder().Build() with
            {
                CellPhoneNumber = new string('x', 51)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenOfficeExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.ContactOfficeMaxLengthExceeded;
            var request = new PersonContactRequestBuilder().Build() with
            {
                Office = new string('x', 51)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenEmailIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.ContactEmailInvalid;
            var request = new PersonContactRequestBuilder().Build() with
            {
                Email = "Joao do caminhao"
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenEmailExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.ContactEmailMaxLengthExceeded;
            var request = new PersonContactRequestBuilder().Build() with
            {
                Email = new string('x', 201)
            };

            ActAndAssertFail(request, expectedError);
        }

        public static TheoryData<PersonContactRequest> ValidTestData() =>
        new()
        {
            { new PersonContactRequestBuilder().Build() },
            { new PersonContactRequestBuilder().Build() with {
                CellPhoneNumber = null,
                PhoneNumber = null
                }
            },
            { new PersonContactRequestBuilder().Build() with {
                Email = null,
                PhoneNumber = null
                }
            },
            { new PersonContactRequestBuilder().Build() with {
                Email = null,
                CellPhoneNumber = null,
                }
            }
        };
    }
}
