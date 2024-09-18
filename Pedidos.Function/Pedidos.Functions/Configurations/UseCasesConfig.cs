using Microsoft.Extensions.DependencyInjection;
using Pedidos.Borders.UseCases;
using Pedidos.UseCases;

namespace Pedidos.Functions.Configurations
{
    public static class UseCasesConfig
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IOnboardingStepUseCase, ProcessOrderUseCase>();
            
            return services;
        }
    }
}
