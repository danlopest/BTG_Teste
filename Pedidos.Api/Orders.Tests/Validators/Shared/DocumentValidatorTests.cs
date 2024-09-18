using FluentAssertions;
using Orders.Borders.Dtos.Shared.Interfaces;
using Orders.Borders.Enums;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Shared;
using Orders.Tests.Builders.Dtos.Internal.Onbording;
using Xunit;

namespace Orders.Tests.Validators.Shared
{
    public class DocumentValidatorTests : BaseValidatorTests<DocumentValidator, IDocument>
    {
        [Fact]
        public void Validate_WhenIssuanceAtOutOfRange_IsInvalid()
        {
            var expectedError = ErrorMessages.DocumentIssuanceAtOutOfRange;
            var request = new PersonDocumentRequestBuilder().Build() with
            {
                IssuanceAt = DateTime.MinValue
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenExpirationAtOutOfRange_IsInvalid()
        {
            var expectedError = ErrorMessages.DocumentExpirationAtOutOfRange;
            var request = new PersonDocumentRequestBuilder().Build() with
            {
                ExpirationAt = DateTime.MaxValue
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenIssuanceCountryMaxLengthExceeded_IsInvalid()
        {
            var expectedError = ErrorMessages.DocumentIssuanceCountryMaxLengthExceeded;
            var request = new PersonDocumentRequestBuilder().Build() with
            {
                IssuanceCountry = "ABCDEF"
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenDocumentNumberIsEmpty_IsInvalid()
        {
            var expectedError = ErrorMessages.DocumentNumberIsRequired;
            var request = new PersonDocumentRequestBuilder().Build() with
            {
                DocumentNumber = ""
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenDocumentNumberMaxLengthExceeded_IsInvalid()
        {
            var expectedError = ErrorMessages.DocumentNumberMaxLengthExceeded;
            var request = new PersonDocumentRequestBuilder().Build() with
            {
                DocumentNumber = new string('X', 51)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenDocumentTypeIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.DocumentTypeInvalid;
            var request = new PersonDocumentRequestBuilder().Build() with
            {
                Type = (DocumentType)2000
            };

            ActAndAssertFail(request, expectedError);
        }
    }
}
