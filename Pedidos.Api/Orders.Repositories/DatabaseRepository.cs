using Orders.Borders.Repositories;

namespace Orders.Repositories;
public class DatabaseRepository : IDatabaseRepository
{
    private readonly IMongoClientFactory factory;
    public DatabaseRepository(IMongoClientFactory factory)
    {
        this.factory = factory;
    }
}