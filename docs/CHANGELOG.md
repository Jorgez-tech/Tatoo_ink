# Changelog - Ink Studio

Registro de cambios y progreso del proyecto Ink Studio (frontend + backend).

Formato basado en [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

---

## [Unreleased] - Trabajo en Progreso

### En Progreso

- Consolidación de documentación (reducción de 19 a 8 archivos core)
- Estandarización de READMEs (raíz, backend, frontend)

---

## [0.95.0] - 2025-01-09 - Release Candidate

### Estado General

- **Progreso:** 95% completado
- **Backend:** Producción ready
- **Frontend:** Producción ready
- **Documentación:** 100% completa (pre-consolidación)
- **Tests:** 55/55 pasando (100%)

### Added

- **Backend:**
  - Endpoint `POST /api/contact` completamente funcional
  - Endpoint `GET /api/gallery` retornando imágenes desde BD
  - Base de datos SQLite con DbInitializer para seed automático
  - 55 tests (unitarios, property-based, integración) - 100% pasando
  - Rate limiting (10 req/min por IP)
  - Sanitización de entrada HTML
  - Logging estructurado con Serilog
  - Health checks en `/health`
  - Dual email service (SendGrid + SMTP)
  - Validación con FluentValidation
  - Middleware de manejo global de excepciones
  - Colección de Postman con 6 requests

- **Frontend:**
  - Gallery dinámica consumiendo API (`/api/gallery`)
  - Formulario de contacto end-to-end funcional
  - Imágenes locales optimizadas (WebP + JPG fallback)
  - Lazy loading con placeholders
  - Lightbox interactivo con navegación por teclado
  - Scroll spy en Navbar
  - Smooth scroll global
  - Responsive design mobile-first
  - Bundle optimizado: 75.14 KB gzipped
  - SEO completo (Open Graph, Twitter Cards)
  - PWA manifest básico

- **Documentación:**
  - 19 documentos técnicos completos
  - 3 READMEs específicos (raíz, backend, frontend)
  - JSDoc completo en 7 componentes principales
  - Guías de deployment, customization, performance, accessibility

### Changed

- Backend: DbInitializer actualizado para UPDATE de registros existentes
- Frontend: Gallery usa URLs locales (`/images/gallery/...`)
- Imágenes: Migradas de Unsplash a assets locales (reducción de 66% en peso)
- Configuración: CORS actualizado para incluir puertos de desarrollo

### Fixed

- Gallery: Navegación siguiente/anterior en lightbox funcionando
- Contact: Persistencia en BD antes de enviar email (no se pierde data)
- CORS: Configurado correctamente para desarrollo y producción
- Rate limiting: Funcionando sin bloquear tests

### Performance

- **Lighthouse Audit:**
  - Performance: 100
  - Accessibility: 88
  - Best Practices: 92
  - SEO: 100
- **Bundle size:** 75.14 KB gzipped (optimizado)
- **Imágenes:** WebP con fallback JPG (65% reducción de peso)
- **LCP:** Mejorado con eager loading y fetchPriority

---

## [0.75.0] - 2025-12-19 - Fase 3 Completada

### Estado

- **Fase 3 (Documentación):** 100% completada
- JSDoc en todos los componentes principales (7/7)
- Documentación técnica exhaustiva

### Added

- **JSDoc completo en componentes:**
  - Navbar.tsx
  - Footer.tsx
  - Hero.tsx
  - Services.tsx
  - Gallery.tsx
  - About.tsx
  - Contact.tsx

- **Documentación técnica:**
  - ARCHITECTURE.md - Arquitectura completa
  - API-REST.md - Especificación de endpoints
  - SECURITY.md - Controles de seguridad
  - QA.md - Testing y calidad
  - DEPLOYMENT.md - Guías de despliegue
  - CUSTOMIZATION.md - Personalización
  - PERFORMANCE.md - Optimizaciones
  - ACCESSIBILITY.md - WCAG AA
  - STRUCTURE.md - Estructura detallada
  - BACKEND-QUICKSTART.md - Inicio rápido backend
  - BACKEND-INTEGRATION.md - Integración frontend-backend
  - CHECKPOINT-FINAL.md - Checklist de release
  - GITHUB-INSTRUCTIONS.md - Workflow de Git
  - GUIDELINES.md - Lineamientos del proyecto
  - IMAGE-OPTIMIZATION-GUIDE.md - Guía de optimización

---

## [0.55.0] - 2025-12-18 - Fase 2 Completada

### Estado

- **Fase 2 (Optimización):** 100% completada
- Performance mejorado significativamente
- Responsive optimizado en todos los componentes

### Added

- Scroll spy en Navbar con detección activa
- Lazy loading de imágenes con placeholders animados
- Lightbox de Gallery con navegación por teclado (flechas, ESC)
- Smooth scroll global
- Mejoras de performance (LCP, CLS, eager loading, fetchPriority)
- SEO básico (meta tags OG/Twitter/description)
- PWA manifest mínimo

### Changed

- Navbar: Efecto backdrop blur en scroll
- Gallery: Navegación circular (primera ? última)
- Hero: fetchPriority="high" en imagen principal
- About: Lazy loading optimizado

### Performance

- LCP reducido con eager loading en Hero
- CLS mejorado con dimensiones explícitas en imágenes
- Bundle optimizado con tree-shaking

---

## [0.35.0] - 2025-12-02 - Fase 1 Completada

### Estado

- **Fase 1 (Auditoría y limpieza):** 100% completada
- Estructura reorganizada y optimizada

### Added

- Estructura de carpetas reorganizada:
  - `components/layout/` - Navbar, Footer
  - `components/sections/` - Hero, Services, Gallery, About, Contact
  - `components/ui/` - 6 componentes activos
  - `config/` - Configuración centralizada (6 archivos)
  - `hooks/` - Custom hooks (2)
  - `lib/` - Utilidades
  - `types/` - Tipos TypeScript

### Removed

- **40 componentes UI no utilizados** de shadcn/ui
- Dependencias innecesarias
- Código duplicado

### Changed

- Configuración centralizada en `config/`
- Importaciones con path alias `@/`
- Tipos unificados en `types/index.ts`

---

## [0.20.0] - 2025-11-09 - Fase 0 Completada

### Estado

- **Fase 0 (Configuración):** 100% completada
- Proyecto base funcional

### Added

- **Backend ASP.NET Core .NET 8:**
  - API REST básica
  - Entity Framework Core + SQLite
  - Validación con FluentValidation
  - Email service (SendGrid + SMTP)
  - Rate limiting
  - Logging con Serilog

- **Frontend React + TypeScript:**
  - Vite 7 como build tool
  - Tailwind CSS 3.4 para estilos
  - shadcn/ui components (46 iniciales)
  - React Hook Form para formularios
  - Lucide React para iconos

- **Landing Page:**
  - Hero section
  - Services section
  - Gallery section
  - About section
  - Contact form
  - Footer

- **Configuración:**
  - ESLint configurado
  - TypeScript strict mode
  - Git conventional commits en español
  - Estructura base de documentación

---

## Convenciones de Commits

El proyecto sigue [Conventional Commits](https://www.conventionalcommits.org/) en español:

### Tipos

- `feat` - Nueva funcionalidad
- `fix` - Corrección de bug
- `refactor` - Refactorización sin cambio funcional
- `style` - Cambios de estilos visuales
- `docs` - Documentación
- `chore` - Mantenimiento
- `perf` - Performance
- `test` - Tests

### Ejemplos

```bash
feat(navbar): añade detección de scroll
fix(hero): corrige fetchPriority en imagen
docs: actualiza STATUS con progreso de Fase 2
refactor(gallery): mejora navegación del lightbox
perf(img): implementa lazy loading
chore: elimina componentes UI no utilizados
```

---

## Stack Tecnológico

### Frontend

- React 18.0
- TypeScript 5.9.3
- Vite 7.1.12
- Tailwind CSS 3.4.17
- shadcn/ui (6 componentes activos)
- Lucide React (iconos)
- React Hook Form (formularios)

### Backend

- ASP.NET Core .NET 8.0
- Entity Framework Core 8.0
- SQLite
- FluentValidation
- Serilog
- SendGrid / SMTP
- AspNetCoreRateLimit
- HtmlSanitizer (Ganss.Xss)

### Testing

- xUnit (backend)
- FsCheck (property-based testing)
- WebApplicationFactory (integration tests)

### Tools

- Git (control de versiones)
- Postman (testing API)
- Squoosh / Sharp (optimización de imágenes)
- Lighthouse (performance audit)

---

## Métricas de Calidad

### Tests

- **Total:** 55 tests
- **Unitarios:** 49
- **Integración:** 6
- **Estado:** 100% pasando

### Performance (Lighthouse)

- **Performance:** 100
- **Accessibility:** 88
- **Best Practices:** 92
- **SEO:** 100

### Bundle

- **Size:** 75.14 KB gzipped
- **Optimización:** Tree-shaking, code splitting
- **Imágenes:** WebP + JPG fallback (65% reducción)

### Code Quality

- **TypeScript:** Strict mode, 0 errores
- **ESLint:** 0 errores (103 warnings en .md - formateo)
- **Documentación:** 100% de componentes con JSDoc

---

## Roadmap Futuro

### Corto Plazo

- [ ] Consolidar documentación (19 ? 8 archivos)
- [ ] Performance audit completo
- [ ] Pruebas de integración frontend-backend
- [ ] Limpieza de dependencias no utilizadas
- [ ] Accesibilidad audit (WCAG AA)

### Mediano Plazo

- [ ] Dashboard admin para mensajes de contacto
- [ ] CRUD de galería desde admin
- [ ] CI/CD pipeline (GitHub Actions)
- [ ] Docker containerization
- [ ] Deploy en staging

### Largo Plazo

- [ ] Sistema de autenticación
- [ ] Multi-tenant para múltiples clientes
- [ ] CMS headless (Strapi/Contentful)
- [ ] Internacionalización (i18n)
- [ ] Analytics avanzado

---

## Contribuidores

- **Jorge** - Desarrollador principal
- **GitHub Copilot** - Asistente de desarrollo

---

## Licencia

Proyecto privado - Ink Studio

---

**Última actualización:** 2025-01-09  
**Próxima revisión:** Al completar consolidación de documentación
