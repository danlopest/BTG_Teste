using MongoDB.Driver;

namespace Orders.Repositories;

public class DbContext : IDbContext
{
    private readonly IMongoClient mongoClient;
    private readonly string databaseName;

    public DbContext(IMongoClientFactory mongoClientFactory)
    {
        mongoClient = mongoClientFactory.GetClient();
        databaseName = mongoClientFactory.DatabaseName;
    }

    public IMongoDatabase Database => mongoClient.GetDatabase(databaseName);

    public IMongoCollection<Borders.Entities.Orders.Order> Order
        => Database.GetCollection<Borders.Entities.Orders.Order>("order");
}