# Design Document

## Overview

El backend del estudio de tatuajes es una Web API RESTful construida con ASP.NET Core que proporciona un endpoint para procesar mensajes de contacto desde el frontend. El sistema implementa una arquitectura en capas que separa las responsabilidades de presentación, lógica de negocio y acceso a datos. Utiliza Entity Framework Core para la persistencia de datos y un servicio de correo electrónico (SendGrid o SMTP) para notificaciones.

El flujo principal del sistema es: recibir solicitud HTTP → validar datos → persistir en base de datos → enviar notificación por correo → retornar respuesta al cliente.

## Architecture

### Architectural Pattern

El sistema sigue una arquitectura en capas (Layered Architecture) con tres capas principales:

**1. Presentation Layer (API Controllers)**

- Responsabilidad: Recibir solicitudes HTTP, validar formato de entrada, invocar servicios de negocio, retornar respuestas HTTP
- Componentes: ContactController
- Tecnología: ASP.NET Core Web API Controllers

**2. Business Logic Layer (Services)**

- Responsabilidad: Implementar reglas de negocio, coordinar operaciones entre repositorios y servicios externos, manejar transacciones
- Componentes: ContactService, EmailService
- Patrón: Service Layer Pattern

**3. Data Access Layer (Repositories/DbContext)**

- Responsabilidad: Abstraer operaciones de base de datos, ejecutar queries, gestionar conexiones
- Componentes: ApplicationDbContext, ContactMessageRepository (opcional)
- Tecnología: Entity Framework Core

### Cross-Cutting Concerns

- **Logging**: Middleware de logging para registrar todas las solicitudes y errores
- **Exception Handling**: Middleware global para capturar y formatear excepciones
- **Validation**: Data Annotations en DTOs y FluentValidation para reglas complejas
- **Configuration**: IConfiguration para leer appsettings.json y variables de entorno
- **CORS**: Middleware de CORS configurado para el dominio del frontend

### Technology Stack

- **Framework**: ASP.NET Core 8.0 (LTS)
- **ORM**: Entity Framework Core 8.0
- **Database**: SQLite
- **Email Provider**: SendGrid SDK o System.Net.Mail (SMTP)
- **Validation**: FluentValidation
- **Logging**: Serilog con sinks a archivo y consola
- **Configuration**: appsettings.json + variables de entorno (opcional: User Secrets en desarrollo)

## Components and Interfaces

### 1. ContactController (Presentation Layer)

**Responsabilidad**: Exponer endpoint HTTP POST para recibir mensajes de contacto

**Endpoint**:

- `POST /api/contact` - Recibe un mensaje de contacto

**Request DTO**:

```
ContactRequestDto
- Name: string (requerido, max 100 caracteres)
- Email: string (requerido, formato email válido)
- Phone: string (requerido, max 20 caracteres)
- Message: string (requerido, max 1000 caracteres)
- WantsAppointment: bool (requerido)
```

**Response**:

- 200 OK: `{ "success": true, "message": "Mensaje recibido correctamente" }`
- 400 Bad Request: `{ "success": false, "errors": ["campo1: error", "campo2: error"] }`
- 500 Internal Server Error: `{ "success": false, "message": "Error interno del servidor" }`

**Dependencias**:

- IContactService (inyectado vía constructor)
- ILogger<ContactController> (inyectado vía constructor)

### 2. IContactService / ContactService (Business Logic Layer)

**Responsabilidad**: Coordinar la lógica de negocio para procesar mensajes de contacto

**Interface**:

```
IContactService
- Task<ServiceResult> ProcessContactMessageAsync(ContactRequestDto request)
```

**ServiceResult**:

```
ServiceResult
- bool Success
- string Message
- List<string> Errors
```

**Flujo de ProcessContactMessageAsync**:

1. Validar datos de entrada (delegado a FluentValidation)
2. Mapear DTO a entidad ContactMessage
3. Persistir en base de datos vía repositorio
4. Si persistencia exitosa: enviar correo vía IEmailService
5. Si envío de correo falla: registrar error pero retornar éxito (mensaje ya guardado)
6. Retornar ServiceResult

**Dependencias**:

- IContactMessageRepository o ApplicationDbContext
- IEmailService
- ILogger<ContactService>

### 3. IEmailService / EmailService (Business Logic Layer)

**Responsabilidad**: Enviar notificaciones por correo electrónico

**Interface**:

```
IEmailService
- Task<bool> SendContactNotificationAsync(ContactMessage message)
```

**Implementaciones**:

- **SendGridEmailService**: Usa SendGrid SDK
- **SmtpEmailService**: Usa System.Net.Mail.SmtpClient

**Configuración requerida**:

- Dirección de correo del estudio (destinatario)
- Credenciales del proveedor (API Key para SendGrid o usuario/contraseña para SMTP)
- Servidor SMTP y puerto (solo para SMTP)

**Formato del correo**:

- Asunto: "Nuevo mensaje de contacto - [Nombre del cliente]"
- Cuerpo: Plantilla HTML con información del cliente y mensaje
- Indicador visual si el cliente solicita cita

**Dependencias**:

- IConfiguration (para leer configuración)
- ILogger<EmailService>

### 4. ApplicationDbContext (Data Access Layer)

**Responsabilidad**: Configurar Entity Framework Core y gestionar entidades

**DbSets**:

- ContactMessages: DbSet<ContactMessage>

**Configuración**:

- Cadena de conexión desde IConfiguration
- Configuración de entidades vía Fluent API o Data Annotations
- Migraciones para crear/actualizar esquema de base de datos

**Dependencias**:

- DbContextOptions<ApplicationDbContext>

### 5. IContactMessageRepository / ContactMessageRepository (Data Access Layer - Opcional)

**Responsabilidad**: Abstraer operaciones CRUD sobre ContactMessage

**Interface**:

```
IContactMessageRepository
- Task<ContactMessage> AddAsync(ContactMessage message)
- Task<ContactMessage> GetByIdAsync(int id)
- Task<IEnumerable<ContactMessage>> GetAllAsync()
```

**Nota**: Este componente es opcional. Se puede usar directamente ApplicationDbContext en ContactService para simplicidad, o implementar el patrón Repository para mayor abstracción y testabilidad.

### 6. GlobalExceptionMiddleware (Cross-Cutting)

**Responsabilidad**: Capturar excepciones no controladas y retornar respuestas HTTP apropiadas

**Comportamiento**:

- Captura todas las excepciones que no fueron manejadas en capas superiores
- Registra el error completo en logs con stack trace
- Retorna respuesta HTTP 500 con mensaje genérico (sin exponer detalles internos)
- Formato de respuesta consistente con otros errores

### 7. ContactRequestValidator (Validation)

**Responsabilidad**: Validar ContactRequestDto usando FluentValidation

**Reglas**:

- Name: NotEmpty, MaxLength(100)
- Email: NotEmpty, EmailAddress (formato RFC 5322)
- Phone: NotEmpty, MaxLength(20)
- Message: NotEmpty, MaxLength(1000)
- WantsAppointment: NotNull

## Data Models

### ContactMessage (Entity)

**Propósito**: Representar un mensaje de contacto en la base de datos

**Propiedades**:

- `Id`: int (Primary Key, Identity)
- `Name`: string (max 100, not null)
- `Email`: string (max 255, not null)
- `Phone`: string (max 20, not null)
- `Message`: string (max 1000, not null)
- `WantsAppointment`: bool (not null)
- `CreatedAt`: DateTime (not null, default: UTC now)
- `EmailSent`: bool (not null, default: false)
- `EmailSentAt`: DateTime? (nullable)

**Índices**:

- Index en `CreatedAt` (para consultas ordenadas por fecha)
- Index en `Email` (para búsquedas por cliente)

**Tabla en base de datos**: `ContactMessages`

### ContactRequestDto (Data Transfer Object)

**Propósito**: Recibir datos del frontend en el endpoint POST

**Propiedades**:

- `Name`: string
- `Email`: string
- `Phone`: string
- `Message`: string
- `WantsAppointment`: bool

**Validación**: Aplicada vía FluentValidation antes de procesar

### ContactResponseDto (Data Transfer Object)

**Propósito**: Retornar respuesta al frontend

**Propiedades**:

- `Success`: bool
- `Message`: string
- `Errors`: List<string> (opcional, solo en caso de error)

## Correctness Properties

_A property is a characteristic or behavior that should hold true across all valid executions of a system-essentially, a formal statement about what the system should do. Properties serve as the bridge between human-readable specifications and machine-verifiable correctness guarantees._

### Property 1: Valid input acceptance

_For any_ ContactRequestDto with all required fields present and valid (non-empty name/phone/message, valid email format), the system should accept the request and create a ContactMessage entity with all fields preserved exactly as provided.
**Validates: Requirements 1.1, 1.2, 1.4**

### Property 2: Email format validation

_For any_ string provided as email, the system should accept it if and only if it conforms to RFC 5322 email format standards.
**Validates: Requirements 1.3**

### Property 3: Invalid input rejection

_For any_ ContactRequestDto with missing required fields or invalid email format, the system should reject the request with HTTP 400 status and return descriptive error messages identifying all validation failures.
**Validates: Requirements 1.5, 8.1**

### Property 4: Appointment flag preservation

_For any_ ContactRequestDto with any value of WantsAppointment (true or false), the system should persist that exact boolean value in the ContactMessage entity without modification.
**Validates: Requirements 2.1, 2.2**

### Property 5: Processing independence from appointment flag

_For any_ two ContactRequestDto instances that are identical except for the WantsAppointment flag, the system should process both through the same validation, persistence, and email notification flow without any differences in handling.
**Validates: Requirements 2.3**

### Property 6: Persistence before notification

_For any_ valid ContactRequestDto, the system should persist the ContactMessage to the database and only after successful persistence attempt to send the email notification.
**Validates: Requirements 3.1, 3.5, 4.1**

### Property 7: Unique identifier generation

_For any_ set of ContactMessage entities persisted to the database, each should receive a unique integer identifier (Id) that is never reused or duplicated.
**Validates: Requirements 3.2**

### Property 8: Timestamp recording

_For any_ ContactMessage persisted to the database, the CreatedAt field should be automatically populated with a valid UTC DateTime representing the moment of persistence.
**Validates: Requirements 3.3**

### Property 9: Persistence failure handling

_For any_ ContactRequestDto where database persistence fails, the system should return HTTP 500 status and should not attempt to send email notification.
**Validates: Requirements 3.4**

### Property 10: Complete email content

_For any_ ContactMessage that triggers email notification, the email body should contain all five required pieces of information: client name, email address, phone number, message text, and appointment request status.
**Validates: Requirements 2.4, 4.2**

### Property 11: Email failure resilience

_For any_ ContactMessage successfully persisted to the database, if the email notification fails to send, the system should log the error but still return HTTP 200 success response to the client.
**Validates: Requirements 4.4, 8.3**

### Property 12: Success response format

_For any_ ContactRequestDto that is successfully processed (persisted and email sent or email failed gracefully), the system should return HTTP 200 with a success message.
**Validates: Requirements 4.5**

### Property 13: Payload size validation

_For any_ HTTP request to the contact endpoint, if the payload size exceeds 10 KB, the system should reject the request before processing.
**Validates: Requirements 6.1**

### Property 14: Rate limiting enforcement

_For any_ sequence of more than 10 requests from the same IP address within a 60-second window, the system should reject subsequent requests until the time window resets.
**Validates: Requirements 6.2**

### Property 15: Input sanitization

_For any_ ContactRequestDto containing potentially dangerous characters (e.g., `<script>`, SQL injection patterns), the system should sanitize the input to remove or escape these characters before persistence.
**Validates: Requirements 6.3**

### Property 16: Request logging completeness

_For any_ HTTP request received by the contact endpoint, the system should create a log entry containing timestamp, source IP address, and operation result (success/failure).
**Validates: Requirements 6.5**

### Property 17: Configuration validation at startup

_For any_ required configuration value (database connection string, email provider credentials, studio email address, CORS origins) that is missing or invalid, the system should fail to start and log a descriptive error message identifying the missing configuration.
**Validates: Requirements 7.5**

### Property 18: Database error response format

_For any_ error originating from database operations, the system should return HTTP 500 with a generic error message that does not expose internal implementation details or stack traces to the client.
**Validates: Requirements 8.2**

### Property 19: Error logging completeness

_For any_ error that occurs during request processing, the system should create a log entry containing severity level, timestamp, stack trace, and request context information.
**Validates: Requirements 8.5**

## Error Handling

### Error Categories

**1. Validation Errors (HTTP 400)**

- Missing required fields
- Invalid email format
- Payload size exceeded
- Invalid data types

**Response Format**:

```json
{
  "success": false,
  "errors": ["Name is required", "Email format is invalid"]
}
```

**2. Rate Limiting Errors (HTTP 429)**

- Too many requests from same IP

**Response Format**:

```json
{
  "success": false,
  "message": "Too many requests. Please try again later.",
  "retryAfter": 60
}
```

**3. Server Errors (HTTP 500)**

- Database connection failures
- Unexpected exceptions
- Configuration errors

**Response Format**:

```json
{
  "success": false,
  "message": "An internal error occurred. Please try again later."
}
```

**Note**: Server errors should never expose internal details like stack traces or database schema information to clients.

### Error Handling Strategy

**Controller Level**:

- Catch validation exceptions from FluentValidation
- Return 400 with validation error details
- Let other exceptions bubble up to middleware

**Service Level**:

- Catch and log specific exceptions (database, email)
- Return ServiceResult with appropriate success/failure status
- Include error messages suitable for logging but not client exposure

**Middleware Level**:

- Global exception handler catches all unhandled exceptions
- Logs complete error details (stack trace, context)
- Returns generic 500 response to client
- Ensures no sensitive information leaks

### Logging Strategy

**Log Levels**:

- **Information**: Successful requests, email sent successfully
- **Warning**: Email send failed (but request succeeded), rate limit triggered
- **Error**: Database errors, validation failures, unexpected exceptions
- **Critical**: Startup configuration failures, database connection lost

**Log Content**:

- Timestamp (UTC)
- Log level
- Request ID (correlation)
- Source IP address
- Endpoint called
- User input (sanitized, no sensitive data)
- Error message and stack trace (for errors)
- Execution time

**Log Destinations**:

- Development: Console + File (rolling)
- Production: File (rolling) + APM/observabilidad del hosting (si aplica)

## Testing Strategy

### Unit Testing

**Components to Test**:

- **ContactRequestValidator**: Test all validation rules with valid and invalid inputs
- **ContactService**: Test business logic con dependencias reales (DbContext SQLite + email real via SMTP pickup directory)
- **EmailService**: Test de formato y envio usando SMTP pickup directory (genera .eml) o un SMTP local
- **ContactController**: Test HTTP request/response handling with mocked service

**Testing Framework**: xUnit
**Mocking Framework**: Moq
**Assertion Library**: FluentAssertions

**Example Unit Tests**:

- Validator rejects empty name
- Validator accepts valid email format
- Service returns error when repository throws exception
- Service logs error when email fails but returns success
- Controller returns 400 for validation errors
- Controller returns 200 for successful processing

### Property-Based Testing

**Property Testing Library**: FsCheck (for C#/.NET)

**Configuration**:

- Minimum 100 iterations per property test
- Custom generators for ContactRequestDto with valid/invalid data
- Shrinking enabled to find minimal failing cases

**Properties to Test**:
Each property test must be tagged with a comment referencing the design document property:

- `// Feature: tattoo-studio-backend, Property 1: Valid input acceptance`

**Key Properties**:

1. **Valid input acceptance** - Generate random valid DTOs, verify all are accepted and persisted correctly
2. **Email format validation** - Generate random email strings, verify validation matches RFC 5322
3. **Invalid input rejection** - Generate random invalid DTOs, verify all are rejected with 400
4. **Appointment flag preservation** - Generate random DTOs with both flag values, verify preservation
5. **Unique identifier generation** - Generate multiple messages, verify all IDs are unique
6. **Timestamp recording** - Generate random messages, verify all have valid timestamps
7. **Complete email content** - Generate random messages, verify emails contain all required fields
8. **Input sanitization** - Generate strings with dangerous characters, verify sanitization

**Custom Generators**:

- ValidContactRequestDtoGenerator: Generates DTOs with all valid fields
- InvalidContactRequestDtoGenerator: Generates DTOs with various validation failures
- EmailStringGenerator: Generates both valid and invalid email formats
- DangerousStringGenerator: Generates strings with XSS and SQL injection patterns

### Integration Testing

**Scope**: Test complete flow from HTTP request to database persistence and email sending

**Test Database**: SQLite real (archivo temporal por suite de tests)

**Email Testing**: Usar un servidor SMTP local (por ejemplo smtp4dev) o SMTP en modo pickup directory para generar archivos .eml

**Key Integration Tests**:

- End-to-end successful contact message submission
- Database persistence verification
- Email notification verification
- Error handling across layers
- CORS configuration
- Rate limiting behavior

**Testing Framework**: xUnit with WebApplicationFactory for in-memory API hosting

### Manual Testing

**Postman Collection**: Create collection with:

- Valid contact submission
- Invalid submissions (missing fields, bad email)
- Payload size limit test
- Rate limiting test
- CORS test from different origins

**Test Environments**:

- Local development (localhost)
- Staging (hosting similar a produccion)
- Production smoke tests (limited)

## Deployment Considerations

### Environment Configuration

**Development**:

- SQLite (archivo local)
- SMTP local para pruebas de email (por ejemplo smtp4dev) o pickup directory
- User Secrets / variables de entorno para configuración sensible
- CORS: Allow localhost:5173 (Vite default)

**Staging**:

- SQLite (archivo local con datos acotados)
- SendGrid (si aplica) o SMTP
- Variables de entorno / secret store del hosting
- CORS: Allow staging frontend URL

**Production**:

- SQLite (archivo persistente con estrategia de backup)
- SendGrid production account (si aplica) o SMTP
- Variables de entorno / secret store del hosting
- CORS: Allow production frontend domain only
- Application Insights for monitoring
- Rate limiting with distributed cache (Redis)

### Database Migrations

**Strategy**: Code-First with EF Core Migrations

**Process**:

1. Create initial migration: `Add-Migration InitialCreate`
2. Apply to development: `Update-Database`
3. Generate SQL script for staging/production: `Script-Migration`
4. Review and apply scripts manually in higher environments
5. Never run `Update-Database` directly in production

**Migration Naming**: Use descriptive names with date prefix (e.g., `20240115_AddContactMessages`)

### Monitoring and Observability

**Metrics to Track**:

- Request count and response times
- Error rate by type (validation, database, email)
- Email send success/failure rate
- Database connection pool usage
- Rate limit triggers per IP

**Alerts**:

- Error rate exceeds 5% over 5 minutes
- Email send failure rate exceeds 10%
- Database connection failures
- Application startup failures

**Health Checks**:

- `/health` endpoint checking:
  - Database connectivity
  - Email service configuration
  - Required configuration presence

## Security Considerations

### Input Validation and Sanitization

- All input validated with FluentValidation before processing
- HTML encoding applied to all text fields before email sending
- SQL injection prevented by EF Core parameterized queries
- Maximum field lengths enforced at validation and database level

### Rate Limiting

- IP-based rate limiting: 10 requests per minute per IP
- Distributed rate limiting in production (Redis-backed)
- Rate limit headers in response: `X-RateLimit-Limit`, `X-RateLimit-Remaining`

### CORS Configuration

- Whitelist only specific frontend domain(s)
- No wildcard (\*) origins in production
- Credentials not allowed (no cookies/auth headers needed)

### Secrets Management

- Never commit secrets to source control
- Development: User Secrets or .env file (gitignored)
- Production: secret store del hosting o equivalente
- Rotate email API keys quarterly

### HTTPS Enforcement

- Require HTTPS in production
- HSTS header enabled
- Redirect HTTP to HTTPS

### Logging Security

- Never log sensitive data (full email content, phone numbers in plain text)
- Sanitize user input before logging
- Secure log storage with access controls
- Log retention policy (90 days)

## Performance Considerations

### Expected Load

- Low traffic: ~100 submissions per day
- Peak: ~10 submissions per hour
- Average response time target: <500ms
- Database: Small dataset (<10,000 records per year)

### Optimization Strategies

**Database**:

- Indexes on CreatedAt and Email columns
- Connection pooling (default EF Core behavior)
- Async operations throughout

**Email Sending**:

- Fire-and-forget pattern (don't block response on email send)
- Consider background job queue for high volume (Hangfire o funciones serverless)
- Retry logic for transient email failures

**Caching**:

- Not required for current scope (no read operations)
- Consider caching configuration values if needed

**Response Compression**:

- Enable Gzip compression for responses
- Minimal benefit for small payloads but good practice

## Future Enhancements

### Phase 2 Potential Features

1. **Admin Dashboard**:

   - View all contact messages
   - Mark messages as responded/resolved
   - Search and filter by date, email, appointment requests

2. **Appointment Scheduling**:

   - Artist can propose available time slots
   - Client receives email with booking link
   - Calendar integration

3. **File Uploads**:

   - Allow clients to upload reference images
   - Store in object storage (por ejemplo S3/Blob Storage)
   - Include in email notification

4. **Multi-language Support**:

   - Accept language preference in request
   - Send emails in client's language
   - Localized error messages

5. **Analytics**:

   - Track conversion rates
   - Popular inquiry times
   - Response time metrics

6. **Webhooks**:
   - Notify external systems of new contacts
   - Integration with CRM systems

### Scalability Path

If traffic grows significantly:

- Move email sending to funciones serverless (si aplica)
- Implement message queue (RabbitMQ o equivalente)
- Add Redis cache for rate limiting and configuration
- Consider read replicas for database if admin dashboard added
- Implement CDN for static assets
- Add load balancer for multiple API instances
