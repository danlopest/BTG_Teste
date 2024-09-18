using Orders.Borders.UseCases.Configurations;
using Orders.UseCases.Configurations;
using Orders.UseCases.Orders;

namespace Orders.Api.Configurations;

public static class UseCasesConfig
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services
            .AddScoped<IListOrderUseCase, ListOrderByClientIdUseCase>()
            .AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
    }
}