using FluentAssertions;
using Orders.Borders.Shared;
using Orders.Borders.Validators;
using Xunit;

namespace Orders.Tests.Validators
{
    public class DocumentNumberValidatorTests
    {
        private readonly DocumentNumberValidator validator = new(nameof(DocumentNumberValidatorTests));
        private readonly ErrorMessage validatorError = ErrorMessages.InvalidDocumentNumber.Build(nameof(DocumentNumberValidatorTests));

        private const string ValidCnpj = "36770561000117";
        private const string ValidCpf = "32325316017";
        private void ValidateFailure(FluentValidation.Results.ValidationResult result)
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(t => t.ErrorMessage == validatorError.Message);
        }

        [Fact]
        public void Validate_WhenDocumentNumberIsEmpty_IsInvalid()
        {
            var result = validator.Validate("");
            ValidateFailure(result);
        }
        [Fact]
        public void Validate_WhenDocumentNumberIsInvalid_IsInvalid()
        {
            var result = validator.Validate("123");
            ValidateFailure(result);
        }

        [Fact]
        public void Validate_WhenDocumentNumberIsCPFAndInvalid_IsInvalid()
        {
            var result = validator.Validate("12345678901");
            ValidateFailure(result);
        }

        [Fact]
        public void Validate_WhenDocumentNumberIsCPFAndValid_IsValid()
        {
            var result = validator.Validate(ValidCpf);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenDocumentNumberIsCNPJAndInvalid_IsInvalid()
        {
            var result = validator.Validate("12345678901234");
            ValidateFailure(result);
        }

        [Fact]
        public void Validate_WhenDocumentNumberIsCNPJAndValid_IsValid()
        {
            var result = validator.Validate(ValidCnpj);
            result.IsValid.Should().BeTrue();
        }
    }
}
