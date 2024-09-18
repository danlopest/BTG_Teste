using Orders.Borders.Shared.Exceptions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Orders.Borders.Shared.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T?> Get<T>(this HttpClient httpClient, string requestUri, string errorMessage) where T : class
           => await Send<T>(httpClient, HttpMethod.Get, requestUri, null, errorMessage);

    public static async Task<T> Post<T>(this HttpClient httpClient, string requestUri, object? data, string errorMessage) where T : class
        => (await Send<T>(httpClient, HttpMethod.Post, requestUri, data, errorMessage))!;

    public static async Task<T> Put<T>(this HttpClient httpClient, string requestUri, object data, string errorMessage) where T : class
    => (await Send<T>(httpClient, HttpMethod.Put, requestUri, data, errorMessage))!;

    private static async Task<T?> Send<T>(HttpClient httpClient, HttpMethod httpMethod, string requestUri, object? data, string errorMessage) where T : class
    {
        try
        {
            using var requestMessage = new HttpRequestMessage(httpMethod, requestUri);

            if (httpMethod != HttpMethod.Get && data is not null)
            {
                if (data is HttpContent content)
                    requestMessage.Content = content;
                else
                    requestMessage.Content = JsonContent.Create(data, options: JsonExtensions.DefaultSerializerOptions);
            }

            using var response = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

            try
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsJsonAsync<T>();

                if (httpMethod == HttpMethod.Get && response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                throw await HandleErrors(errorMessage, response);
            }
            catch (JsonException exception)
            {
                throw await HandleException(requestUri, exception, response);
            }
        }
        catch (Exception exception) when (exception is not RepositoryException)
        {
            var messageError = $@"API replied with an invalid body. Request path: {requestUri}";
            throw new RepositoryException(exception, messageError, null, HttpStatusCode.BadGateway);
        }
    }


    private static async Task<RepositoryException> HandleErrors(string errorMessage, HttpResponseMessage response)
    {
        IEnumerable<ErrorMessage> errors;

        try
        {
            errors = await response.Content.ReadAsJsonAsync<IEnumerable<ErrorMessage>>();
        }
        catch (JsonException)
        {
            errors = [await response.Content.ReadAsJsonAsync<ErrorMessage>()];
        }
        catch (Exception)
        {
            errors = [new ErrorMessage("00:01","Error to execute service")];
        }

        return new RepositoryException($"{errorMessage} - {response.StatusCode}", errors, response.StatusCode);
    }

    private static async Task<RepositoryException> HandleException(string requestUri, JsonException exception, HttpResponseMessage response)
    {
        var rawBody = await response.Content.ReadAsStringAsync();
        var messageError = $@"API replied with an invalid body. Request path: {requestUri} Status code: {response.StatusCode} Body: {rawBody}";
        return new RepositoryException(exception, messageError, null, response.StatusCode);
    }

    private static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
    {
        if (typeof(T) == typeof(string))
            return (T)Convert.ChangeType(await content.ReadAsStringAsync(), typeof(T));

        using var stream = await content.ReadAsStreamAsync();
        return JsonSerializer.Deserialize<T>(stream, JsonExtensions.DefaultSerializerOptions)!;
    }
}
