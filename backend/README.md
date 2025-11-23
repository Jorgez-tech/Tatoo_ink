# Backend - Tattoo Studio Contact API

## 📋 Descripción

Backend API RESTful para el sistema de contacto de un estudio de tatuajes. Permite a los clientes enviar mensajes de contacto y solicitar citas a través de un formulario web.

## 🏗️ Arquitectura

El sistema implementa una arquitectura en capas:

- **Presentation Layer**: Controllers (API endpoints)
- **Business Logic Layer**: Services (lógica de negocio)
- **Data Access Layer**: DbContext/Repositories (acceso a datos)

## 🛠️ Stack Tecnológico

- **Framework**: ASP.NET Core 9.0
- **ORM**: Entity Framework Core (pendiente de instalación)
- **Database**: SQL Server
- **Email**: SendGrid o SMTP
- **Validation**: FluentValidation (pendiente de instalación)
- **Logging**: Serilog (pendiente de instalación)

## 📝 Estado del Proyecto

### ✅ Completado

- [x] Estructura base del proyecto
- [x] Configuración inicial de ASP.NET Core
- [x] Limpieza de módulos no relacionados con el spec

### 🚧 Pendiente (según spec)

- [ ] Modelos de datos (ContactMessage, DTOs)
- [ ] Entity Framework Core y DbContext
- [ ] FluentValidation
- [ ] ContactController (POST /api/contact)
- [ ] ContactService (lógica de negocio)
- [ ] EmailService (SendGrid/SMTP)
- [ ] Middleware de manejo de excepciones
- [ ] Serilog para logging
- [ ] CORS configuration
- [ ] Rate limiting
- [ ] Validación de payload size
- [ ] Migraciones de base de datos

## 📚 Documentación del Spec

El spec completo del proyecto se encuentra en:

- **Requirements**: `.kiro/specs/tattoo-studio-backend/requirements.md`
- **Design**: `.kiro/specs/tattoo-studio-backend/design.md`
- **Tasks**: `.kiro/specs/tattoo-studio-backend/tasks.md`

## 🔄 Historial de Cambios

### 2024-11-23 - Limpieza y Alineación con Spec

**Cambios realizados:**

- ❌ Eliminado módulo de autenticación (AuthController, AuthService, User model)
- ❌ Removida dependencia `System.IdentityModel.Tokens.Jwt`
- ✅ Limpiado Program.cs de referencias a autenticación
- ✅ Proyecto alineado con spec de sistema de contacto
- ✅ Backend listo para implementar funcionalidades del spec

**Razón:**
El proyecto original contenía un sistema de autenticación JWT que no estaba contemplado en el spec. Se eliminó para mantener el alcance enfocado únicamente en el sistema de contacto según lo definido en los requisitos.

## 🚀 Próximos Pasos

Para continuar con la implementación, seguir las tareas definidas en `.kiro/specs/tattoo-studio-backend/tasks.md`:

1. **Tarea 1**: Configurar proyecto ASP.NET Core Web API

   - Instalar paquetes NuGet necesarios
   - Configurar appsettings.json

2. **Tarea 2**: Implementar modelos de datos y DTOs

3. **Tarea 3**: Configurar Entity Framework Core y base de datos

... (continuar según el plan de implementación)

## 📞 Endpoint Principal (Pendiente)

```
POST /api/contact
```

**Request Body:**

```json
{
  "name": "string",
  "email": "string",
  "phone": "string",
  "message": "string",
  "wantsAppointment": boolean
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Mensaje recibido correctamente"
}
```

## ⚙️ Configuración Requerida (Pendiente)

El sistema requerirá las siguientes configuraciones en `appsettings.json`:

- **ConnectionStrings**: Cadena de conexión a SQL Server
- **EmailSettings**: Credenciales de SendGrid o SMTP
- **CorsSettings**: Dominios permitidos para CORS
- **StudioEmail**: Email del estudio para recibir notificaciones

## 🧪 Testing

El proyecto incluirá:

- **Unit Tests**: xUnit con Moq
- **Property-Based Tests**: FsCheck (100+ iteraciones por propiedad)
- **Integration Tests**: WebApplicationFactory con base de datos en memoria

## 📄 Licencia

[Definir licencia del proyecto]
