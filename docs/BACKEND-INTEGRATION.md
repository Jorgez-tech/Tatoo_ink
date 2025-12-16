# Integracion con Backend ASP.NET Core

Este documento describe como integrar el frontend React con el backend ASP.NET Core usando scaffolding.

---

## Autenticacion en Backend ASP.NET Core

### Endpoints

#### Registro de usuario
- **POST** `/api/auth/register`
- **Body:**
```json
{
  "username": "string",
  "email": "string",
  "password": "string"
}
```
- **Response:**
```json
{
  "message": "Registro exitoso"
}
```
- **Errores:**
  - 409: El email ya esta registrado
  - 400: Datos invalidos

#### Login de usuario
- **POST** `/api/auth/login`
- **Body:**
```json
{
  "email": "string",
  "password": "string"
}
```
- **Response:**
```json
{
  "token": "jwt-token"
}
```
- **Errores:**
  - 401: Credenciales invalidas
  - 400: Datos invalidos

---

## Endpoint de Contacto

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
  "name": "Juan Perez",
  "email": "juan@example.com",
  "phone": "+56 9 1234 5678",
  "message": "Me gustaria agendar una cita para un tatuaje."
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

## Configuracion del Frontend

### Variables de Entorno

Crear archivo `.env` en la raiz del proyecto:

```env
# Desarrollo local
VITE_API_BASE_URL=http://localhost:5000

# Desarrollo local con HTTPS
# VITE_API_BASE_URL=https://localhost:7001

# Produccion
# VITE_API_BASE_URL=https://api.inkstudio.cl
```

**Nota:** Si no se define `VITE_API_BASE_URL`, el valor por defecto es `http://localhost:5000`

---

## Modelo de Datos para ASP.NET Core

### DTO (Data Transfer Object)

```csharp
public class ContactRequestDto
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

## Controlador ASP.NET Core

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
                Message = "Datos invalidos",
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

## CORS Configuration

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

## Servicio de Email (Opcional)

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
        // Implementar logica de envio de email
      // Usar SMTP o SendGrid segun configuracion del backend.
    }
}
```

---

## Base de Datos (SQLite)

En este proyecto los mensajes se persisten en una base de datos SQLite via EF Core.

```csharp
public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
  public string Phone { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
  public bool WantsAppointment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

---

## Validacion

El frontend ya incluye validacion, pero el backend tambien debe validar:

- **Nombre:** Requerido, minimo 2 caracteres
- **Email:** Requerido, formato valido
- **Telefono:** Requerido, formato valido
- **Mensaje:** Requerido, minimo 10 caracteres
- **Solicitud de cita:** Requerido (booleano true/false)

---

## Pasos para Integracion

1. **Crear el endpoint en ASP.NET Core:**

   - Crear `ContactController`
   - Crear DTOs (`ContactRequestDto`, `ContactResponseDto`)
   - Implementar logica de negocio (email, BD, etc.)

2. **Configurar CORS:**

   - Permitir origen del frontend
   - Configurar en `Program.cs`

3. **Configurar variables de entorno:**

   - Crear `.env` en el frontend
   - Configurar `VITE_API_BASE_URL`

4. **Probar integracion:**

   - Iniciar backend ASP.NET Core
   - Iniciar frontend con `npm run dev`
   - Verificar CORS
   - Probar formulario de contacto

5. **Desplegar:**
   - Frontend: Vercel, Netlify, etc.
  - Backend: hosting para .NET (VPS, contenedor, u otra plataforma equivalente)
   - Actualizar `VITE_API_BASE_URL` en produccion

---

## Testing

### Frontend

```bash
# Asegurarse de que el backend este corriendo
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
    "message": "Este es un mensaje de prueba",
    "wantsAppointment": false
  }'
```

---

## Notas

- El frontend esta preparado para recibir errores de validacion del backend
- Los mensajes de error se mostraran automaticamente en el formulario
- El formulario maneja estados de loading, success y error
- Requiere backend ASP.NET Core corriendo para funcionar

---

**Ultima actualizacion:** 2025-11-21
