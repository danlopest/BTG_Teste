using Microsoft.Extensions.DependencyInjection;
using Pedidos.Borders.Repositories;
using Pedidos.Borders.Shared;
using Pedidos.Repositories;
using System;

namespace Pedidos.Functions.Configurations
{
    public static class RepositoriesConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, ApplicationConfig appConfig)
        {
            services.AddHttpClient<IManagementRepository, OrdersRepository>(client =>
            {
                client.BaseAddress = new Uri(appConfig.Orders.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
