using Orders.Borders.Repositories;
using Orders.Borders.Shared;
using Orders.Repositories;

namespace Orders.Api.Configurations;
public static class RepositoriesConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, ApplicationConfig applicationConfig)
    {
        return services
             .AddSingleton<IDatabaseRepository, DatabaseRepository>()
             .AddSingleton<IDbContext, DbContext>()
             .AddSingleton<IMongoClientFactory, DatabaseFactory>()
             .AddScoped<IOrderRepository, OrderRepository>();
    }
}