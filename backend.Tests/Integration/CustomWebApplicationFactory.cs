using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using backend.Data;
using backend.Services;

namespace backend.Tests.Integration;

/// <summary>
/// Factory base para crear instancias de la aplicacion para pruebas de integracion.
///
/// Usa SQLite real (archivo temporal) y envio de email via SMTP pickup directory
/// (genera archivos .eml; no requiere servicios externos).
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private string? _dbPath;
    private string? _pickupDirectory;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _dbPath ??= Path.Combine(Path.GetTempPath(), $"InkStudioTests_{Guid.NewGuid():N}.db");
        _pickupDirectory ??= Path.Combine(Path.GetTempPath(), $"InkStudioEmails_{Guid.NewGuid():N}");
        Directory.CreateDirectory(_pickupDirectory);

        // Configurar ambiente como "Test" para deshabilitar ConfigurationValidator
        builder.UseEnvironment("Test");

        // Configurar appsettings de prueba
        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Limpiar configuraci�n existente
            config.Sources.Clear();

            // Agregar configuraci�n de prueba en memoria
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = $"Data Source={_dbPath}",
                ["EmailSettings:Provider"] = "SMTP",
                ["EmailSettings:SmtpServer"] = "localhost",
                ["EmailSettings:SmtpPort"] = "25",
                ["EmailSettings:StudioEmail"] = "studio@test.com",
                ["EmailSettings:StudioName"] = "Ink Studio",
                ["EmailSettings:PickupDirectory"] = _pickupDirectory,
                ["RateLimiting:EnableEndpointRateLimiting"] = "true",
                ["RateLimiting:GeneralRules:0:Endpoint"] = "*",
                ["RateLimiting:GeneralRules:0:Period"] = "1m",
                ["RateLimiting:GeneralRules:0:Limit"] = "1000",  // Limite alto para pruebas
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
                options.UseSqlite($"Data Source={_dbPath}");
            });

            // Asegurar que el servicio de email use SMTP (pickup directory) sin mocks
            var emailDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IEmailService));
            if (emailDescriptor != null)
            {
                services.Remove(emailDescriptor);
            }
            services.AddScoped<IEmailService, SmtpEmailService>();

            // Inicializar la base de datos
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!string.IsNullOrWhiteSpace(_dbPath))
            {
                try { File.Delete(_dbPath); } catch { }
            }

            if (!string.IsNullOrWhiteSpace(_pickupDirectory))
            {
                try { Directory.Delete(_pickupDirectory, recursive: true); } catch { }
            }
        }

        base.Dispose(disposing);
    }
}
