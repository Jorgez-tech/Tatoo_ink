# Integración con Backend ASP.NET Core

Este documento describe cómo integrar el frontend React con el backend ASP.NET Core usando scaffolding.

---

## 📋 Endpoint de Contacto

### Request

**Endpoint:** `POST /api/contact`

**Headers:**
```
Content-Type: application/json
```

**Body:**
```json
{
  "name": "string",
  "email": "string",
  "phone": "string",
  "message": "string"
}
```

**Ejemplo:**
```json
{
  "name": "Juan Pérez",
  "email": "juan@example.com",
  "phone": "+56 9 1234 5678",
  "message": "Me gustaría agendar una cita para un tatuaje."
}
```

### Response

**Success (200):**
```json
{
  "success": true,
  "message": "Mensaje recibido correctamente"
}
```

**Error (400/500):**
```json
{
  "success": false,
  "message": "Error description",
  "errors": {
    "field": ["error message"]
  }
}
```

---

## 🔧 Configuración del Frontend

### Variables de Entorno

Crear archivo `.env` en la raíz del proyecto:

```env
# Para desarrollo con backend
VITE_API_BASE_URL=http://localhost:5000
VITE_USE_MOCK_API=false

# Para producción
# VITE_API_BASE_URL=https://api.tudominio.com
# VITE_USE_MOCK_API=false
```

### Modo Mock (Desarrollo sin Backend)

Si no hay backend disponible, el frontend usa modo mock automáticamente:

```env
# Dejar vacío o no definir VITE_API_BASE_URL
VITE_USE_MOCK_API=true
```

---

## 🏗️ Modelo de Datos para ASP.NET Core

### DTO (Data Transfer Object)

```csharp
public class ContactRequestDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formato de teléfono inválido")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "El mensaje es obligatorio")]
    [MinLength(10, ErrorMessage = "El mensaje debe tener al menos 10 caracteres")]
    public string Message { get; set; } = string.Empty;
}
```

### Response DTO

```csharp
public class ContactResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
```

---

## 🎯 Controlador ASP.NET Core

### Ejemplo de Controller

```csharp
using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;
    private readonly IEmailService _emailService;

    public ContactController(
        ILogger<ContactController> logger,
        IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ContactResponseDto
            {
                Success = false,
                Message = "Datos inválidos",
                Errors = ModelState
            });
        }

        try
        {
            // Procesar el mensaje (enviar email, guardar en BD, etc.)
            await _emailService.SendContactEmailAsync(request);

            return Ok(new ContactResponseDto
            {
                Success = true,
                Message = "Mensaje recibido correctamente"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al procesar contacto");
            return StatusCode(500, new ContactResponseDto
            {
                Success = false,
                Message = "Error al procesar el mensaje. Por favor, intenta nuevamente."
            });
        }
    }
}
```

---

## 🔐 CORS Configuration

Para permitir requests desde el frontend, configurar CORS en `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://tudominio.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// ...

app.UseCors("AllowFrontend");
```

---

## 📧 Servicio de Email (Opcional)

Ejemplo de servicio para enviar emails:

```csharp
public interface IEmailService
{
    Task SendContactEmailAsync(ContactRequestDto request);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendContactEmailAsync(ContactRequestDto request)
    {
        // Implementar lógica de envío de email
        // Usar SMTP, SendGrid, Azure Communication Services, etc.
    }
}
```

---

## 🗄️ Base de Datos (Opcional)

Si necesitas guardar los mensajes en una base de datos:

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

---

## ✅ Validación

El frontend ya incluye validación, pero el backend también debe validar:

- **Nombre:** Requerido, mínimo 2 caracteres
- **Email:** Requerido, formato válido
- **Teléfono:** Opcional, formato válido
- **Mensaje:** Requerido, mínimo 10 caracteres

---

## 🚀 Pasos para Integración

1. **Crear el endpoint en ASP.NET Core:**
   - Crear `ContactController`
   - Crear DTOs (`ContactRequestDto`, `ContactResponseDto`)
   - Implementar lógica de negocio (email, BD, etc.)

2. **Configurar CORS:**
   - Permitir origen del frontend
   - Configurar en `Program.cs`

3. **Configurar variables de entorno:**
   - Crear `.env` en el frontend
   - Configurar `VITE_API_BASE_URL`

4. **Probar integración:**
   - Probar con modo mock primero
   - Probar con backend real
   - Verificar CORS

5. **Desplegar:**
   - Frontend: Vercel, Netlify, etc.
   - Backend: Azure, AWS, etc.
   - Actualizar `VITE_API_BASE_URL` en producción

---

## 🔍 Testing

### Frontend (Mock Mode)

```bash
# El formulario funcionará con modo mock
npm run dev
```

### Backend

```bash
# Test con curl
curl -X POST http://localhost:5000/api/contact \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test User",
    "email": "test@example.com",
    "phone": "+56 9 1234 5678",
    "message": "Este es un mensaje de prueba"
  }'
```

---

## 📝 Notas

- El frontend ya está preparado para recibir errores de validación del backend
- Los mensajes de error se mostrarán automáticamente en el formulario
- El formulario maneja estados de loading, success y error
- El modo mock permite desarrollo frontend sin necesidad de backend

---

**Última actualización:** 2025-01-27

