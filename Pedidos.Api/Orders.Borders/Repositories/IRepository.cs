namespace Orders.Borders.Repositories
{
    public interface IRepository
    {
        string Name { get; }
        int? Version { get; }
        bool External { get; }

        Task<bool> CheckHealth();
    }
}
