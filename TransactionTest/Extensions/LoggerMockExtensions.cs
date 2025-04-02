using Microsoft.Extensions.Logging;
using Moq;

namespace TransactionTest.Extensions
{
    public static class LoggerMockExtensions
    {
        public static void VerifyInformation<T>(this Mock<ILogger<T>> loggerMock, string expectedMessage)
        {
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains(expectedMessage)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

        public static void VerifyError<T>(this Mock<ILogger<T>> loggerMock, string expectedMessage)
        {
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains(expectedMessage)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}
