using MongoDB.Driver;
using Orders.Borders.Shared;

namespace Orders.Repositories;
public class DatabaseFactory : IMongoClientFactory
{
    private readonly MongoUrl mongoUrl;
    private readonly MongoClient mongoClient;

    public DatabaseFactory(ApplicationConfig applicationConfig)
    {
        mongoUrl = new MongoUrl(applicationConfig.ConnectionStrings.DefaultConnection);
        mongoClient = new MongoClient(mongoUrl);
    }

    public string DatabaseName => mongoUrl.DatabaseName;

    public IMongoClient GetClient() => mongoClient;
}