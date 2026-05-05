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
using System.Text;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.EnvironmentName != "Test")
{
    ConfigurationValidator.Validate(builder.Configuration);
}

builder.Host.UseSerilog((context, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
        .MinimumLevel.Information();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ContactRequestValidator>();

var emailProvider = builder.Configuration["EmailSettings:Provider"];
if (emailProvider?.ToLower() == "sendgrid")
{
    builder.Services.AddScoped<IEmailService, SendGridEmailService>();
}
else
{
    builder.Services.AddScoped<IEmailService, SmtpEmailService>();
}

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IBusinessSettingsService, BusinessSettingsService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("RateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// La autenticación real es manejada por AuthorizationMiddleware (JWT custom con HMACSHA256).
// Este esquema es necesario para que [Authorize(Roles = "..")] de ASP.NET funcione
// contra los Claims que el middleware custom ya inyecta en context.User.
builder.Services.AddAuthentication("Custom")
    .AddScheme<AuthenticationSchemeOptions, NoOpAuthenticationHandler>("Custom", _ => { });

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("database");

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

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

app.UseCors(policy =>
{
    var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    policy.WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // Required for credentials: "include" (refresh token cookie)
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseIpRateLimiting();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    DbInitializer.Initialize(context, builder.Configuration, logger);
}

app.Run();

public partial class Program { }
