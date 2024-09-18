using Orders.Borders.Dtos.Orders;
using Orders.Borders.Repositories;
using Orders.Borders.Shared;
using Orders.Borders.UseCases.Configurations;

namespace Orders.UseCases.Configurations;
public class CreateOrderUseCase(IOrderRepository orderRepository) : ICreateOrderUseCase
{
    public async Task<UseCaseResponse<OrderResponse>> Execute(OrderRequest request)
    {
        var order = new Borders.Entities.Orders.Order(request);

        await orderRepository.Insert(order);

        return UseCaseResponse<OrderResponse>.Persisted(new OrderResponse(order), order._id.ToString());
    }
}