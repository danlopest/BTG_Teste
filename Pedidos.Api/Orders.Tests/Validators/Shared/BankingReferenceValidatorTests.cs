using FluentAssertions;
using Orders.Borders.Dtos.Shared.Interfaces;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Shared;
using Orders.Tests.Builders.Dtos.Internal.Onbording;
using Xunit;

namespace Orders.Tests.Validators.Shared
{
    public class BankingReferenceValidatorTests : BaseValidatorTests<BankingReferenceValidator, IBankingReference>
    {
        [Fact]
        public void Validate_WhenFinancialInstitutionIdIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.FinancialInstitutionIdInvalid;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                FinancialInstitutionId = Guid.Empty
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenBranchExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.BranchMaxLengthExceeded;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                Branch = new string('x', 5)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenBranchContainsLetters_IsInvalid()
        {
            var expectedError = ErrorMessages.BranchMustBeDigitsOrX;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                Branch = "ABCD"
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenAccountNumberIsEmpty_IsInvalid()
        {
            var expectedError = ErrorMessages.AccountNumberIsRequired;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                AccountNumber = ""
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenAccountNumberExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.AccountNumberMaxLengthExceeded;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                AccountNumber = new string('x', 14)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenAccountNumberContainsLetters_IsInvalid()
        {
            var expectedError = ErrorMessages.AccountNumberMustBeDigitsOrX;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                AccountNumber = "ABCD"
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenAccountDigitIsEmpty_IsInvalid()
        {
            var expectedError = ErrorMessages.AccountDigitIsRequired;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                AccountDigit = ""
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenAccountDigitExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.AccountDigitMaxLengthExceeded;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                AccountDigit = new string('x', 3)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenAccountDigitIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.AccountDigitInvalid;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                AccountDigit = "D"
            };

            ActAndAssertFail(request, expectedError);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("X")]
        [InlineData("P")]
        public void Validate_WhenAccountDigitIsValid_Valid(string digit)
        {
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                AccountDigit = digit
            };
            ActAndAssertSuccess(request);
        }

        [Fact]
        public void Validate_WhenAccountExtensionExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.AccountExtensionMaxLengthExceeded;
            var request = new PersonBankingReferenceRequestBuilder().Build() with
            {
                AccountExtension = new string('x', 5)
            };

            ActAndAssertFail(request, expectedError);
        }

    }
}
