using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pedidos.Borders.Shared.Extensions
{
    public static class JsonExtensions
    {
        public static readonly JsonSerializerOptions DefaultSerializerOptions = new(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static T DeserializeMessage<T>(string message, IReadOnlyDictionary<string, object?> contextBindinData, ILogger logger)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(message, DefaultSerializerOptions)!;
            }
            catch
            {
                contextBindinData.TryGetValue("MessageId", out var messageId);
                logger.LogError($"Error deserializing message '{{@MessageId}}'", messageId);

                throw;
            }
        }
    }
}
