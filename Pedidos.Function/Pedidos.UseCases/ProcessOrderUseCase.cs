using Microsoft.Extensions.Logging;
using Pedidos.Borders.Dtos.Messages;
using Pedidos.Borders.Repositories;
using Pedidos.Borders.UseCases;

namespace Pedidos.UseCases;
public class ProcessOrderUseCase(ILogger<ProcessOrderUseCase> logger, IManagementRepository managementRepository) : IOnboardingStepUseCase
{
    public async Task Execute(OrderRequest request)
    {
        try
        {
            await managementRepository.ProcessOrders(request);
            logger.LogInformation($"[{{UseCaseName}}] Order {{OrderId}} to Client {{ClientId}} processed successfully.", GetType().Name, request.CodigoPedido, request.CodigoCliente);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"[{{UseCaseName}}] Error when processing onboarding {{OnboardingId}} client {{ClientId}} - {{@Exception}}", GetType().Name, request.CodigoPedido, request.CodigoCliente, ex);
            throw;
        }
    }
}