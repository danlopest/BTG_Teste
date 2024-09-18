using Orders.Borders.Entities.Orders;

namespace Orders.Borders.Repositories;
public interface IOrderRepository
{
    Task Insert(Order order);
    Task<IEnumerable<Order>> ListByClientId(Guid clientId);
}