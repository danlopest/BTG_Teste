using Pedidos.Borders.Shared.Builders;

namespace Pedidos.Borders.Shared.Constants
{
    public static class ErrorMessages
    {
        public static readonly ErrorMessage ErrorCommunicatingWithIdentity = new("41.01", "Error communicating with Identity");
        public static readonly ErrorMessage ErrorCommunicatingWithService = new("41.02", "Error communicating with service");
        public static readonly ErrorMessageBuilder RepositoryUnexpectedError = new("41.03", "Unexpected error at {0} on {1}");
    }
}
