using Microsoft.Extensions.Configuration;
using Pedidos.Borders.Shared;

namespace Pedidos.Functions.Configurations
{
    public static class ConfigurationExtensions
    {
        public static ApplicationConfig LoadConfiguration(this IConfiguration source) => source.Get<ApplicationConfig>()!;
    }   
}
