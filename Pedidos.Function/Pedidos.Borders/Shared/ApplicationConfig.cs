#nullable disable

namespace Pedidos.Borders.Shared
{
    public record ApplicationConfig
    {
        public AuthenticationConfig Authentication { get; init; }
        public ApiConfig Orders { get; init; }
    }

    public record AuthenticationConfig
    {
        public Uri Authority { get; init; }
        public string Audience { get; init; }
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public string[] Scopes { get; init; }
    }

    public class ApiConfig
    {
        public string BaseUrl { get; init; }
        public string Scope { get; init; }
    }
}
