using Bogus;
using Orders.Borders.Entities.Configurations;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Entities.Configurations
{
    public class AppointmentConfigurationBuilder
    {
        private readonly Faker<AppointmentConfiguration> instance;

        public AppointmentConfigurationBuilder(Guid? personId = null, Role? role = null)
        {
            instance = new AutoFaker<AppointmentConfiguration>();

            if (personId != null)
                instance.RuleFor(x => x.PersonId, personId);

            if (role != null)
                instance.RuleFor(x => x.Role, role);
        }

        public AppointmentConfiguration Build() => instance.Generate();
    }
}
