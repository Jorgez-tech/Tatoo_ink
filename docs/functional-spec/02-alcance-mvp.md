# Alcance MVP

## Incluido en MVP

- Landing publica con secciones de negocio (servicios, galeria, contacto, identidad).
- Formulario de contacto con validaciones basicas y registro persistente.
- Panel interno para visualizar solicitudes y actualizar su estado.
- Gestion de galeria en panel interno (crear, editar metadata, activar/desactivar).
- Gestion de perfil de negocio en panel interno (telefono, horarios, direccion, redes, textos base).
- Roles basicos para acceso interno (admin y artista).
- Autenticacion basica con login y sesion/token para zona interna.
- API versionada para operaciones principales (contactos, galeria, perfil y auth).

## Fuera de alcance MVP

- Agenda completa con calendario avanzado y recordatorios automativos.
- Pagos en linea.
- Programa de fidelizacion.
- Multi-sucursal o multi-negocio.
- Sistema complejo de analytics propio.
- RBAC avanzado con jerarquias complejas y permisos granulares por campo.

## Supuestos

- Un solo negocio y una sola identidad de marca.
- Volumen de contactos inicial moderado.
- Operacion interna por equipo pequeno.

## Dependencias funcionales

- Definicion clara de estados de solicitud.
- Politica de acceso por rol.
- Campos minimos obligatorios para convertir interes en oportunidad.
- Seed inicial de usuario administrador para primer acceso seguro.
