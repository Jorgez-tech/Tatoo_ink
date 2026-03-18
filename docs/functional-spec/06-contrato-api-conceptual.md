# Contrato API Conceptual

## Convenciones

- Prefijo de version: `/api/v1`
- JSON para requests y responses
- Fechas en formato ISO-8601 UTC
- Nombres de propiedades JSON en `camelCase`
- IDs en formato `int`
- En C#, DTOs en `PascalCase` con serializacion JSON a `camelCase`
- Claves y contratos tecnicos en ingles; mensajes de negocio en espanol

## Endpoints publicos

### POST /api/v1/contact-requests

Uso: crear solicitud desde landing publica.

Request (conceptual):

```json
{
  "name": "Nombre",
  "email": "correo@dominio.com",
  "phone": "+56...",
  "message": "Quiero cotizar...",
  "serviceInterest": "Blackwork"
}
```

Response exitosa (201):

```json
{
  "id": 123,
  "status": "new",
  "createdAt": "2026-03-14T12:00:00Z"
}
```

## Endpoints de autenticacion (interno)

### POST /api/v1/auth/login

Uso: iniciar sesion en panel interno.

### POST /api/v1/auth/bootstrap-admin

Uso: crear primer usuario admin en entorno controlado.

## Endpoints internos (autenticados)

### GET /api/v1/internal/contact-requests

Uso: listar solicitudes con filtros por estado y fecha.

### GET /api/v1/internal/contact-requests/{id}

Uso: ver detalle de una solicitud.

### PATCH /api/v1/internal/contact-requests/{id}/status

Uso: actualizar estado de solicitud.

Request (conceptual):

```json
{
  "status": "inReview"
}
```

### POST /api/v1/internal/contact-requests/{id}/notes

Uso: agregar nota interna.

## Endpoints de galeria (interno)

### POST /api/v1/internal/gallery-images

Uso: crear nueva imagen de galeria.

### PATCH /api/v1/internal/gallery-images/{id}

Uso: editar metadata de imagen.

### PATCH /api/v1/internal/gallery-images/{id}/status

Uso: activar o desactivar publicacion.

### PATCH /api/v1/internal/gallery-images/reorder

Uso: actualizar orden de visualizacion.

## Endpoints de perfil de negocio (interno)

### GET /api/v1/internal/business-profile

Uso: obtener configuracion editable del negocio.

### PATCH /api/v1/internal/business-profile

Uso: actualizar telefono, horario, direccion, redes y textos base.

## Requisitos no funcionales del contrato

- Idempotencia en operaciones donde aplique.
- Estructura consistente de error.
- Tiempos de respuesta razonables para uso operativo diario.
