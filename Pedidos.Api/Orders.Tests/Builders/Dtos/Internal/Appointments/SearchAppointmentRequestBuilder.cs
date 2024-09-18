using Orders.Borders.Dtos.Internal.Appointments;
using Orders.Borders.Enums;

namespace Orders.Tests.Builders.Dtos.Internal.Appointments
{
    public class SearchAppointmentRequestBuilder
    {
        private SearchAppointmentRequest instance;

        public SearchAppointmentRequestBuilder()
        {
            instance = new SearchAppointmentRequest()
            {
                MainParticipantId = Guid.NewGuid(),
                MainParticipantRole = Role.Broker,
                OtherParticipantRole = Role.PolicyHolder,
                ProductId = Guid.NewGuid(),
                ReferenceDate = DateTime.UtcNow
            };
        }

        public SearchAppointmentRequestBuilder WithProductId(Guid productId)
        {
            instance = instance with { ProductId = productId };
            return this;
        }

        public SearchAppointmentRequestBuilder WithMainParticipantId(Guid mainParticipantId)
        {
            instance = instance with { MainParticipantId = mainParticipantId };
            return this;
        }

        public SearchAppointmentRequestBuilder WithMainParticipantRole(Role mainParticipantRole)
        {
            instance = instance with { MainParticipantRole = mainParticipantRole };
            return this;
        }

        public SearchAppointmentRequestBuilder WithParticipantsRole(Role participantsRole)
        {
            instance = instance with { OtherParticipantRole = participantsRole };
            return this;
        }

        public SearchAppointmentRequest Build() => instance;
    }
}
