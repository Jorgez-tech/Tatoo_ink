# Arquitectura - Ink Studio

Este documento describe la arquitectura actual del proyecto Ink Studio (frontend + backend), sus principales decisiones de diseño y cómo fluye la información.

## Vision general

Ink Studio es una solución fullstack compuesta por:

- **Frontend:** SPA con React + TypeScript (Vite) orientada a performance, accesibilidad y reutilización como template.
- **Backend:** API REST con ASP.NET Core (.NET 8) con persistencia SQLite, validación robusta y controles de seguridad (rate limiting, límite de payload, sanitización).

## Objetivos de diseño

- **Simplicidad:** evitar complejidad accidental y dependencias innecesarias.
- **Reutilización:** separar datos de presentación mediante configuración centralizada.
- **Type-safety:** TypeScript estricto en frontend; DTOs explícitos en backend.
- **Operación:** logging estructurado y errores consistentes.
- **Seguridad básica por defecto:** validación + sanitización + límites de uso.

## Estructura de alto nivel

```
.
|-- src/                 # Frontend (React)
|-- public/              # Assets estáticos
|-- backend/             # Backend (ASP.NET Core Web API)
|-- backend.Tests/       # Suite de pruebas del backend
|-- docs/                # Documentación
```

## Frontend

### Organización

El frontend está organizado por responsabilidades:

```
src/
|-- components/
|   |-- layout/          # Navbar, Footer
|   |-- sections/        # Secciones principales (Hero, Services, Gallery, About, Contact)
|   |-- ui/              # UI base (conjunto reducido)
|   +-- shared/          # Reservado
|-- config/              # Fuente de verdad del contenido y datos
|-- hooks/               # Hooks (scroll spy, responsive)
|-- lib/                 # Utilidades
|-- styles/              # Estilos globales
|-- types/               # Tipos TS compartidos
|-- App.tsx
+-- main.tsx
```

### Configuración centralizada

Regla: el contenido y la identidad del negocio se ajusta en `src/config/` (por ejemplo, datos del negocio, textos, navegación, servicios e imágenes). Esto permite reutilizar el template sin refactorizar componentes.

### Integración con backend

El frontend consume la API usando una URL base configurada por variable de entorno:

- `VITE_API_BASE_URL`

Detalles y ejemplos: ver `BACKEND-INTEGRATION.md` y `API-REST.md`.

## Backend

### Capas

Arquitectura por capas:

```
Controllers (HTTP)
  -> Services (lógica de negocio)
    -> Data (EF Core)
      -> SQLite
```

Responsabilidades:

- **Controllers:** exponen endpoints, devuelven códigos HTTP y DTOs.
- **Services:** validan reglas de negocio y orquestan persistencia + envío de email.
- **Data:** `DbContext` y migraciones.
- **Middleware:** manejo global de errores.

### Flujos clave

#### Envío de contacto (POST /api/contact)

1. Frontend envía JSON a `POST /api/contact`.
2. Backend valida input (FluentValidation) y aplica controles de seguridad.
3. Se persiste el mensaje en SQLite.
4. Se intenta enviar email (SendGrid o SMTP, según configuración).
5. Se retorna respuesta de éxito o error con código HTTP apropiado.

#### Lectura de galería (GET /api/gallery)

1. Frontend solicita imágenes al endpoint.
2. Backend consulta `GalleryImages` en SQLite y retorna la lista.

## Observabilidad

- Logging con Serilog (consola + rolling file).
- Manejo global de excepciones para respuestas consistentes.

Ver `SECURITY.md` para los controles de seguridad y `QA.md` para el estado de pruebas.
