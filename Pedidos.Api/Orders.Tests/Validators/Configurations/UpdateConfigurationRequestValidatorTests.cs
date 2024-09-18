using FluentAssertions;
using Orders.Borders.Dtos.Internal.Brokers;
using Orders.Borders.Dtos.Internal.Configurations;
using Orders.Borders.Entities.Configurations;
using Orders.Borders.Enums;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Brokers;
using Orders.Borders.Validators.Configurations;
using Orders.Tests.Builders.Dtos.Internal.Configurations;
using Orders.Tests.Builders.Entities.Configurations;
using Xunit;

namespace Orders.Tests.Validators.Configurations
{
    public class UpdateConfigurationRequestValidatorTests : BaseValidatorTests<UpdateConfigurationRequestValidator, (UpdateConfigurationRequest, Configuration)>
    {
        [Fact]
        public void Validate_WhenValid_ReturnsOk()
        {
            var request = new UpdateConfigurationRequestBuilder().Build();
            var existingConfig = new ConfigurationBuilder().Build();

            ActAndAssertSuccess((request, existingConfig));
        }

        [Fact]
        public void Validate_WhenHasInvalidUpdatedBy_IsInvalid()
        {
            var expectedError = ErrorMessages.UpdatedByMustNotBeEmpty;
            var request = new UpdateConfigurationRequestBuilder().Build();
            request = request with
            {
                UpdatedBy = ""
            };
            var existingConfig = new ConfigurationBuilder().Build();

            ActAndAssertFail((request, existingConfig), expectedError);
        }

        [Theory]
        [MemberData(nameof(AppointmentsData))]
        public void Validate_WhenHasInvalidOnboardingStepsAndAppointments_IsInvalid(UpdateConfigurationRequest request, Configuration config, ErrorMessage expectedError)
        {
            ActAndAssertFail((request, config), expectedError);
        }

        public static TheoryData<UpdateConfigurationRequest, Configuration, ErrorMessage> AppointmentsData() =>
            new()
            {
                {
                    new UpdateConfigurationRequestBuilder().WithOnboardingSteps([OnboardingStep.Appointments]).Build(),
                    new ConfigurationBuilder().WithOnboardingSteps([OnboardingStep.Legacy]).Build(),
                    ErrorMessages.DefaultAppointmentsRequired
                },
                {
                    new UpdateConfigurationRequestBuilder().WithOnboardingSteps([OnboardingStep.Legacy, OnboardingStep.Appointments]).Build(),
                    new ConfigurationBuilder().WithOnboardingSteps([OnboardingStep.Legacy]).Build(),
                    ErrorMessages.DefaultAppointmentsRequired
                },
                {
                    new UpdateConfigurationRequestBuilder().WithOnboardingSteps([OnboardingStep.Appointments]).WithDefaultAppointments([]).Build(),
                    new ConfigurationBuilder().WithOnboardingSteps([OnboardingStep.Appointments]).Build(),
                    ErrorMessages.DefaultAppointmentsCannotBeClearedWhileHavingAppointmentsStepInRequest
                },
                {
                    new UpdateConfigurationRequestBuilder().WithOnboardingSteps(null).WithDefaultAppointments([]).Build(),
                    new ConfigurationBuilder().WithOnboardingSteps([OnboardingStep.Appointments]).Build(),
                    ErrorMessages.DefaultAppointmentsCannotBeClearedWhenConfigurationHasStep
                },
                {
                    new UpdateConfigurationRequestBuilder().WithOnboardingSteps([OnboardingStep.Legacy]).WithDefaultAppointments([Role.Beneficiary]).Build(),
                    new ConfigurationBuilder().WithOnboardingSteps([OnboardingStep.Appointments]).WithDefaultAppointments([Role.Insured]).Build(),
                    ErrorMessages.DefaultAppointmentsNotAllowedWithoutStepInRequest
                },
                {
                    new UpdateConfigurationRequestBuilder().WithOnboardingSteps(null).WithDefaultAppointments([Role.Beneficiary]).Build(),
                    new ConfigurationBuilder().WithOnboardingSteps([OnboardingStep.Legacy]).Build(),
                    ErrorMessages.DefaultAppointmentsNotAllowedWithoutStepInRequestOrEntity
                }
            };

        [Fact]
        public void Validate_WhenHasNoUpdateProperty_IsInvalid()
        {
            var expectedError = ErrorMessages.AtLeastOneUpdatePropertyMustBeSent;
            var request = new UpdateConfigurationRequestBuilder().Build();
            request = request with
            {
                OnboardingCorporateRequirements = null,
                OnboardingIndividualRequirements = null,
                OnboardingSteps = null,
                DefaultAppointments = null
            };
            var existingConfig = new ConfigurationBuilder().Build();

            ActAndAssertFail((request, existingConfig), expectedError);
        }

        [Fact]
        public void Validate_WhenHasDuplicateOnboardingSteps_IsInvalid()
        {
            var expectedError = ErrorMessages.OnboardingStepsMustNotContainDuplicates;
            var request = new UpdateConfigurationRequestBuilder().Build();
            request = request with
            {
                OnboardingSteps = [OnboardingStep.Legacy, OnboardingStep.Legacy]
            };
            var existingConfig = new ConfigurationBuilder().Build();

            ActAndAssertFail((request, existingConfig), expectedError);
        }

        [Fact]
        public void Validate_WhenHasDuplicateCorporateRequirements_IsInvalid()
        {
            var expectedError = ErrorMessages.CorporateRequirementsMustNotContainDuplicates;
            var request = new UpdateConfigurationRequestBuilder().Build();
            request = request with
            {
                OnboardingCorporateRequirements = [OnboardingRequirement.Address, OnboardingRequirement.Address]
            };
            var existingConfig = new ConfigurationBuilder().Build();

            ActAndAssertFail((request, existingConfig), expectedError);
        }

        [Fact]
        public void Validate_WhenHasDuplicateIndividualRequirements_IsInvalid()
        {
            var expectedError = ErrorMessages.IndividualRequirementsMustNotContainDuplicates;
            var request = new UpdateConfigurationRequestBuilder().Build();
            request = request with
            {
                OnboardingIndividualRequirements = [OnboardingRequirement.Address, OnboardingRequirement.Address]
            };
            var existingConfig = new ConfigurationBuilder().Build();

            ActAndAssertFail((request, existingConfig), expectedError);
        }
    }
}
