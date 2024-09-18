namespace Pedidos.Borders.Dtos.Messages;

public record OrderRequest(
    Guid CodigoPedido, 
    Guid CodigoCliente,
    IEnumerable<ItemsRequest> Itens
);