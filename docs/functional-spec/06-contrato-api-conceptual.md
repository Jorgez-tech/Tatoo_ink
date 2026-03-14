# Contrato API Conceptual

## Convenciones

- Prefijo de version: `/api/v1`
- JSON para requests y responses
- Fechas en formato ISO-8601 UTC

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
  "id": "uuid",
  "status": "new",
  "createdAt": "2026-03-14T12:00:00Z"
}
```

## Endpoints internos (autenticados)

### GET /api/v1/internal/contact-requests

Uso: listar solicitudes con filtros por estado y fecha.

### GET /api/v1/internal/contact-requests/{id}

Uso: ver detalle de una solicitud.

### PATCH /api/v1/internal/contact-requests/{id}/status

Uso: actualizar estado de solicitud.

### POST /api/v1/internal/contact-requests/{id}/notes

Uso: agregar nota interna.

## Requisitos no funcionales del contrato

- Idempotencia en operaciones donde aplique.
- Estructura consistente de error.
- Tiempos de respuesta razonables para uso operativo diario.
