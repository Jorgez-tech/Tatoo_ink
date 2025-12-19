# QA y Testing - Ink Studio

Este documento describe cómo validar la calidad del proyecto (frontend + backend) y el estado actual de las pruebas.

## Estado actual

- **Backend:** suite de pruebas activa con **55 tests** pasando.
- **Frontend:** validación principal vía TypeScript + build de Vite + lint.

## Backend

### Ejecutar pruebas

Desde la raíz del repo:

```bash
dotnet test backend.Tests/backend.Tests.csproj
```

Con más detalle:

```bash
dotnet test backend.Tests/backend.Tests.csproj --verbosity normal
```

Cobertura (si está habilitada en el entorno):

```bash
dotnet test backend.Tests/backend.Tests.csproj --collect:"XPlat Code Coverage"
```

### Tipos de pruebas

- Unitarias y de propiedades (validación, sanitización, resiliencia del servicio de email, persistencia).
- Integración (E2E) mediante `WebApplicationFactory`.

### Advertencias conocidas

Al ejecutar `dotnet test` pueden aparecer warnings (por ejemplo, sobre `#nullable` o InlineData duplicado). No impiden que la suite pase, pero se recomienda corregirlos como mantenimiento.

## Frontend

### Build (TypeScript + Vite)

```bash
npm run build
```

### Lint

```bash
npm run lint
```

### Smoke test manual

- Navegar la landing en mobile/desktop.
- Probar formulario de contacto (estados: loading/success/error).
- Probar lightbox de galería con teclado.

## Checklist de liberación

Antes de publicar:

- `npm run build` (sin errores)
- `npm run lint` (sin errores)
- `dotnet build backend/backend.csproj` (sin errores)
- `dotnet test backend.Tests/backend.Tests.csproj` (55/55 ok)
- Validar variables de entorno y configuración de producción (email, CORS)

Ver checklist detallado en `CHECKPOINT-FINAL.md`.
