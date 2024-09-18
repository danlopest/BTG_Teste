using FluentAssertions;
using Orders.Borders.Shared;
using Orders.Borders.Validators;
using Xunit;

namespace Orders.Tests.Validators
{
    public class GuidValidatorTests
    {
        private readonly ErrorMessage errorMessage = new("00.00", "Error message");
        private readonly GuidValidator validator;

        public GuidValidatorTests()
        {
            validator = new(errorMessage);
        }

        [Fact]
        public void Validate_WhenGuidIsValid_IsValid()
        {
            var result = validator.Validate(Guid.NewGuid());
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenGuidIsEmpty_IsInvalid()
        {
            var result = validator.Validate(Guid.Empty);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorCode == errorMessage.Code && x.ErrorMessage == errorMessage.Message);
        }
    }
}
