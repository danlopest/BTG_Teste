namespace Pedidos.Borders.UseCases
{
    public interface IUseCase<in TRequest>
    {
        Task Execute(TRequest request);
    }
}
