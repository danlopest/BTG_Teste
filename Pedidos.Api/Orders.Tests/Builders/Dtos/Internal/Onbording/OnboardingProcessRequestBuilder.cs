using Bogus;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class OnboardingProcessRequestBuilder
    {
        private readonly Faker<OnboardingProcessRequest> instance;

        public OnboardingProcessRequestBuilder(Role role = Role.PolicyOwner, PersonType personType = PersonType.Individual, Guid? productId = null)
        {
            productId ??= Guid.NewGuid();

            instance = instance = new AutoFaker<OnboardingProcessRequest>()
                .RuleFor(t => t.Role, role)
                .RuleFor(t => t.ProductId, productId)
                .RuleFor(t => t.Person, new PersonOnboardingRequestBuilder(personType).Build());
        }

        public OnboardingProcessRequest Build() => instance.Generate();

        public OnboardingProcessRequestBuilder WithAppointments(int? totalAppointments = 1, Guid? personId = null, string? documentNumber = null)
        {
            if (totalAppointments == null)
                instance.RuleFor(t => t.Appointments, (object?)null);
            else
                instance.RuleFor(t => t.Appointments, faker => faker.Make(totalAppointments.Value, () => 
                personId == null ? new AppointmentRequestBuilder().WithDocumentNumber(documentNumber).Build() : new AppointmentRequestBuilder().WithPersonId((Guid)personId).Build()));

            return this;
        }

        public OnboardingProcessRequestBuilder WithAppointments(IEnumerable<AppointmentRequest> appointments)
        {
            instance.RuleFor(t => t.Appointments, appointments);
            return this;
        }
    }
}
