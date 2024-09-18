using FluentAssertions;
using Orders.Borders.Shared;
using Orders.Borders.Validators;
using Xunit;

namespace Orders.Tests.Validators
{
    public class DateTimeValidatorTests
    {
        private readonly ErrorMessage errorMessage = new("00.00", "Error message");
        private readonly DateTimeValidator validator;

        public DateTimeValidatorTests() => validator = new DateTimeValidator(errorMessage);

        [Fact]
        public void Validate_WhenDateTimeIsValid_IsValid()
        {
            var result = validator.Validate(DateTime.UtcNow);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(InvalidDates))]
        public void Validate_WhenDateTimeIsInvalid_IsInvalid(DateTime dateTime)
        {
            var result = validator.Validate(dateTime);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorCode == errorMessage.Code && x.ErrorMessage == errorMessage.Message);
        }

        public static TheoryData<DateTime> InvalidDates() =>
        new()
        {
            { DateTime.MinValue },
            { DateTime.MaxValue }
        };
    }
}
