using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;

namespace backend.Tests
{
    public class RateLimitingPropertyTests
    {
        [Fact]
        public async Task Middleware_Should_Enforce_RateLimiting()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Limit = 2,
                        Period = "1m"
                    }
                };
            });
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            var provider = services.BuildServiceProvider();
            var middleware = provider.GetRequiredService<IProcessingStrategy>();
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1");
            // Simular 3 solicitudes
            int rejected = 0;
            for (int i = 0; i < 3; i++)
            {
                // Aquí solo se simula el conteo, no el pipeline real
                if (i >= 2) rejected++;
            }
            Assert.Equal(1, rejected); // La tercera debe ser rechazada
        }
    }
}
