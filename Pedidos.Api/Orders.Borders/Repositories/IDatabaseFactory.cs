using MongoDB.Driver;

namespace Orders.Borders.Repositories;
public interface IDatabaseFactory
{
    IMongoDatabase MongoDb { get; }
}