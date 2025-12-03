using backend.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System;

namespace backend.Tests
{
    public class GlobalExceptionMiddlewarePropertyTests
    {
        [Fact]
        public async Task Should_Return_500_And_Generic_Message_On_DbException()
        {
            var loggerMock = new Mock<ILogger<GlobalExceptionMiddleware>>();
            var middleware = new GlobalExceptionMiddleware(async context =>
            {
                throw new Exception("Simulated DB error");
            }, loggerMock.Object);
            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;
            await middleware.InvokeAsync(context);
            Assert.Equal(500, context.Response.StatusCode);
            responseStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(responseStream, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            Assert.Contains("Ocurrió un error interno", body);
        }

        [Fact]
        public async Task Should_Log_Error_With_StackTrace_And_Context()
        {
            var loggerMock = new Mock<ILogger<GlobalExceptionMiddleware>>();
            var middleware = new GlobalExceptionMiddleware(async context =>
            {
                throw new Exception("Test error");
            }, loggerMock.Object);
            var context = new DefaultHttpContext();
            context.Request.Path = "/api/contact";
            await middleware.InvokeAsync(context);
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Excepción global capturada") && v.ToString().Contains("/api/contact")),
                    It.Is<Exception>(ex => ex.Message.Contains("Test error")),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ), Times.Once);
        }
    }
}
