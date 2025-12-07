using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using backend.Data;
using backend.Services;
using Moq;

namespace backend.Tests.Integration;

/// <summary>
/// Factory base para crear instancias de la aplicación para pruebas de integración
/// Configura base de datos en memoria y mock del servicio de email
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Configurar ambiente como "Test" para deshabilitar ConfigurationValidator
        builder.UseEnvironment("Test");

        // Configurar appsettings de prueba
        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Limpiar configuración existente
            config.Sources.Clear();

            // Agregar configuración de prueba en memoria
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = "DataSource=:memory:",
                ["EmailSettings:Provider"] = "SMTP",
                ["EmailSettings:SmtpServer"] = "localhost",
                ["EmailSettings:SmtpPort"] = "25",
                ["EmailSettings:SmtpUsername"] = "test@test.com",
                ["EmailSettings:SmtpPassword"] = "testpassword",
                ["EmailSettings:FromEmail"] = "test@test.com",
                ["EmailSettings:StudioEmail"] = "studio@test.com",
                ["RateLimiting:EnableEndpointRateLimiting"] = "true",
                ["RateLimiting:GeneralRules:0:Endpoint"] = "*",
                ["RateLimiting:GeneralRules:0:Period"] = "1m",
                ["RateLimiting:GeneralRules:0:Limit"] = "1000",  // Límite muy alto para pruebas
                ["Security:MaxPayloadSizeKB"] = "10",
                ["CorsSettings:AllowedOrigins:0"] = "http://localhost:5173"
            });
        });

        builder.ConfigureServices(services =>
        {
            // Remover el DbContext existente
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Agregar DbContext con base de datos en memoria
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });

            // Remover el servicio de email real y reemplazarlo con un mock
            var emailDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IEmailService));
            if (emailDescriptor != null)
            {
                services.Remove(emailDescriptor);
            }

            // Agregar mock del servicio de email que siempre retorna true
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService
                .Setup(x => x.SendContactNotificationAsync(It.IsAny<Models.ContactMessage>()))
                .ReturnsAsync(true);
            services.AddScoped(_ => mockEmailService.Object);

            // Inicializar la base de datos
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
