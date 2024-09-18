using Microsoft.AspNetCore.Mvc;
using Orders.Api.Models;
using Orders.Borders.Dtos.Orders;
using Orders.Borders.Shared;
using Orders.Borders.UseCases.Configurations;

namespace Orders.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController(
    IActionResultConverter actionResultConverter,
    ICreateOrderUseCase createConfigurationUseCase,
    IListOrderUseCase getConfigurationsByProductIdUseCase) : ControllerBase
{
    /// <summary>
    /// Cria um novo pedido
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMessage[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage[]))]
    public async Task<IActionResult> Create([FromBody] OrderRequest request)
    {
        var response = await createConfigurationUseCase.Execute(request);
        return actionResultConverter.Convert(response);
    }

    /// <summary>
    /// Lista todos os pedidos por cliente
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMessage[]))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage[]))]
    public async Task<IActionResult> List([FromQuery] Guid clientId)
    {
        var response = await getConfigurationsByProductIdUseCase.Execute(clientId);
        return actionResultConverter.Convert(response);
    }
}
