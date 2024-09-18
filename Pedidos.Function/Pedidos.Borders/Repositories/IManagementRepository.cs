using Pedidos.Borders.Dtos.Messages;

namespace Pedidos.Borders.Repositories
{
    public interface IManagementRepository
    {
        Task ProcessOrders(OrderRequest request);
    }
}
