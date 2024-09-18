using Orders.Borders.Dtos.External.Appointments;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.External.Appointments
{
    public class AppointmentResponseBuilder
    {
        private AppointmentResponse instance;

        public AppointmentResponseBuilder()
        {
            instance = new AppointmentResponse()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                ProductId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-1),
                Participants = new List<AppointmentParticipant>
                {
                    new(Guid.NewGuid(), Role.Broker),
                    new(Guid.NewGuid(), Role.PolicyHolder)
                }
            };
        }

        public AppointmentResponseBuilder WithProductId(Guid productId)
        {
            instance = instance with { ProductId = productId };
            return this;
        }

        public AppointmentResponseBuilder WithParticipants(IEnumerable<AppointmentParticipant> participants)
        {
            instance = instance with { Participants = participants };
            return this;
        }

        public AppointmentResponseBuilder WithEndDate(DateTime? endDate)
        {
            instance = instance with { EndDate = endDate };
            return this;
        }

        public AppointmentResponse Build() => instance;
    }
}
