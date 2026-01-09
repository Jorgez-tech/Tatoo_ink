using backend.Data;
using backend.Middleware;
using backend.Services;
using backend.Utils;
using backend.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using AspNetCoreRateLimit;
using Ganss.Xss;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Validar configuración antes de construir la app (solo si no es entorno de Test)
if (builder.Environment.EnvironmentName != "Test")
{
    ConfigurationValidator.Validate(builder.Configuration);
}

// Configurar Serilog
builder.Host.UseSerilog((context, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
        .MinimumLevel.Information();
});

// Configurar DbContext con cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ContactRequestValidator>();

// Configurar servicio de email según el proveedor configurado
var emailProvider = builder.Configuration["EmailSettings:Provider"];
if (emailProvider?.ToLower() == "sendgrid")
{
    builder.Services.AddScoped<IEmailService, SendGridEmailService>();
}
else
{
    builder.Services.AddScoped<IEmailService, SmtpEmailService>();
}

// Registrar servicio de contacto
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();

// Configuración de rate limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("RateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();

// Health checks: base de datos y configuracion critica
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("database");

var app = builder.Build();

// Middleware de manejo global de excepciones
app.UseMiddleware<GlobalExceptionMiddleware>();

// Middleware de validación de tamaño de payload
app.Use(async (context, next) =>
{
    var maxKb = builder.Configuration.GetValue<int>("Security:MaxPayloadSizeKB", 10);
    var maxBytes = maxKb * 1024;
    var feature = context.Features.Get<IHttpMaxRequestBodySizeFeature>();
    if (feature != null)
    {
        feature.MaxRequestBodySize = maxBytes;
    }
    await next();
});

app.Use(async (context, next) =>
{
    if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json"))
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;
        var sanitizer = context.RequestServices.GetRequiredService<IHtmlSanitizer>();
        var sanitized = sanitizer.Sanitize(body);
        if (sanitized != body)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("{\"error\":\"Entrada contiene contenido potencialmente peligroso.\"}");
            return;
        }
    }
    await next();
});

app.UseCors(policy =>
{
    var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    policy.WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seguridad adicional solo en producción
if (!app.Environment.IsDevelopment())
{
    // HSTS ayuda a forzar HTTPS en clientes una vez que se ha establecido
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.UseIpRateLimiting();

// Endpoint de health check sencillo para monitorización (DB + configuración)
app.MapHealthChecks("/health");

app.MapControllers();

// Inicializar base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();

// Make Program class accessible for integration tests
public partial class Program { }
