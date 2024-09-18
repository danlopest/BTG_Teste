using Orders.Borders.Dtos.Orders;
using Orders.Borders.Shared;

namespace Orders.Borders.UseCases.Configurations;
public interface ICreateOrderUseCase : IUseCase<OrderRequest, OrderResponse>
{
}