using MongoDB.Bson;

namespace Orders.Borders.Entities.Orders;
public class Order
{
    public Order(Dtos.Orders.OrderRequest request)
    {
        CodigoPedido = request.CodigoPedido;
        CodigoCliente = request.CodigoCliente;
        Itens = request.Itens.Select(i => new Item(i.Produto, i.Quantidade, i.Preco));
    }
    public ObjectId _id { get; set; }
    public Guid CodigoPedido { get; init; }
    public Guid CodigoCliente { get; init; }
    public IEnumerable<Item> Itens { get; init; }
}