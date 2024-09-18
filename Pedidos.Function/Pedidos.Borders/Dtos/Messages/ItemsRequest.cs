namespace Pedidos.Borders.Dtos.Messages;
public record ItemsRequest(
    string Produto,
    int Quantidade,
    double Preco
);