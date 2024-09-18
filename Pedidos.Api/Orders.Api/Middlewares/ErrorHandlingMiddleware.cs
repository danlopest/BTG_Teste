using FluentValidation;
using Orders.Borders.Shared;
using Orders.Borders.Shared.Exceptions;
using Orders.Borders.Shared.Extensions;
using System.Net;
using System.Text.Json;

namespace Orders.Api.Middlewares
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger)
        {
            var (code, errorMessage, errors) = exception switch
            {
                RepositoryException ex => (ex.StatusCode, ex.Message, ex.Errors),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error", new ErrorMessage[] { new(ErrorCodes.InternalServerError, "Internal Server Error") })
            };

            if (errorMessage is not null)
            {
                logger.LogError(exception, "{@ErrorMessage} {@Errors}", errorMessage, errors);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonSerializer.Serialize(errors, JsonExtensions.DefaultSerializerOptions));
        }
    }
}
