# Corrección 1: Admin debe gestionar la Galería

Fecha: 2026-03-17
Estatus: Propuesta de enmienda

## Problema

La especificación 03-actores-casos-uso.md omite completamente la gestión de galería en el panel admin. Esto hace que el cliente dependa del desarrollador para cualquier cambio visual de su portafolio.

## Solución propuesta

### Nuevos casos de uso internos (Admin)

Agregar a la especificación:

```
7. Gestionar galería (NUEVO):
   7.1. Subir nueva foto
   7.2. Editar metadata de foto (Alt, Categoría)
   7.3. Reordenar fotos
   7.4. Borrar foto
7.5. Gestionar información del negocio (NUEVO):
   7.5.1. Editar nombre del negocio
   7.5.2. Editar teléfono
   7.5.3. Editar email
   7.5.4. Editar horarios
   7.5.5. Editar redes sociales
   7.5.6. Editar descripción
```

### Nuevos endpoints necesarios

Agregar a 06-contrato-api-conceptual.md:

```
### Gallery Management (Admin only)

POST /api/v1/internal/gallery
Uso: subir nueva foto a la galería

PATCH /api/v1/internal/gallery/{id}
Uso: editar metadata de foto existente

DELETE /api/v1/internal/gallery/{id}
Uso: eliminar foto

### Business Settings (Admin only)

PATCH /api/v1/internal/business-settings
Uso: actualizar info del negocio (nombre, phone, email, horarios, redes)

GET /api/v1/internal/business-settings
Uso: obtener configuración actual (para inicializar form)
```

### Nuevas entidades en modelo

Agregar a 05-modelo-datos-conceptual.md:

```
## BusinessSettings

- id
- businessName
- businessTagline
- businessDescription
- phoneNumber
- emailAddress
- address
- schedule (JSON con horarios por día)
- instagramUrl
- facebookUrl
- twitterUrl
- updatedAt
- updatedByUserId
```

### Impacto en permisos

Actualizar 04-permisos-acceso.md:

```
| Subir foto a galería | No | No | Si |
| Editar info del negocio | No | No | Si |
```

## Justificación

Un producto que no permite que el cliente gestione su contenido no es independiente. Sin esto, cada cambio requiere intervención del desarrollador.

## Próximos pasos

1. Actualizar 03-actores-casos-uso.md con nuevos casos de uso
2. Actualizar 04-permisos-acceso.md con nuevas líneas
3. Actualizar 05-modelo-datos-conceptual.md con BusinessSettings
4. Actualizar 06-contrato-api-conceptual.md con nuevos endpoints
5. Actualizar 07-semantica-errores.md si hay nuevos códigos de error (ej: FILE_TOO_LARGE)
