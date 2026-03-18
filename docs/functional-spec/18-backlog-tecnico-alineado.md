# Backlog tecnico alineado (Backend + Infra + Frontend Admin)

Fecha: 2026-03-17
Estado: Propuesto para implementacion
Alcance: Sin cambio de stack (Frontend TypeScript SPA existente + Backend C# API existente)

## 1) Tareas de Backend (C#)

| ID | Descripcion corta | Dependencias |
|---|---|---|
| BE-01 | Crear o ajustar entidad User (email unico, passwordHash, role, isActive, timestamps). | - |
| BE-02 | Implementar seed de primer admin (bootstrap controlado). | BE-01 |
| BE-03 | Implementar POST /api/v1/auth/login con emision de token/sesion. | BE-01, BE-02 |
| BE-04 | Implementar refresh/logout para sesion interna. | BE-03 |
| BE-05 | Proteger endpoints internos con autorizacion por rol (deny by default). | BE-03 |
| BE-06 | Ajustar modelo GalleryImage para incluir descripcion editable del trabajo. | - |
| BE-07 | Implementar alta interna de trabajo (imagen + metadata + descripcion). | BE-05, BE-06 |
| BE-08 | Implementar edicion interna de trabajo (imagen/metadata/descripcion). | BE-05, BE-06 |
| BE-09 | Implementar eliminacion interna de trabajo. | BE-05, BE-06 |
| BE-10 | Implementar listado interno de trabajos para administracion. | BE-05, BE-06 |
| BE-11 | Mantener endpoint publico de galeria con solo trabajos activos/publicables. | BE-06 |
| BE-12 | Implementar listado interno de mensajes de contacto (opcional, solo revision). | BE-05 |
| BE-13 | Implementar detalle interno de mensaje de contacto (opcional). | BE-05, BE-12 |
| BE-14 | Unificar validaciones y semantica de errores (401, 403, 404, 422). | BE-03, BE-07, BE-08, BE-09, BE-12 |
| BE-15 | Agregar pruebas unitarias/integracion para auth, roles y CRUD de galeria. | BE-03 a BE-10, BE-14 |

## 2) Tareas de Infraestructura

| ID | Descripcion corta | Dependencias |
|---|---|---|
| INF-01 | Configurar base de datos de produccion para persistir trabajos y mensajes. | - |
| INF-02 | Ejecutar migraciones en pipeline de deploy. | INF-01 |
| INF-03 | Configurar almacenamiento persistente de imagenes de trabajos. | - |
| INF-04 | Definir limites de subida (peso maximo, tipos MIME permitidos, naming). | INF-03 |
| INF-05 | Gestionar secretos fuera de codigo (token keys, DB, correo). | - |
| INF-06 | Habilitar HTTPS obligatorio en produccion. | - |
| INF-07 | Configurar CORS restringido al frontend autorizado. | INF-06 |
| INF-08 | Definir respaldo basico de BD e imagenes con politica de retencion. | INF-01, INF-03 |
| INF-09 | Habilitar logging de accesos y acciones admin (crear/editar/eliminar trabajos). | INF-05 |
| INF-10 | Definir checklist post-deploy (login admin, CRUD galeria, lectura de contactos). | INF-02, INF-03, INF-05, INF-07 |

## 3) Tareas de Frontend - Panel Admin (TypeScript)

Nota funcional: las vistas deben usar la misma base de codigo, estilos y componentes existentes de la SPA actual.

| ID | Descripcion corta | Dependencias |
|---|---|---|
| FE-01 | Crear rutas internas del panel admin dentro de la SPA existente. | - |
| FE-02 | Implementar vista Login de admin/artista con componentes actuales. | FE-01, BE-03 |
| FE-03 | Implementar gestion de sesion/token en cliente + logout. | FE-02, BE-04 |
| FE-04 | Implementar guard de rutas privadas del panel. | FE-03 |
| FE-05 | Implementar control por rol para acciones de escritura (crear/editar/eliminar). | FE-03, BE-05 |
| FE-06 | Crear vista Listado de trabajos con acciones editar/eliminar. | FE-04, BE-10 |
| FE-07 | Crear vista Nuevo trabajo (form + subida imagen + descripcion + metadata). | FE-04, BE-07 |
| FE-08 | Crear vista Editar trabajo reutilizando formulario de alta. | FE-06, BE-08 |
| FE-09 | Implementar flujo Eliminar trabajo con confirmacion y refresco. | FE-06, BE-09 |
| FE-10 | Integrar cliente API tipado para endpoints internos de auth y galeria. | FE-06 a FE-09, BE-07 a BE-10 |
| FE-11 | Implementar estados UI de carga/error/exito para login y CRUD. | FE-02 a FE-10 |
| FE-12 | (Opcional) Crear vista Listado de mensajes de contacto para revision. | FE-04, BE-12 |
| FE-13 | (Opcional) Crear vista Detalle de mensaje de contacto. | FE-12, BE-13 |
| FE-14 | Agregar pruebas de flujo para login y CRUD de trabajos en panel. | FE-02 a FE-11 |
