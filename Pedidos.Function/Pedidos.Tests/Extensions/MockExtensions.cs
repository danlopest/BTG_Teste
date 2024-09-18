using Microsoft.Extensions.Logging;
using Moq;

namespace Pedidos.Tests.Extensions
{
    public static class MockExtensions
    {
        public static void AssertLog<T>(this Mock<ILogger<T>> logger, LogLevel logLevel, string message, Exception? exception, Times times)
        {
            logger.Verify(it => it.Log(
                logLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(message)),
                exception,
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)), times);
        }
    }
}
