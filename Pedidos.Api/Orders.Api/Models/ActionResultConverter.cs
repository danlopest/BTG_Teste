using Microsoft.AspNetCore.Mvc;
using Orders.Borders.Shared;
using Orders.Borders.Shared.Extensions;
using System.Net;

namespace Orders.Api.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false);
        IActionResult Convert<T>(UseCaseResponse<PaginationResponse<T>> response);
    }

    public class ActionResultConverter(IHttpContextAccessor accessor) : IActionResultConverter
    {
        private readonly string path = accessor?.HttpContext?.Request.Path.Value ?? string.Empty;

        public IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false)
        {
            if (response == null)
                return BuildError(new[] { new ErrorMessage(ErrorCodes.InternalServerError, "ActionResultConverter Error") }, UseCaseResponseKind.InternalServerError);

            if (response.Success())
            {
                if (noContentIfSuccess)
                    return new NoContentResult();

                return BuildSuccessResult(response.Result!, response.Status, response.ResultId);
            }

            return HandleErrors(response);
        }

        public IActionResult Convert<T>(UseCaseResponse<PaginationResponse<T>> response)
        {
            if (response.Success())
            {
                accessor.HttpContext!.Response.Headers.Append("X-Total-Count", $"{response.Result!.TotalCount}");
                return BuildSuccessResult(response.Result!.Data, response.Status, null);
            }

            return HandleErrors(response);
        }

        private static ObjectResult HandleErrors<T>(UseCaseResponse<T> response)
        {
            if (response.Result != null && response.Errors == null)
                return BuildError(response.Result, response.Status);

            var hasErrors = response.Errors.IsNullOrEmpty();

            var errorResult = hasErrors
                ? [new ErrorMessage(ErrorCodes.InternalServerError, response.ErrorMessage ?? "Unknown error")]
                : response.Errors;

            return BuildError(errorResult!, response.Status);
        }

        private IActionResult BuildSuccessResult(object data, UseCaseResponseKind status, string? id) => status switch
        {
            UseCaseResponseKind.DataPersisted => new CreatedResult($"{path}/{id ?? string.Empty}", data),
            UseCaseResponseKind.DataAccepted => new AcceptedResult($"{path}/{id ?? string.Empty}", data),
            _ => new OkObjectResult(data),
        };

        private static ObjectResult BuildError(object data, UseCaseResponseKind status) => new(data) { StatusCode = (int)GetErrorHttpStatusCode(status) };

        private static HttpStatusCode GetErrorHttpStatusCode(UseCaseResponseKind status)
        {
            return status switch
            {
                UseCaseResponseKind.RequestValidationError or UseCaseResponseKind.ForeignKeyViolationError or UseCaseResponseKind.BadRequest => HttpStatusCode.BadRequest,
                UseCaseResponseKind.Unauthorized => HttpStatusCode.Unauthorized,
                UseCaseResponseKind.Forbidden => HttpStatusCode.Forbidden,
                UseCaseResponseKind.NotFound => HttpStatusCode.NotFound,
                UseCaseResponseKind.UniqueViolationError => HttpStatusCode.Conflict,
                UseCaseResponseKind.Unavailable => HttpStatusCode.ServiceUnavailable,
                UseCaseResponseKind.BadGateway => HttpStatusCode.BadGateway,
                UseCaseResponseKind.UnprocessableEntity => HttpStatusCode.UnprocessableEntity,
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }
}
