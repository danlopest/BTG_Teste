namespace Orders.Borders.Dtos.Orders;
public record OrderResponse : Order
{
    public OrderResponse(Entities.Orders.Order order)
    {
        CodigoCliente = order.CodigoCliente;
        CodigoPedido = order.CodigoPedido;
        Itens = order.Itens.Select(item => new Item(
            item.Produto,
            item.Quantidade,
            item.Preco
        ));
    }
}