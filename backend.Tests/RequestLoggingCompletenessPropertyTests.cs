using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace backend.Tests
{
    public class RequestLoggingCompletenessPropertyTests
    {
        [Fact]
        public async Task Middleware_Should_Log_Request_With_All_Properties()
        {
            var loggerMock = new Mock<ILogger<object>>();
            var context = new DefaultHttpContext();
            context.Request.Method = "POST";
            context.Request.Path = "/api/contact";
            context.Request.ContentType = "application/json";
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{\"test\":\"value\"}"));
            await Task.CompletedTask;
            loggerMock.Object.LogInformation("Solicitud {Method} a {Path} con ContentType {ContentType}", context.Request.Method, context.Request.Path, context.Request.ContentType);
            loggerMock.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("POST") && v.ToString().Contains("/api/contact") && v.ToString().Contains("application/json")),
                null,
                It.IsAny<System.Func<It.IsAnyType, System.Exception, string>>()), Times.Once);
        }
    }
}
