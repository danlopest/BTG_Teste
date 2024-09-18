namespace Orders.Borders.Shared
{
    public record PaginationResponse<T>(IEnumerable<T> Data, int TotalCount);
}
