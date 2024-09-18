using MongoDB.Driver;
using Orders.Borders.Entities.Orders;

namespace Orders.Repositories;
public interface IDbContext
{
    IMongoDatabase Database { get; }
    IMongoCollection<Order> Order { get; }
}