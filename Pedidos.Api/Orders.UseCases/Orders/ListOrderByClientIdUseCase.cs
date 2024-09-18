using Orders.Borders.Dtos.Orders;
using Orders.Borders.Repositories;
using Orders.Borders.Shared;
using Orders.Borders.UseCases.Configurations;

namespace Orders.UseCases.Orders;

public class ListOrderByClientIdUseCase(IOrderRepository repository) : IListOrderUseCase
{
    public async Task<UseCaseResponse<IEnumerable<OrderResponse>>> Execute(Guid clientId)
    {
        var results = await repository.ListByClientId(clientId);

        var response  = new List<OrderResponse>();

        foreach (var order in results) 
        {
            response.Add(
             new OrderResponse(order)
             );
        };

        return UseCaseResponse<IEnumerable<OrderResponse>>.Success(response);
    }
}