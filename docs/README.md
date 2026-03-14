# Documentation - Ink Studio

Documentación técnica y operativa del proyecto Ink Studio (landing page fullstack para estudio de tatuajes).

Esta documentación está diseñada para que cualquier desarrollador pueda:

- Configurar y ejecutar el proyecto (frontend + backend)
- Entender la arquitectura y decisiones de diseño
- Consumir la API REST
- Personalizar el template para nuevos clientes
- Desplegar a producción
- Mantener y extender el código

---

## Inicio Rápido

### Para Desarrolladores Nuevos

1. **[Getting Started](GETTING-STARTED.md)** - Setup inicial, instalación y ejecución
2. **[Architecture](ARCHITECTURE.md)** - Visión general de la arquitectura
3. **[Development Guide](DEVELOPMENT-GUIDE.md)** - Convenciones, testing y workflow

### Para Despliegue

1. **[Deployment](DEPLOYMENT.md)** - Guías de despliegue (Vercel, Azure, AWS)
2. **[API Reference](API-REFERENCE.md)** - Especificación completa de endpoints

### Para Personalización

1. **[Customization](CUSTOMIZATION.md)** - Adaptar el template para nuevos clientes

---

## Índice de Documentación

### Core (Documentos principales)

| Documento | Descripción | Audiencia |
|-----------|-------------|-----------|
| [**GETTING-STARTED.md**](GETTING-STARTED.md) | Setup inicial, instalación, configuración | Desarrolladores nuevos |
| [**ARCHITECTURE.md**](ARCHITECTURE.md) | Arquitectura completa (frontend + backend + integración) | Todos |
| [**API-REFERENCE.md**](API-REFERENCE.md) | Especificación de endpoints REST | Backend, Frontend |
| [**DEVELOPMENT-GUIDE.md**](DEVELOPMENT-GUIDE.md) | Convenciones, testing, QA, workflow | Desarrolladores |
| [**DEPLOYMENT.md**](DEPLOYMENT.md) | Guías de despliegue (Vercel, Azure, Railway) | DevOps, Desarrolladores |
| [**CUSTOMIZATION.md**](CUSTOMIZATION.md) | Personalización del template | Clientes, Desarrolladores |
| [**CHANGELOG.md**](CHANGELOG.md) | Historial de cambios y releases | Todos |

### Documentación Especializada

| Documento | Descripción |
|-----------|-------------|
| [**SECURITY.md**](SECURITY.md) | Controles de seguridad, rate limiting, sanitización |
| [**PERFORMANCE.md**](PERFORMANCE.md) | Optimizaciones, métricas, Lighthouse |
| [**ACCESSIBILITY.md**](ACCESSIBILITY.md) | Cumplimiento WCAG AA, accesibilidad |
| [**ATTRIBUTIONS.md**](ATTRIBUTIONS.md) | Créditos, licencias, atribuciones |

### Especificacion Funcional

- [**functional-spec/00-README.md**](functional-spec/00-README.md) - Paquete conceptual del producto unico (vision, alcance, casos de uso, permisos, datos, API y roadmap)

### READMEs Específicos

- **[README.md](../README.md)** (raíz) - Visión general del proyecto
- **[backend/README.md](../backend/README.md)** - Backend ASP.NET Core
- **[src/README.md](../src/README.md)** - Frontend React + TypeScript

---

## Guía de Lectura por Rol

### Desarrollador Nuevo

**Primera vez en el proyecto:**

1. [README.md](../README.md) - Visión general
2. [GETTING-STARTED.md](GETTING-STARTED.md) - Setup local
3. [ARCHITECTURE.md](ARCHITECTURE.md) - Arquitectura
4. [DEVELOPMENT-GUIDE.md](DEVELOPMENT-GUIDE.md) - Convenciones

**Desarrollo diario:**

1. [DEVELOPMENT-GUIDE.md](DEVELOPMENT-GUIDE.md) - Testing, commits, workflow
2. [API-REFERENCE.md](API-REFERENCE.md) - Referencia de endpoints
3. [CHANGELOG.md](CHANGELOG.md) - Últimos cambios

### DevOps / Deployment

**Preparación de deployment:**

1. [DEPLOYMENT.md](DEPLOYMENT.md) - Guías de despliegue
2. [SECURITY.md](SECURITY.md) - Controles de seguridad
3. [ARCHITECTURE.md](ARCHITECTURE.md) - Arquitectura para infraestructura
4. [backend/README.md](../backend/README.md) - Configuración de appsettings

### Cliente / Personalización

**Adaptar para nuevo cliente:**

1. [CUSTOMIZATION.md](CUSTOMIZATION.md) - Personalización completa
2. [GETTING-STARTED.md](GETTING-STARTED.md) - Setup local
3. [ARCHITECTURE.md](ARCHITECTURE.md) - Entender estructura

### QA / Testing

**Validación de calidad:**

1. [DEVELOPMENT-GUIDE.md](DEVELOPMENT-GUIDE.md) - Testing y QA
2. [PERFORMANCE.md](PERFORMANCE.md) - Métricas de performance
3. [ACCESSIBILITY.md](ACCESSIBILITY.md) - Cumplimiento WCAG

---

## Estructura del Proyecto

```
tatoo_ink/
├── backend/                    # Backend ASP.NET Core .NET 8
│   ├── Controllers/           # Endpoints REST API
│   ├── Services/              # Lógica de negocio
│   ├── Models/                # Entidades y DTOs
│   ├── Data/                  # DbContext y migraciones
│   └── README.md              # Documentación backend
├── backend.Tests/             # Tests (55 total)
├── src/                       # Frontend React + TypeScript
│   ├── components/            # Componentes React
│   ├── config/                # Configuración centralizada
│   ├── hooks/                 # Custom hooks
│   └── README.md              # Documentación frontend
├── docs/                      # Esta documentación
│   ├── GETTING-STARTED.md
│   ├── ARCHITECTURE.md
│   ├── API-REFERENCE.md
│   ├── DEVELOPMENT-GUIDE.md
│   ├── DEPLOYMENT.md
│   ├── CUSTOMIZATION.md
│   ├── CHANGELOG.md
│   └── archive/               # Documentos consolidados/obsoletos
└── README.md                  # Visión general del proyecto
```

---

## Stack Tecnológico

### Frontend

- **Framework:** React 18.0
- **Language:** TypeScript 5.9.3
- **Build Tool:** Vite 7.1.12
- **Styling:** Tailwind CSS 3.4.17
- **UI Components:** shadcn/ui (6 componentes activos)
- **Icons:** Lucide React
- **Forms:** React Hook Form

### Backend

- **Framework:** ASP.NET Core .NET 8.0
- **Database:** SQLite + Entity Framework Core
- **Validation:** FluentValidation
- **Email:** SendGrid / SMTP
- **Logging:** Serilog
- **Security:** AspNetCoreRateLimit, HtmlSanitizer

### Testing

- **Backend:** xUnit, FsCheck (property-based)
- **Integration:** WebApplicationFactory
- **Total Tests:** 55 (100% pasando)

---

## Estado del Proyecto

**Versión actual:** 0.95.0 - Release Candidate  
**Progreso:** 95% completado  
**Estado:** Producción ready (pendiente configuración final)

**Completado:**

- ✅ Backend API funcional (2 endpoints)
- ✅ Frontend optimizado (75KB gzipped)
- ✅ 55 tests pasando (100%)
- ✅ Gallery dinámica desde BD
- ✅ Formulario de contacto end-to-end
- ✅ Imágenes optimizadas (WebP + fallback)
- ✅ Documentación consolidada
- ✅ Lighthouse: Performance 100, SEO 100

**Pendiente:**

- ⏳ Configuración de producción (5% restante)

Ver [CHANGELOG.md](CHANGELOG.md) para historial completo.

---

## Convenciones del Proyecto

### Commits (Español + Conventional Commits)

```bash
feat(navbar): añade detección de scroll
fix(hero): corrige fetchPriority en imagen
docs: actualiza documentación consolidada
refactor(gallery): mejora navegación del lightbox
perf(img): implementa lazy loading
```

### Estructura de Archivos (Fija)

No modificar estructura sin coordinación:

```
src/
├── components/
│   ├── layout/      # Navbar, Footer
│   ├── sections/    # Hero, Services, Gallery, About, Contact
│   └── ui/          # Solo 6 componentes activos
├── config/          # Configuración centralizada
├── hooks/           # Custom hooks
└── types/           # Tipos TypeScript
```

### Configuración Centralizada

No hardcodear datos del negocio en componentes:

```typescript
// ❌ INCORRECTO
<h1>Ink Studio</h1>

// ✅ CORRECTO
import { businessInfo } from "@/config/business-info";
<h1>{businessInfo.name}</h1>
```

---

## Recursos Adicionales

### Documentación Externa

- [React 19 Docs](https://react.dev/)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)
- [Tailwind CSS v3](https://tailwindcss.com/docs)
- [Vite Guide](https://vite.dev/guide/)
- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)

### Herramientas

- **Squoosh:** https://squoosh.app/ (optimización de imágenes)
- **Lighthouse:** Chrome DevTools (performance audit)
- **axe DevTools:** https://www.deque.com/axe/ (accesibilidad)
- **Postman:** Colección en `backend/Postman/`

---

## Archivo de Documentación

Los siguientes documentos han sido consolidados y archivados en `docs/archive/`:

- `BACKEND-QUICKSTART.md` → Consolidado en `GETTING-STARTED.md`
- `BACKEND-INTEGRATION.md` → Consolidado en `GETTING-STARTED.md` y `ARCHITECTURE.md`
- `QA.md` → Consolidado en `DEVELOPMENT-GUIDE.md`
- `GUIDELINES.md` → Consolidado en `DEVELOPMENT-GUIDE.md`
- `GITHUB-INSTRUCTIONS.md` → Consolidado en `DEVELOPMENT-GUIDE.md`
- `IMAGE-OPTIMIZATION-GUIDE.md` → Consolidado en `DEVELOPMENT-GUIDE.md`
- `STRUCTURE.md` → Consolidado en `ARCHITECTURE.md`
- `NEXT-STEPS.md` → Convertido a `CHANGELOG.md`
- `CHECKPOINT-FINAL.md` → Consolidado en `CHANGELOG.md`
- `DEVELOPMENT-RULES.md` → Consolidado en `DEVELOPMENT-GUIDE.md`

Estos archivos siguen disponibles en `docs/archive/` para referencia histórica.

---

## Principios de Mantenimiento

1. **Evitar duplicación:** Si una regla existe en un documento técnico, los demás deben enlazarla
2. **Ejemplos reproducibles:** Incluir comandos y ejemplos ejecutables
3. **Tono profesional:** Mantener consistencia en estilo y formato
4. **Actualización continua:** Sincronizar documentación con código
5. **Navegación clara:** Estructura predecible y enlaces cruzados

---

## Soporte

- **Issues del proyecto:** [GitHub Issues](https://github.com/Jorgez-tech/Tatoo_ink/issues)
- **Documentación técnica:** [docs/](.)
- **READMEs específicos:** [backend/](../backend/README.md), [src/](../src/README.md)

---

**Última actualización:** 2025-01-09  
**Próxima revisión:** Al completar configuración de producción
