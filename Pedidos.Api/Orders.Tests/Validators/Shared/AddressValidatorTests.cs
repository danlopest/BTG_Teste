using FluentAssertions;
using Orders.Borders.Dtos.Shared.Interfaces;
using Orders.Borders.Enums;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Shared;
using Orders.Tests.Builders.Dtos.Internal.Onbording;
using Xunit;

namespace Orders.Tests.Validators.Shared
{
    public class AddressValidatorTests : BaseValidatorTests<AddressValidator, IAddress>
    {
        [Fact]
        public void Validate_WhenTypeIsInvalid_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressInvalidType;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                Type = (AddressType)2000
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenStreetIsEmpty_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressStreetIsRequired;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                Street = ""
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenStreetExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressStreetMaxLengthExceeded;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                Street = new string('x', 201)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenNumberIsEmpty_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressNumberIsRequired;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                Number = ""
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenNumberExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressNumberMaxLengthExceeded;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                Number = new string('x', 51)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenDistrictExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressDistrictMaxLengthExceeded;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                District = new string('x', 101)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenCityExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressCityMaxLengthExceeded;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                City = new string('x', 201)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenStateLengthIsNotTwo_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressStateLengthIncorrect;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                State = "Sao Paulo"
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenZipCodeExceedsMaxLength_IsInvalid()
        {
            var expectedError = ErrorMessages.AddressZipCodeMaxLengthExceeded;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                ZipCode = new string('x', 11)
            };

            ActAndAssertFail(request, expectedError);
        }

        [Theory]
        [InlineData("")]
        [InlineData("BRA")]
        [InlineData("BRASIL")]
        public void Validate_WhenIsBrazilianAddress_RequiresProperties(string country)
        {
            var expectedError = ErrorMessages.AddressZipCodeIsRequired;
            var request = new PersonAddressRequestBuilder().Build() with
            {
                Country = country,
                ZipCode = ""
            };

            ActAndAssertFail(request, expectedError);
        }
    }
}
