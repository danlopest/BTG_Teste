using FluentAssertions;
using Orders.Borders.Dtos.Internal.Brokers;
using Orders.Borders.Dtos.Internal.Configurations;
using Orders.Borders.Enums;
using Orders.Borders.Shared;
using Orders.Borders.Validators.Brokers;
using Orders.Borders.Validators.Configurations;
using Orders.Tests.Builders.Dtos.Internal.Configurations;
using Xunit;

namespace Orders.Tests.Validators.Configurations
{
    public class CreateConfigurationRequestValidatorTests : BaseValidatorTests<CreateConfigurationRequestValidator, OrderRequest>
    {
        [Theory]
        [MemberData(nameof(ValidTestData))]
        public void Validate_WhenValid_ReturnsOk(OrderRequest request)
        {
            ActAndAssertSuccess(request);
        }

        [Fact]
        public void Validate_WhenHasEmptyProductId_IsInvalid()
        {
            var expectedError = ErrorMessages.InvalidProductId;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                CodigoPedido = Guid.Empty
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenHasInvalidRole_IsInvalid()
        {
            var expectedError = ErrorMessages.InvalidRole;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                Role = (Role)2000
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenHasInvalidCreatedBy_IsInvalid()
        {
            var expectedError = ErrorMessages.CreatedByMustNotBeEmpty;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                CreatedBy = ""
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenHasDuplicateOnboardingSteps_IsInvalid()
        {
            var expectedError = ErrorMessages.OnboardingStepsMustNotContainDuplicates;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                OnboardingSteps = [OnboardingStep.Legacy, OnboardingStep.Legacy]
            };

            ActAndAssertFail(request, expectedError);
        }


        [Fact]
        public void Validate_WhenHasDuplicateCorporateRequirements_IsInvalid()
        {
            var expectedError = ErrorMessages.CorporateRequirementsMustNotContainDuplicates;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                OnboardingCorporateRequirements = [OnboardingRequirement.Address, OnboardingRequirement.Address]
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenHasDuplicateIndividualRequirements_IsInvalid()
        {
            var expectedError = ErrorMessages.IndividualRequirementsMustNotContainDuplicates;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                OnboardingIndividualRequirements = [OnboardingRequirement.Address, OnboardingRequirement.Address]
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenHasDefaultAppointmentsButNoAppointmentStep_IsInvalid()
        {
            var expectedError = ErrorMessages.DefaultAppointmentsNotAllowedWithoutStepInRequest;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                DefaultAppointments = [
                    new(){ PersonId = Guid.NewGuid(), Role = Role.PolicyHolder }
                ],
                OnboardingSteps = [OnboardingStep.Legacy]
            };

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenHasAppointmentStepButNoDefaultAppointments_IsInvalid()
        {
            var expectedError = ErrorMessages.DefaultAppointmentsRequired;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                DefaultAppointments = [],
                OnboardingSteps = [OnboardingStep.Appointments]
            };

            ActAndAssertFail(request, expectedError);
        }

        [Theory]
        [MemberData(nameof(DefaultAppointmentsWithoutStepTestData))]
        public void Validate_WhenDoesNotHaveAppointmentStepsAndHasDefaultAppointments_IsInvalid(OrderRequest request)
        {
            var expectedError = ErrorMessages.DefaultAppointmentsNotAllowedWithoutStepInRequest;

            ActAndAssertFail(request, expectedError);
        }

        [Fact]
        public void Validate_WhenHasAppointmentStepButDuplicateDefaultAppointments_IsInvalid()
        {
            var id = Guid.NewGuid();
            var expectedError = ErrorMessages.DefaultAppointmentsMustNotContainDuplicates;
            var request = new CreateConfigurationRequestBuilder().Build();
            request = request with
            {
                DefaultAppointments = [
                    new(){ PersonId = id, Role = Role.PolicyHolder },
                    new(){ PersonId = id, Role = Role.PolicyHolder }
                ],
                OnboardingSteps = [OnboardingStep.Appointments]
            };

            ActAndAssertFail(request, expectedError);
        }

        public static TheoryData<OrderRequest> DefaultAppointmentsWithoutStepTestData() =>
        new()
        {
            {
                new CreateConfigurationRequestBuilder().Build() with
                {
                    OnboardingSteps = null,
                    DefaultAppointments = [ new AppointmentConfigurationRequest (){
                        PersonId = Guid.NewGuid(), Role = Role.CommercialAgent
                        }
                    ],
                }
            },
            {
                new CreateConfigurationRequestBuilder().Build() with
                {
                    OnboardingSteps = [OnboardingStep.Legacy],
                    DefaultAppointments = [ new AppointmentConfigurationRequest (){
                        PersonId = Guid.NewGuid(), Role = Role.CommercialAgent
                        }
                    ],
                }
            }
        };

        public static TheoryData<OrderRequest> ValidTestData() =>
        new()
        {
            { new CreateConfigurationRequestBuilder().Build() },
            { new CreateConfigurationRequestBuilder().Build() with
                {
                    OnboardingSteps = null,
                    DefaultAppointments = null
                }
            }
        };
    }
}
