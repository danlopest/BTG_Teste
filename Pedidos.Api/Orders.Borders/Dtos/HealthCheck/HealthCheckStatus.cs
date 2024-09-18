using System.Reflection;

namespace Orders.Borders.Dtos.HealthCheck
{
    public class HealthCheckStatus
    {
        public HealthCheckStatus(string buildId, IEnumerable<HealthCheckActivity> activities)
        {
            Activities = activities;
            Version = GetType()
                .GetTypeInfo()
                .Assembly
                .GetCustomAttribute<AssemblyFileVersionAttribute>()!
                .Version;
            BuildId = buildId;
        }

        public string Version { get; }

        public IEnumerable<HealthCheckActivity> Activities { get; }

        public bool Success => Activities.All(a => a.Success);

        public string BuildId { get; }
    }
}