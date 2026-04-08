# Corrección 3: Autenticación y Control de Acceso Completo

Fecha: 2026-03-17
Estatus: Propuesta de enmienda - CRÍTICA

## Problema

La especificación dice "Zona Interna" sin definir cómo se entra. No hay ni login, ni usuarios, ni seguridad. Esto es deuda técnica crítica.

## Solución propuesta

### 3.1 Nueva entidad: User

Agregar a 05-modelo-datos-conceptual.md:

```
## User

- id (int)
- email (unique, required)
- passwordHash (hashed, required)
- name
- role (admin, artist)
- isActive
- createdAt
- updatedAt
- lastLoginAt (nullable)
```

### 3.2 Nuevos endpoints de autenticación

Agregar a 06-contrato-api-conceptual.md - NUEVA SECCIÓN:

```
## Authentication (PUBLIC)

### POST /api/v1/auth/login

Uso: autenticar usuario (admin o artist)

Request:
{
  "email": "admin@example.com",
  "password": "SecurePassword123"
}

Response (200 OK):
{
  "token": "eyJhbGc...",
  "refreshToken": "xyz789...",
  "user": {
    "id": 1,
    "email": "admin@example.com",
    "name": "Admin User",
    "role": "admin"
  }
}

Response (401 Unauthorized):
{
  "error": {
    "code": "INVALID_CREDENTIALS",
    "message": "Email o contraseña incorrectos"
  }
}

### POST /api/v1/auth/refresh

Uso: renovar token de acceso expirado

Request:
{
  "refreshToken": "xyz789..."
}

Response (200 OK):
{
  "token": "eyJhbGc...",
  "refreshToken": "newRefresh..."
}

### POST /api/v1/auth/logout

Uso: cerrar sesión

Request:
{
  "refreshToken": "xyz789..."
}

Response (204 No Content)
```

### 3.3 User Management (Admin only)

Agregar a 06-contrato-api-conceptual.md:

```
### POST /api/v1/internal/users

Uso: crear nuevo usuario (admin only)

Request:
{
  "email": "artist@example.com",
  "name": "Artista Juan",
  "role": "artist",
  "password": "InitialPassword123"
}

Response (201 Created):
{
  "id": 2,
  "email": "artist@example.com",
  "name": "Artista Juan",
  "role": "artist",
  "isActive": true,
  "createdAt": "2026-03-17T10:00:00Z"
}

### GET /api/v1/internal/users

Uso: listar todos los usuarios (admin only)

Response (200 OK):
[
  {
    "id": 1,
    "email": "admin@example.com",
    "name": "Admin",
    "role": "admin",
    "isActive": true
  }
]

### PATCH /api/v1/internal/users/{id}

Uso: editar usuario (admin only)

Request:
{
  "name": "Nuevo nombre",
  "role": "artist",
  "isActive": false
}
```

### 3.4 Token Scheme (JWT)

Agregar a 07-semantica-errores.md - NUEVA SECCIÓN:

```
## Tokens de Autenticación (JWT)

### Access Token
- Contenido: user_id, email, role
- Expiración: 15 minutos
- Header: Authorization: Bearer <token>

### Refresh Token
- Contenido: user_id, refresh_token_version
- Expiración: 7 días
- Almacenamiento: HttpOnly cookie (seguro contra XSS)

### Códigos de error de autenticación
- 401 INVALID_CREDENTIALS: Email/password incorrectos
- 401 TOKEN_EXPIRED: Access token expirado (usar refresh)
- 401 REFRESH_TOKEN_EXPIRED: Refresh token expirado, volver a login
- 401 INVALID_TOKEN: Token corrupto o inválido
- 403 INSUFFICIENT_PERMISSIONS: Token válido pero rol no permite operación
```

### 3.5 Inicialización (Database Seeding)

Agregar a 06-contrato-api-conceptual.md - NUEVA SECCIÓN:

```
## Database Initialization

En la primera ejecución de la aplicación:

1. Migrations corren automáticamente
2. Seed script crea usuario admin:
   - email: admin@example.com
   - password: DEBE GENERARSE ALEATORIAMENTE (12+ chars)
   - La contraseña se imprime en un log de inicio (para que dev pueda leerla)
   - Admin DEBE cambiar contraseña en primer login

El archivo de seed se guarda en: backend/Data/DbSeeder.cs
```

### 3.6 Matriz de permisos actualizada

Actualizar 04-permisos-acceso.md:

```
| Accion | Visitante | Artist | Admin |
|---|---|---|---|
| Login | Si | Si | Si |
| Ver landing publica | Si | Si | Si |
| Enviar contacto | Si | Si | Si |
| Ver listado solicitudes | No | Si | Si |
| Ver detalle solicitud | No | Si (asignadas) | Si (todas) |
| Cambiar estado solicitud | No | Si | Si |
| Agregar nota | No | Si | Si |
| Crear usuario | No | No | Si |
| Listar usuarios | No | No | Si |
| Editar usuario | No | No | Si |
| Gestionar galería | No | No | Si |
| Editar settings | No | No | Si |
```

## No dicho hasta ahora pero necesario

- No hay enumeración de códigos de error completa. Necesita expansión.
- No hay versionado de refresh token (para logout remoto de todos los dispositivos).
- No hay rate limiting de login attempts.

## Próximos pasos

1. Actualizar 04-permisos-acceso.md con matriz completa
2. Crear 05-modelo-datos-conceptual.md nueva entidad User
3. Actualizar 06-contrato-api-conceptual.md con auth endpoints
4. Actualizar 07-semantica-errores.md con códigos de auth
5. Crear documento de "Security Implementation Checklist"

## Timeline estimado

- Diseño de auth: 1-2 días
- Implementación de endpoints: 3-4 días
- Tests de seguridad: 2-3 días
- Total: 1-2 sprints
