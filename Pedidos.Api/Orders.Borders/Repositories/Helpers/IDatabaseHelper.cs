using System.Data;

namespace Orders.Borders.Repositories.Helpers
{
    public interface IDatabaseHelper
    {
        IDbConnection GetConnection();
        Task<int> ExecuteAsyncAndCommit(string sql, object param);
        Task<IEnumerable<T>> ExecuteReaderAsyncAndDeserialize<T>(string sql, object param);
    }
}
