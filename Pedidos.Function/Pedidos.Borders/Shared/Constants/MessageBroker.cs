namespace Pedidos.Borders.Shared.Constants
{
    public static class MessageBroker
    {
        public const string ConnectionStringVariableName = "MessageBrokerConnectionString";

        public static class QueueNames
        {
            public const string Orders = "queue-orders";
        }
    }
}
