using Pedidos.Borders.Dtos.Messages;
using Pedidos.Borders.Repositories;
using Pedidos.Borders.Shared.Constants;
using System.Text;
using System.Text.Json;

namespace Pedidos.Repositories;
public class OrdersRepository(HttpClient httpClient) : IManagementRepository
{
    public async Task ProcessOrders(OrderRequest request)
    {
        var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync($"/api/v1/orders", content);

        if (!response.IsSuccessStatusCode)
            throw new Exception(DefaultErrorMessage(nameof(ProcessOrders)));
    }

    private string DefaultErrorMessage(string callerMethod)
        => ErrorMessages.RepositoryUnexpectedError.Build(callerMethod, GetType().Name).Message;
}
