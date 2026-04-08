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

## Requisitos minimos de autenticacion

- Debe existir endpoint de login para zona interna.
- Debe existir mecanismo de bootstrap para crear el primer admin.
- Las credenciales no se almacenan en texto plano.

## Matriz operativa extendida

| Accion | Visitante | Artista | Admin |
|---|---|---|---|
| Crear imagen de galeria | No | Si | Si |
| Editar imagen de galeria | No | Si | Si |
| Activar/desactivar imagen de galeria | No | Si | Si |
| Reordenar galeria | No | No | Si |
| Editar perfil del negocio | No | No | Si |
| Crear primer usuario admin (bootstrap) | No | No | Si |
