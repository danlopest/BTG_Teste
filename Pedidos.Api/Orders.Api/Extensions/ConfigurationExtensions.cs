using Orders.Borders.Shared;

namespace Orders.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static ApplicationConfig LoadConfiguration(this IConfiguration source) => source.Get<ApplicationConfig>()!;
    }
}
