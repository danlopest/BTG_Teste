namespace Orders.Borders.Dtos.Orders;
public record Order
{
    /// <summary>
    /// Código identificador do Pedido
    /// </summary>
    public Guid CodigoPedido { get; init; }

    /// <summary>
    /// Código identificador do Cliente
    /// </summary>
    public Guid CodigoCliente { get; init; }

    /// <summary>
    /// Itens do pedido
    /// </summary>
    public IEnumerable<Item> Itens { get; init; }
}