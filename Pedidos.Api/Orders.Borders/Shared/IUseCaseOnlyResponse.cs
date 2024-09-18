namespace Orders.Borders.Shared
{
    public interface IUseCaseOnlyResponse<TResponse>
    {
        Task<UseCaseResponse<TResponse>> Execute();
    }
}
