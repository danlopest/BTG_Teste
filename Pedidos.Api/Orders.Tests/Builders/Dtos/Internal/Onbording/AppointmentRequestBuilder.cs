using Bogus;
using Bogus.Extensions.Brazil;
using Orders.Borders.Dtos.Internal.Onboarding;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Onbording
{
    public class AppointmentRequestBuilder
    {
        private readonly Faker<AppointmentRequest> instance;

        public AppointmentRequestBuilder(Role? role = null)
        {
            instance = instance = new AutoFaker<AppointmentRequest>()
                .RuleFor(t => t.DocumentNumber, faker => faker.Person.Cpf(false))
                .RuleFor(t => t.Role, faker => faker.PickRandom<Role>());

            if (role != null)
                instance.RuleFor(t => t.Role, role);
        }

        public AppointmentRequestBuilder WithPersonId(Guid personId)
        {
            instance
               .RuleFor(t => t.DocumentNumber, faker => null)     
               .RuleFor(t => t.PersonId, personId);

            return this;
        }

        public AppointmentRequestBuilder WithDocumentNumber(string? documentNumber)
        {
            instance
               .RuleFor(t => t.DocumentNumber, documentNumber)
               .RuleFor(t => t.PersonId, faker => null);

            return this;
        }

        public AppointmentRequest Build() => instance.Generate();
    }
}
