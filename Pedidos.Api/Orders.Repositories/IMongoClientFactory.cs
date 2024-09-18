using MongoDB.Driver;

namespace Orders.Repositories;
public interface IMongoClientFactory
{
    IMongoClient GetClient();
    string DatabaseName { get; }
}