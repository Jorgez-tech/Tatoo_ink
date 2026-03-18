# Modelo de Datos Conceptual

## Entidades principales

## ContactRequest

- id (int)
- name
- email
- phone
- message
- wantsAppointment
- status
- createdAt
- emailSent
- emailSentAt (optional)

## GalleryImage

- id (int)
- src
- fallback (optional)
- alt
- category (optional)
- photographer (optional)
- isActive
- displayOrder
- createdAt

## User

- id (int)
- name
- email
- passwordHash
- role (admin, artist)
- isActive
- createdAt

## BusinessProfile

- id (int)
- businessName
- tagline
- description
- address
- phone
- email
- schedule
- instagramUrl (optional)
- facebookUrl (optional)
- updatedAt

## Relaciones

- User 1..N ContactRequest (asignacion opcional)
- GalleryImage independiente para render de portafolio publico
- BusinessProfile como fuente editable para informacion publica del negocio

## Estados de ContactRequest

- new: recien recibido.
- inReview: en revision interna.
- contacted: cliente ya contactado.
- scheduled: cita o sesion acordada.
- closed: gestion finalizada.
- rejected: no viable o descartado.

## Transiciones validas de estado

- new -> inReview | rejected
- inReview -> contacted | rejected
- contacted -> scheduled | rejected
- scheduled -> closed | rejected
- closed -> (sin transiciones)
- rejected -> (sin transiciones)

## Reglas de integridad

- Email obligatorio y con formato valido.
- Mensaje con largo minimo.
- Cambios de estado trazables.

## Compatibilidad con estado actual del proyecto

- Los IDs se mantienen en `int` para evitar ruptura de backend y frontend existentes.
- `ContactNote` se difiere a una fase posterior (opcional) y no bloquea MVP.
- `GalleryImage` y `BusinessProfile` son entidades operativas del MVP.
