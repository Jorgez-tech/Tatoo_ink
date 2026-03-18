# Actores y Casos de Uso

## Actores

- Visitante: persona que navega contenido publico.
- Cliente potencial: visitante que envia solicitud por formulario.
- Artista: usuario interno que revisa y atiende solicitudes asignadas.
- Admin: usuario interno con control completo de operacion.

## Casos de uso publicos

1. Navegar contenido del negocio.
2. Revisar servicios y trabajos de referencia.
3. Enviar solicitud de contacto.

## Casos de uso internos

1. Iniciar sesion en zona interna.
2. Listar solicitudes por estado.
3. Ver detalle de una solicitud.
4. Cambiar estado de solicitud.
5. Asignar responsable (admin).
6. Crear imagen de galeria (admin, artista).
7. Editar metadata de imagen (admin, artista).
8. Activar o desactivar imagen en galeria (admin, artista).
9. Reordenar imagenes destacadas (admin).
10. Editar perfil del negocio (admin).
11. Crear usuario interno inicial (bootstrap admin).

## Flujo principal de conversion

1. Visitante explora la pagina.
2. Visitante completa formulario.
3. Sistema valida y registra la solicitud.
4. Equipo interno revisa la solicitud.
5. Equipo avanza estado segun contacto real con cliente.

## Flujo principal de mantenimiento de portafolio

1. Usuario interno inicia sesion.
2. Carga una nueva imagen de trabajo terminado.
3. Completa metadata minima (alt, categoria, estado activo).
4. Publica y valida visualizacion en landing.
