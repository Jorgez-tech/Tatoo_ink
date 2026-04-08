# Corrección 4: Datos del negocio desde base de datos (no hardcode)

Fecha: 2026-03-17
Estatus: Propuesta de enmienda

## Problema

Todos los datos del negocio están hardcodeados en `src/config/business-info.ts`. Cambiar un teléfono requiere:
1. Editar TypeScript
2. Recompilar React
3. Redeploy del frontend

Esto no es un producto, es un template.

## Solución propuesta

### 4.1 Nueva entidad: BusinessSettings (BD)

Agregar a 05-modelo-datos-conceptual.md:

```
## BusinessSettings

- id (int, primary key, singleton)
- businessName (string)
- businessTagline (string)
- businessDescription (text)
- phoneNumber (string)
- emailAddress (string)
- address (string)
- instagramUrl (string nullable)
- facebookUrl (string nullable)
- twitterUrl (string nullable)
- schedule (JSON - horarios por día)
- createdAt
- updatedAt
- updatedByUserId (int, FK a User)
```

**Nota:** Singleton - solo existe 1 registro.

### 4.2 Nuevos endpoints

Agregar a 06-contrato-api-conceptual.md:

```
## Business Settings (PUBLIC READ)

### GET /api/v1/business-settings

Uso: obtener info del negocio (público, sin auth)

Response (200 OK):
{
  "id": 1,
  "businessName": "Ink Studio",
  "businessTagline": "Arte en tu Piel",
  "businessDescription": "Transformamos tus ideas...",
  "phoneNumber": "+34 123 456 789",
  "emailAddress": "info@inkstudio.com",
  "address": "Calle Principal 123",
  "instagramUrl": "https://instagram.com/inkstudio",
  "facebookUrl": "https://facebook.com/inkstudio",
  "twitterUrl": "https://twitter.com/inkstudio",
  "schedule": {
    "monday": "10:00-20:00",
    "tuesday": "10:00-20:00",
    "wednesday": "10:00-20:00",
    "thursday": "10:00-20:00",
    "friday": "10:00-20:00",
    "saturday": "10:00-20:00",
    "sunday": "cerrado"
  }
}

## Business Settings (ADMIN ONLY)

### PATCH /api/v1/internal/business-settings

Uso: actualizar la información del negocio

Request:
{
  "businessName": "Ink Studio",
  "phoneNumber": "+34 987 654 321",
  "schedule": {
    "monday": "11:00-21:00",
    "tuesday": "11:00-21:00"
  }
}

Response (200 OK):
{
  "id": 1,
  "businessName": "Ink Studio",
  "phoneNumber": "+34 987 654 321",
  "updatedAt": "2026-03-17T15:30:00Z"
}
```

### 4.3 Frontend: Cambiar de hardcode a API

**Cambio en src/config/business-info.ts:**

```typescript
// ANTES (hardcodeado):
export const businessInfo: BusinessInfo = {
  name: "Ink Studio",
  tagline: "Arte en tu Piel",
  contact: {
    address: "Calle Principal 123, Ciudad",
    phone: "+34 123 456 789",
    email: "info@tattoostudio.com",
    schedule: "Lun - Sáb: 10:00 - 20:00"
  }
};

// DESPUÉS (desde API):
// Este archivo ya NO se usa para datos. Se convierte en valor por defecto/fallback.
```

**Nuevo hook: useBusinessSettings():**

```typescript
// src/hooks/use-business-settings.ts
export function useBusinessSettings() {
  const [settings, setSettings] = useState(defaultBusinessInfo);
  
  useEffect(() => {
    fetch(`${import.meta.env.VITE_API_BASE_URL}/api/v1/business-settings`)
      .then(res => res.json())
      .then(data => setSettings(data))
      .catch(err => console.warn("Using default business info", err));
  }, []);
  
  return settings;
}
```

**Uso en componentes (ejemplo: Hero.tsx):**

```typescript
// ANTES:
import { businessInfo } from "@/config/business-info";
export function Hero() {
  return <h1>{businessInfo.name}</h1>;
}

// DESPUÉS:
export function Hero() {
  const settings = useBusinessSettings();
  return <h1>{settings.businessName}</h1>;
}
```

### 4.4 Admin panel: Editar Business Settings

El admin podrá:
1. Ver formulario con campos: nombre, teléfono, email, redes, horarios, descripción
2. Cambiar valores
3. Hacer PATCH a /api/v1/internal/business-settings
4. Frontend recarga datos automáticamente

### 4.5 Inicialización

En primera ejecución (DbSeeder.cs):

```csharp
// Crear registro singleton de BusinessSettings
var defaultSettings = new BusinessSettings
{
    BusinessName = "Ink Studio",
    BusinessTagline = "Arte en tu Piel",
    // ... resto de datos
};
context.BusinessSettings.Add(defaultSettings);
await context.SaveChangesAsync();
```

## Matriz de cambios en 04-permisos-acceso.md

Actualizar:

```
| Ver horarios del negocio | Si | Si | Si |
| Ver teléfono del negocio | Si | Si | Si |
| Editar info del negocio | No | No | Si |
| Editar horarios del negocio | No | No | Si |
```

## Impacto

### Beneficio principal

El cliente es completamente autosuficiente para:
- Cambiar teléfono
- Actualizar horarios
- Editar descripción
- Agregar redes sociales

**Sin necesidad de developer.**

### Migración código existente

- El archivo `business-info.ts` se mantiene como fallback
- Los componentes que usan `businessInfo` deben migrar a `useBusinessSettings()`
- El cambio es gradual, sin ruptura

## Timeline

- Crear entidad y endpoint: 1 día
- Admin UI: 1-2 días
- Migrar frontend: 1 día
- Tests: 1 día
- **Total: 4-5 días (< 1 sprint)**

## Próximos pasos

1. Agregar BusinessSettings a modelo (05)
2. Crear endpoints CRUD (06)
3. Actualizar permisos (04)
4. Crear lista de componentes a migrar (frontend)
