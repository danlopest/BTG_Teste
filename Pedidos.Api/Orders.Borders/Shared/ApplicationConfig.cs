#nullable disable

namespace Orders.Borders.Shared
{
    public record ApplicationConfig
    {
        public Repository Repository { get; set; }
        public ApplicationConfig()
        {
            ConnectionStrings = new ConnectionStrings();
        }
        public ConnectionStrings ConnectionStrings { get; init; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Repository
    {
        public MongoDB MongoDB { get; set; }
    }

    public class MongoDB
    {
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
        public bool IsSSL { get; set; }
        public bool Active { get; set; }
    }

    public record ApiConfig
    {
        public virtual string BaseUrl { get; init; }
    }
}