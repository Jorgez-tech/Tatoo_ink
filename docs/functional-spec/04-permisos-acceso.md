# Permisos y Fronteras de Acceso

## Zonas del sistema

- Zona publica: accesible sin autenticacion.
- Zona interna: requiere autenticacion y autorizacion por rol.

## Politica base

- Denegacion por defecto en endpoints internos.
- Minimo privilegio por rol.
- Trazabilidad de cambios relevantes.

## Matriz de permisos (conceptual)

| Accion | Visitante | Artista | Admin |
|---|---|---|---|
| Ver landing publica | Si | Si | Si |
| Enviar contacto | Si | Si | Si |
| Ver listado interno de solicitudes | No | Si | Si |
| Ver detalle interno de solicitud | No | Si | Si |
| Cambiar estado de solicitud | No | Si | Si |
| Asignar solicitud a otro usuario | No | No | Si |
| Gestionar usuarios internos | No | No | Si |

## Fronteras tecnicas esperadas

- Endpoints publicos separados de internos.
- Validacion de sesion en todas las rutas internas.
- Verificacion de rol previa a cada operacion sensible.
