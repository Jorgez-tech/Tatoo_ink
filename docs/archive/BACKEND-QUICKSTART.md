# Backend - Quick Start (Local)

Guía rápida para ejecutar el backend actual de Ink Studio en desarrollo local y conectarlo con el frontend.

## Requisitos

- .NET SDK 8
- (Opcional) `dotnet-ef` si quieres ejecutar migraciones manualmente

## 1) Configurar appsettings (desarrollo)

En `backend/` existe un ejemplo. Crea el archivo real para tu entorno de desarrollo:

- Copiar `backend/appsettings.Development.json.example` a `backend/appsettings.Development.json`
- Completar valores (EmailSettings, CorsSettings, etc.)

Nota: la validación de configuración se ejecuta al inicio (excepto en ambiente `Test`).

## 2) Ejecutar backend

Desde la raíz del repo:

```bash
dotnet run --project backend
```

En desarrollo, Swagger suele estar disponible en `/swagger`.

## 3) Base de datos

El backend usa SQLite. En el arranque:

- Crea la base si no existe.
- Inicializa imágenes de ejemplo para la galería si está vacía.

Si prefieres administrar migraciones manualmente, revisa `backend/README.md`.

## 4) Conectar frontend con backend

En la raíz del repo, configura el frontend con `.env`:

- `VITE_API_BASE_URL` debe apuntar al host/puerto del backend.

Luego ejecuta:

```bash
npm run dev
```

## 5) Verificación rápida

- Probar `POST /api/contact` (ver `API-REST.md`).
- Probar `GET /api/gallery`.

## 6) Ejecutar pruebas del backend

```bash
dotnet test backend.Tests/backend.Tests.csproj
```

## Referencias

- API: `API-REST.md`
- Integración: `BACKEND-INTEGRATION.md`
- Backend (operación/configuración): `backend/README.md`
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
