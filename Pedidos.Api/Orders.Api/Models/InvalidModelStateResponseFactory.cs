using Microsoft.AspNetCore.Mvc;
using Orders.Borders.Shared;

namespace Orders.Api.Models
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult CreateResponse(ActionContext context)
        {
            var errors = context.ModelState.SelectMany(modelState =>
            {
                return modelState.Value!.Errors.Select(err
                    => new ErrorMessage(ErrorCodes.ValidationError, $"{modelState.Key} - {err.ErrorMessage}"));
            });

            return new BadRequestObjectResult(errors);
        }
    }
}
