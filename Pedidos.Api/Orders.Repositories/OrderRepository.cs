using MongoDB.Driver;
using Orders.Borders.Entities.Orders;
using Orders.Borders.Repositories;

namespace Orders.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly IDbContext db;
    public OrderRepository(IDbContext db)
    {
        this.db = db;
    }

    public Task Insert(Order order) => db.Order.InsertOneAsync(order);

    public async Task<IEnumerable<Order>> ListByClientId(Guid clientId)
    {
        var filter = Builders<Order>.Filter.Where(o => o.CodigoCliente == clientId);

        var findResult = await db.Order.FindAsync(filter);

        return await findResult.ToListAsync();
    }
}