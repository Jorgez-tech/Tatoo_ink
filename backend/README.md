# Backend - Ink Studio API

Backend API RESTful para la landing page de Ink Studio, construido con ASP.NET Core .NET 8.0.

## Arquitectura de Capas

```
Controllers (HTTP Layer)
    |
    v
Services (Business Logic)
    |
    v
Data (Persistence Layer)
    |
    v
Database (SQLite)
```

### Responsabilidades

- **Controllers:** Reciben requests HTTP, validan ModelState, retornan responses
- **Services:** Implementan logica de negocio, orquestan operaciones, manejan errores
- **Data (DbContext):** Acceso a datos con Entity Framework Core
- **Validators:** Reglas de validacion con FluentValidation
- **Middleware:** Manejo global de excepciones, logging, seguridad

## Endpoints Disponibles

### POST /api/contact

Envia un mensaje de contacto y opcionalmente solicita una cita.

**Request:**
```json
{
  "name": "Juan Perez",
  "email": "juan@example.com",
  "phone": "123456789",
  "message": "Hola, quiero un tatuaje de dragon",
  "wantsAppointment": true
}
```

**Response 200 OK:**
```json
{
  "success": true,
  "message": "Mensaje recibido correctamente. Nos pondremos en contacto contigo pronto.",
  "id": 1
}
```

**Response 400 Bad Request:**
```json
{
  "success": false,
  "message": "Datos de entrada invalidos"
}
```

**Response 500 Internal Server Error:**
```json
{
  "success": false,
  "message": "Ocurrio un error al procesar tu mensaje. Por favor, intenta nuevamente."
}
```

### GET /api/gallery (Futuro)

Obtiene imagenes de la galeria del estudio.

Nota: existe un endpoint `GET /api/gallery` en el backend actual. La etiqueta "(Futuro)" se mantiene solo si se planea ampliar su funcionalidad (CRUD, categorías, administración).

Referencia API: ver `docs/API-REST.md`.

## Configuracion de appsettings.json

### Estructura Completa

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=TattooStudioDb.db"
  },
  "EmailSettings": {
    "Provider": "SendGrid",
    "SendGridApiKey": "TU_API_KEY_DE_SENDGRID",
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUsername": "tu-email@gmail.com",
    "SmtpPassword": "tu-password-de-aplicacion",
    "StudioEmail": "studio@inkstudio.com",
    "StudioName": "Ink Studio"
  },
  "CorsSettings": {
    "AllowedOrigins": [
      "http://localhost:5173",
      "http://localhost:3000",
      "https://tu-dominio.com"
    ]
  },
  "RateLimiting": {
    "MaxRequestsPerMinute": 10,
    "EnableRateLimiting": true
  },
  "Security": {
    "MaxPayloadSizeKB": 10
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day"
        }
      }
    ],
    "Enrich": ["FromLogContext"],
    "Properties": {
      "Application": "TattooStudioBackend"
    }
  }
}
```

### Seccion por Seccion

#### ConnectionStrings

**Desarrollo (SQLite):**
```json
"DefaultConnection": "Data Source=TattooStudioDb.db"
```

**Produccion (SQLite):**
```json
"DefaultConnection": "Data Source=TattooStudioDb.db"
```

Nota: en produccion se recomienda definir una ubicacion persistente del archivo `.db` y configurar backups.

#### EmailSettings

**Provider:** `"SendGrid"` o `"Smtp"`

**SendGrid:**
- Crear cuenta en [sendgrid.com](https://sendgrid.com/)
- Obtener API Key desde Settings > API Keys
- Configurar `SendGridApiKey`

**SMTP (Gmail):**
1. Habilitar autenticacion de 2 factores
2. Generar password de aplicacion
3. Configurar `SmtpServer`, `SmtpPort`, `SmtpUsername`, `SmtpPassword`

#### CorsSettings

Lista de origenes permitidos para CORS.

**Desarrollo:** `http://localhost:5173`, `http://localhost:3000`  
**Produccion:** URL de tu frontend desplegado

#### RateLimiting

- `MaxRequestsPerMinute`: Maximo de requests por IP (default: 10)
- `EnableRateLimiting`: `true` para activar

#### Security

- `MaxPayloadSizeKB`: Tamano maximo de payload (default: 10 KB)

## Documentación técnica

- Arquitectura: `docs/ARCHITECTURE.md`
- API: `docs/API-REST.md`
- Seguridad: `docs/SECURITY.md`
- QA: `docs/QA.md`

## Comandos Utiles

### Migraciones de Base de Datos

```bash
# Crear nueva migracion
dotnet ef migrations add NombreDeMigracion --project backend

# Aplicar migraciones
dotnet ef database update --project backend

# Eliminar ultima migracion
dotnet ef migrations remove --project backend

# Generar script SQL
dotnet ef migrations script --project backend
```

### Ejecucion

```bash
# Ejecutar en desarrollo
dotnet run --project backend

# Ejecutar con watch (recarga automatica)
dotnet watch run --project backend

# Ejecutar en produccion
dotnet run --project backend --configuration Release
```

### Pruebas

```bash
# Ejecutar todas las pruebas
dotnet test backend.Tests/backend.Tests.csproj

# Con detalles
dotnet test backend.Tests/backend.Tests.csproj --verbosity normal

# Con cobertura
dotnet test backend.Tests/backend.Tests.csproj --collect:"XPlat Code Coverage"
```

### Build

```bash
# Build en desarrollo
dotnet build backend/backend.csproj

# Build en produccion
dotnet build backend/backend.csproj --configuration Release

# Publicar para despliegue
dotnet publish backend/backend.csproj --configuration Release --output ./publish
```

## Dependencias Principales

```xml
AspNetCoreRateLimit 5.0.0
FluentValidation.AspNetCore 11.3.1
HtmlSanitizer 9.0.889
Microsoft.EntityFrameworkCore 8.0.0
Microsoft.EntityFrameworkCore.Sqlite 8.0.0
SendGrid 9.29.1
Serilog.AspNetCore 8.0.0
Swashbuckle.AspNetCore 10.0.1
```

## Estructura de Archivos

```
backend/
├── Controllers/
│   ├── ContactController.cs       # POST /api/contact
│   └── GalleryController.cs       # GET /api/gallery (futuro)
├── Services/
│   ├── IContactService.cs         # Interface del servicio de contacto
│   ├── ContactService.cs          # Logica de negocio para contactos
│   ├── IEmailService.cs           # Interface del servicio de email
│   ├── SendGridEmailService.cs    # Implementacion con SendGrid
│   ├── SmtpEmailService.cs        # Implementacion con SMTP
│   ├── IGalleryService.cs         # Interface del servicio de galeria
│   └── GalleryService.cs          # Logica de negocio para galeria
├── Models/
│   ├── ContactMessage.cs          # Entidad EF Core
│   ├── ContactRequestDto.cs       # DTO para requests
│   ├── ContactResponseDto.cs      # DTO para responses
│   └── ServiceResult.cs           # Resultado de operaciones
├── Data/
│   ├── ApplicationDbContext.cs    # DbContext de EF Core
│   └── DbInitializer.cs           # Inicializacion de DB
├── Validators/
│   └── ContactRequestValidator.cs # Reglas de FluentValidation
├── Middleware/
│   └── GlobalExceptionMiddleware.cs # Manejo de excepciones
├── Utils/
│   └── ConfigurationValidator.cs  # Validacion de configuracion
├── Migrations/                     # Migraciones de EF Core
├── appsettings.json               # Configuracion base
├── appsettings.Development.json   # Configuracion de desarrollo
├── appsettings.Production.json    # Configuracion de produccion
├── Program.cs                     # Entry point y configuracion
└── backend.csproj                 # Archivo de proyecto
```

## Seguridad Implementada

- **Rate Limiting:** 10 requests/minuto por IP
- **Payload Size Validation:** Maximo 10 KB
- **HTML Sanitization:** Prevencion de XSS
- **CORS:** Solo origenes configurados
- **Input Validation:** FluentValidation en todos los DTOs
- **Error Handling:** Mensajes genericos, detalles en logs
- **HTTPS:** Forzado en produccion

## Logging

Logs estructurados con Serilog:
- **Consola:** Desarrollo
- **Archivo:** `logs/log-YYYY-MM-DD.txt` (rolling diario)
- **Niveles:** Information, Warning, Error

## Troubleshooting

### Error: "No se encuentra la base de datos"
```bash
dotnet ef database update --project backend
```

### Error: "CORS policy blocked"
Verificar que el origen del frontend este en `CorsSettings:AllowedOrigins`

### Error: "Email failed to send"
- Verificar credenciales en `EmailSettings`
- Revisar logs en `logs/log-YYYY-MM-DD.txt`

### Error: "Rate limit exceeded"
Esperar 1 minuto o ajustar `RateLimiting:MaxRequestsPerMinute`

## Documentacion Adicional

- **Swagger UI:** `https://localhost:7000/swagger` (solo desarrollo)
- **API Spec:** Ver `docs/NEXT-STEPS.md`
- **Tests:** 39 pruebas unitarias en `backend.Tests/`

---

**Nota:** Para produccion, usar variables de entorno para valores sensibles (API keys, passwords).
