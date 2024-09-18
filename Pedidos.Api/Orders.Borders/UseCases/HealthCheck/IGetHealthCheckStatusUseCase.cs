using Orders.Borders.Dtos.HealthCheck;
using Orders.Borders.Shared;

namespace Orders.Borders.UseCases.HealthCheck
{
    public interface IGetHealthCheckStatusUseCase : IUseCase<bool, HealthCheckStatus>
    {
    }
}