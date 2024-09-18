namespace Orders.Borders.Shared
{
    public class UseCaseResponse<T>
    {
        public UseCaseResponseKind Status { get; private set; }
        public T? Result { get; private set; }
        public string? ResultId { get; private set; }
        public string? ErrorMessage { get; private set; }
        public IEnumerable<ErrorMessage>? Errors { get; private set; } = [];

        private UseCaseResponse(UseCaseResponseKind status, T result, string? resultId = null)
        {
            Status = status;
            Result = result;
            ResultId = resultId;
        }

        private UseCaseResponse(UseCaseResponseKind status, string? errorMessage = null, IEnumerable<ErrorMessage>? errors = null)
        {
            ErrorMessage = errorMessage;
            Status = status;
            Errors = errors;
        }

        public static UseCaseResponse<T> Success(T result) => new(UseCaseResponseKind.Success, result);

        public static UseCaseResponse<T> BadRequest(ErrorMessage message) => BadRequest([message]);

        public static UseCaseResponse<T> BadRequest(IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.BadRequest, "Bad Request", errors);

        public static UseCaseResponse<T> Unavailable(T result) => new(UseCaseResponseKind.Unavailable, result) { ErrorMessage = "Service Unavailable" };

        public static UseCaseResponse<T> BadGateway(IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.BadGateway, "Bad Gateway", errors);

        public static UseCaseResponse<T> BadGateway(string errorMessage) => new(UseCaseResponseKind.BadGateway, "Bad Gateway",
            [new(ErrorCodes.BadGateway, errorMessage)]);

        public static UseCaseResponse<T> Conflict(IEnumerable<ErrorMessage> errors) => new(UseCaseResponseKind.UniqueViolationError,"Conflict", errors);

        public static UseCaseResponse<T> Persisted(T result, string resultId) => new(UseCaseResponseKind.DataPersisted, result, resultId);

        public static UseCaseResponse<T> Accepted(T result) => new(UseCaseResponseKind.DataAccepted, result);

        public static UseCaseResponse<T> NotFound(ErrorMessage errorMessage) => new(UseCaseResponseKind.NotFound, "Data not found",
            [errorMessage]);

        public static UseCaseResponse<T> InternalServerError(string errorMessage) => new(UseCaseResponseKind.InternalServerError, "Internal Server Error",
            [new(ErrorCodes.InternalServerError, errorMessage)]);

        public bool Success() => string.IsNullOrEmpty(ErrorMessage) && !(Errors?.Any() ?? false);
    }
}
