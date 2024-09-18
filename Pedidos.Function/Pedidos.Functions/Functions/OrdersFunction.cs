using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Pedidos.Borders.Dtos.Messages;
using Pedidos.Borders.Shared.Constants;
using Pedidos.Borders.UseCases;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pedidos.Functions.Functions;
public class OrdersFunction(ILogger<OrdersFunction> logger, IOnboardingStepUseCase useCase)
{
    [Function(FunctionNames.ProcessOrders)]
    public virtual async Task Run(
        [RabbitMQTrigger(MessageBroker.QueueNames.Orders, ConnectionStringSetting = MessageBroker.ConnectionStringVariableName)]
        string message,
        FunctionContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        var orderRequest = JsonSerializer.Deserialize<OrderRequest>(message, options);

        if (orderRequest == null)
        {
            logger.LogError("Failed to deserialize message.");
            return;
        }

        await useCase.Execute(orderRequest);
    }
}