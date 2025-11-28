# Backend Integration - Quick Start

Guia rapida para integrar el backend ASP.NET Core con el frontend React.

## Checklist Pre-Integracion

Antes de comenzar, asegurate de tener:

- [ ] .NET 8.0 SDK instalado
- [ ] Visual Studio 2022 o VS Code
- [ ] SQL Server o base de datos configurada (opcional)
- [ ] Cuenta de email service (SendGrid, SMTP, etc.) - opcional

---

## Inicio Rapido (5 minutos)

### 1. Configurar Variables de Entorno

Copia `.env.example` a `.env`:

```bash
cp .env.example .env
```

Edita `.env`:

```env
VITE_API_BASE_URL=http://localhost:5000
VITE_USE_MOCK_API=false
```

### 2. Crear Proyecto Backend

```bash
# Crear solucion y proyecto API
dotnet new webapi -n InkStudio.Api
cd InkStudio.Api

# Agregar paquetes necesarios
dotnet add package Microsoft.AspNetCore.Cors
dotnet add package FluentValidation.AspNetCore
```

### 3. Crear Endpoint de Contacto

**Models/ContactRequest.cs:**

```csharp
using System.ComponentModel.DataAnnotations;

namespace InkStudio.Api.Models;

public class ContactRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formato de telefono invalido")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "El mensaje es obligatorio")]
    [MinLength(10, ErrorMessage = "El mensaje debe tener al menos 10 caracteres")]
    public string Message { get; set; } = string.Empty;
}

public class ContactResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
```

**Controllers/ContactController.cs:**

```csharp
using Microsoft.AspNetCore.Mvc;
using InkStudio.Api.Models;

namespace InkStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;

    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Post([FromBody] ContactRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ContactResponse
            {
                Success = false,
                Message = "Datos invalidos"
            });
        }

        try
        {
            // TODO: Implementar logica de negocio
            // - Enviar email
            // - Guardar en base de datos
            // - Notificar administrador

            _logger.LogInformation(
                "Contacto recibido de {Name} ({Email})",
                request.Name,
                request.Email
            );

            return Ok(new ContactResponse
            {
                Success = true,
                Message = "Mensaje recibido correctamente"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al procesar contacto");
            return StatusCode(500, new ContactResponse
            {
                Success = false,
                Message = "Error al procesar el mensaje"
            });
        }
    }
}
```

### 4. Configurar CORS

**Program.cs:**

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",  // Vite dev server
            "https://inkstudio.cl"    // Production
        )
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

### 5. Ejecutar Backend

```bash
dotnet run
```

El backend estara disponible en `http://localhost:5000`

### 6. Probar Integracion

En otra terminal, ejecuta el frontend:

```bash
npm run dev
```

Abre `http://localhost:5173` y prueba el formulario de contacto.

---

## Testing

### Test con curl

```bash
curl -X POST http://localhost:5000/api/contact \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test User",
    "email": "test@example.com",
    "phone": "+56 9 1234 5678",
    "message": "Este es un mensaje de prueba"
  }'
```

### Test con Swagger

Abre `http://localhost:5000/swagger` y prueba el endpoint desde la UI.

---

## Agregar Envio de Emails (Opcional)

### Opcion 1: SMTP

**appsettings.json:**

```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "noreply@inkstudio.cl",
    "SenderName": "Ink Studio",
    "Username": "your-email@gmail.com",
    "Password": "your-app-password"
  }
}
```

**Services/EmailService.cs:**

```csharp
using System.Net;
using System.Net.Mail;

public interface IEmailService
{
    Task SendContactEmailAsync(ContactRequest request);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendContactEmailAsync(ContactRequest request)
    {
        var settings = _config.GetSection("EmailSettings");

        using var client = new SmtpClient(settings["SmtpServer"],
            int.Parse(settings["SmtpPort"]!))
        {
            Credentials = new NetworkCredential(
                settings["Username"],
                settings["Password"]
            ),
            EnableSsl = true
        };

        var message = new MailMessage
        {
            From = new MailAddress(settings["SenderEmail"]!, settings["SenderName"]),
            Subject = $"Nuevo contacto de {request.Name}",
            Body = $@"
                <h2>Nuevo mensaje de contacto</h2>
                <p><strong>Nombre:</strong> {request.Name}</p>
                <p><strong>Email:</strong> {request.Email}</p>
                <p><strong>Telefono:</strong> {request.Phone ?? "No proporcionado"}</p>
                <p><strong>Mensaje:</strong></p>
                <p>{request.Message}</p>
            ",
            IsBodyHtml = true
        };

        message.To.Add("contacto@inkstudio.cl");

        await client.SendMailAsync(message);
    }
}
```

Registrar servicio en **Program.cs:**

```csharp
builder.Services.AddScoped<IEmailService, EmailService>();
```

Usar en **ContactController.cs:**

```csharp
private readonly IEmailService _emailService;

public ContactController(
    ILogger<ContactController> logger,
    IEmailService emailService)
{
    _logger = logger;
    _emailService = emailService;
}

[HttpPost]
public async Task<IActionResult> Post([FromBody] ContactRequest request)
{
    // ... validacion ...

    await _emailService.SendContactEmailAsync(request);

    // ... respuesta ...
}
```

### Opcion 2: SendGrid

```bash
dotnet add package SendGrid
```

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;

public class SendGridEmailService : IEmailService
{
    private readonly string _apiKey;

    public SendGridEmailService(IConfiguration config)
    {
        _apiKey = config["SendGrid:ApiKey"]!;
    }

    public async Task SendContactEmailAsync(ContactRequest request)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("noreply@inkstudio.cl", "Ink Studio");
        var to = new EmailAddress("contacto@inkstudio.cl");
        var subject = $"Nuevo contacto de {request.Name}";
        var htmlContent = $@"
            <h2>Nuevo mensaje de contacto</h2>
            <p><strong>Nombre:</strong> {request.Name}</p>
            <p><strong>Email:</strong> {request.Email}</p>
            <p><strong>Telefono:</strong> {request.Phone ?? "No proporcionado"}</p>
            <p><strong>Mensaje:</strong></p>
            <p>{request.Message}</p>
        ";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
        await client.SendEmailAsync(msg);
    }
}
```

---

## Agregar Base de Datos (Opcional)

### Entity Framework Core

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

**Models/ContactMessage.cs:**

```csharp
public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
}
```

**Data/AppDbContext.cs:**

```csharp
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<ContactMessage> ContactMessages { get; set; }
}
```

**Program.cs:**

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
```

**appsettings.json:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=InkStudio;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Crear migracion:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Deployment

### Frontend (Vercel/Netlify)

Configurar variable de entorno:

```
VITE_API_BASE_URL=https://api.inkstudio.cl
VITE_USE_MOCK_API=false
```

### Backend (Azure/AWS)

1. Publicar aplicacion:

```bash
dotnet publish -c Release
```

2. Configurar CORS con dominio de produccion
3. Configurar variables de entorno en el hosting
4. Configurar SSL/HTTPS

---

## Recursos

- [Documentacion completa](./BACKEND-INTEGRATION.md)
- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [SendGrid Docs](https://docs.sendgrid.com)

---

## Troubleshooting

**Error de CORS:**

- Verificar que el origen este en la lista de CORS
- Verificar que `UseCors()` este antes de `UseAuthorization()`

**Error 404:**

- Verificar que la ruta sea `/api/contact`
- Verificar que el backend este corriendo

**Error de validacion:**

- Verificar que los campos cumplan las validaciones
- Revisar logs del backend

---

**Tiempo estimado:** 30-60 minutos para integracion basica
