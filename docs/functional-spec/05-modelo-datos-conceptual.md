# Modelo de Datos Conceptual

## Entidades principales

## ContactRequest

- id
- nombre
- email
- telefono (opcional)
- mensaje
- servicioInteres (opcional)
- estado
- fuente
- createdAt
- updatedAt
- assignedToUserId (opcional)

## User

- id
- nombre
- email
- role (admin, artista)
- isActive
- createdAt

## ContactNote

- id
- contactRequestId
- authorUserId
- contenido
- createdAt

## Relaciones

- User 1..N ContactRequest (asignacion opcional)
- ContactRequest 1..N ContactNote
- User 1..N ContactNote

## Estados de ContactRequest

- new: recien recibido.
- in_review: en revision interna.
- contacted: cliente ya contactado.
- scheduled: cita o sesion acordada.
- closed: gestion finalizada.
- rejected: no viable o descartado.

## Reglas de integridad

- Email obligatorio y con formato valido.
- Mensaje con largo minimo.
- Historial de notas no editable retroactivamente (solo agregar).
- Cambios de estado trazables.
