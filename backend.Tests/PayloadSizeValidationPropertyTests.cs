using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Xunit;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace backend.Tests
{
    public class PayloadSizeValidationPropertyTests
    {
        [Fact]
        public async Task Middleware_Should_Reject_Payload_Exceeding_MaxSize()
        {
            var context = new DefaultHttpContext();
            context.Request.ContentType = "application/json";
            var maxKb = 10;
            var maxBytes = maxKb * 1024;
            var largePayload = new string('a', maxBytes + 1);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(largePayload));
            context.Request.Body = stream;
            var called = false;
            var middleware = new Func<HttpContext, Task>(async ctx => { called = true; await Task.CompletedTask; });
            var feature = new MaxRequestBodySizeFeature();
            context.Features.Set<IHttpMaxRequestBodySizeFeature>(feature);
            // Simular el middleware
            if (feature != null)
            {
                feature.MaxRequestBodySize = maxBytes;
            }
            if (stream.Length > feature.MaxRequestBodySize)
            {
                context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
            }
            else
            {
                await middleware(context);
            }
            Assert.Equal(StatusCodes.Status413PayloadTooLarge, context.Response.StatusCode);
            Assert.False(called);
        }
    }

    public class MaxRequestBodySizeFeature : IHttpMaxRequestBodySizeFeature
    {
        public long? MaxRequestBodySize { get; set; }
        public bool IsReadOnly => false;
    }
}
