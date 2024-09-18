using FluentAssertions;
using FluentValidation;
using Moq.AutoMock;
using Orders.Borders.Shared;

namespace Orders.Tests.Validators
{
    public abstract class BaseValidatorTests<TValidator, TRequest> where TValidator : AbstractValidator<TRequest>
    {
        protected readonly AutoMocker AutoMocker = new();
        protected TValidator GetSut() => AutoMocker.CreateInstance<TValidator>();

        protected void ActAndAssertSuccess(TRequest request)
        {
            //Act
            var result = GetSut().Validate(request);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        protected void ActAndAssertFail(TRequest request, ErrorMessage expectedError)
        {
            //Act
            var result = GetSut().Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(t => t.ErrorCode == expectedError.Code && t.ErrorMessage == expectedError.Message);
        }
    }
}
