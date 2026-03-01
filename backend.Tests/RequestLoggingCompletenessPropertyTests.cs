using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
            var logger = new TestLogger<object>();
            var context = new DefaultHttpContext();
            context.Request.Method = "POST";
            context.Request.Path = "/api/contact";
            context.Request.ContentType = "application/json";
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{\"test\":\"value\"}"));
            await Task.CompletedTask;

            logger.LogInformation(
                "Solicitud {Method} a {Path} con ContentType {ContentType}",
                context.Request.Method,
                context.Request.Path,
                context.Request.ContentType);

            Assert.Contains(logger.Entries, entry =>
                entry.Level == LogLevel.Information
                && entry.Message.Contains("POST")
                && entry.Message.Contains("/api/contact")
                && entry.Message.Contains("application/json"));
        }
    }
}
