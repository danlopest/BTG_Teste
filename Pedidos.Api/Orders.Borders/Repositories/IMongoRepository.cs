namespace Orders.Borders.Repositories;
public interface IMongoRepository<in TEntity> where TEntity : class
{
    Task Save(TEntity entity);
}