using System.Net;

namespace Orders.Borders.Shared.Exceptions
{
    public class RepositoryException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public IEnumerable<ErrorMessage> Errors { get; } = Array.Empty<ErrorMessage>();

        public RepositoryException(string message, IEnumerable<ErrorMessage>? errors, HttpStatusCode statusCode) : base(message)
        {
            Errors = errors ?? Array.Empty<ErrorMessage>();
            StatusCode = statusCode;
        }

        public RepositoryException(Exception innerException, string message, IEnumerable<ErrorMessage>? errors, HttpStatusCode statusCode) : base(message, innerException)
        {
            Errors = errors ?? Array.Empty<ErrorMessage>();
            StatusCode = statusCode;
        }
    }
}
