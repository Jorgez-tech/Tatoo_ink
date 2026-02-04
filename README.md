# Ink Studio - Landing Page Fullstack

Landing page profesional para estudio de tatuajes con backend ASP.NET Core y frontend React + TypeScript.

**Versión:** 0.95.0 - Release Candidate  
**Estado:** Producción Ready (95% completado)  
**Changelog:** [Ver historial completo](docs/CHANGELOG.md)

---

## Descripción General

Solución fullstack completa que permite a los clientes enviar mensajes de contacto y solicitar citas a través de un formulario web. El sistema persiste los datos en base de datos SQLite y envía notificaciones por correo electrónico al estudio.

### Stack Tecnológico

- **Frontend:** React 18.0 + TypeScript 5.9 + Vite 7 + Tailwind CSS 3.4
- **Backend:** ASP.NET Core Web API .NET 8.0
- **Base de Datos:** SQLite + Entity Framework Core
- **Email:** SendGrid / SMTP (dual service)
- **Testing:** xUnit + FsCheck (55 pruebas pasando, 100%)
- **Bundle:** 75.14 KB gzipped (optimizado)
- **Performance:** Lighthouse 100 (Performance), 100 (SEO)

## Características Destacadas

✅ Backend API REST funcional (2 endpoints)  
✅ Frontend React + TypeScript optimizado (75KB gzipped)  
✅ Gallery desde BD con imágenes locales optimizadas  
✅ Formulario de contacto end-to-end funcional  
✅ Imágenes optimizadas (WebP + fallback JPG)  
✅ 55 tests unitarios y de integración pasando (100%)  
✅ Rate limiting y seguridad implementada  
✅ Documentación exhaustiva (17+ documentos)  
✅ Responsive design mobile-first  
✅ Lighthouse: Performance 100, Accessibility 88, SEO 92  
✅ End-to-end validado (Gallery + Formulario + BD)  
⏳ Configuración de producción (5% restante)  

## Requisitos Previos

- **.NET 8.0 SDK** - [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 18+** y npm - [Descargar aquí](https://nodejs.org/)
- **Git** - [Descargar aquí](https://git-scm.com/)
- **SQLite** (incluido con .NET)
- **Cuenta de SendGrid** (opcional, para email en producción)

## 🚀 Inicio Rápido

### 1. Clonar el Repositorio

```bash
git clone https://github.com/Jorgez-tech/Tatoo_ink.git
cd Tatoo_ink
```

### 2. Configurar Backend

```bash
cd backend

# La BD se crea automáticamente al ejecutar
dotnet run --launch-profile http
```

El backend estará disponible en: `http://localhost:5177`  
Swagger UI: `http://localhost:5177/swagger`

### 3. Configurar Frontend

```bash
# Desde la raíz del proyecto
npm install

# Ejecutar frontend
npm run dev
```

El frontend estará disponible en: `http://localhost:5173`

## Ejecución Rápida

### Backend

```bash
cd backend
dotnet run --launch-profile http
```

- API disponible en: `http://localhost:5177`
- Swagger UI: `http://localhost:5177/swagger`
- Health check: `http://localhost:5177/health`

### Frontend

```bash
npm install
npm run dev
```

- App disponible en: `http://localhost:5173`
- Hot reload habilitado

### Verificación

**Backend:**

```bash
cd backend.Tests
dotnet test
```

**Frontend:**

```bash
npm run build
npm run lint
```

**Para más detalles:** Ver [Getting Started](docs/GETTING-STARTED.md)

## Estructura del Proyecto

```
Tatoo_ink/
??? backend/                    # Backend ASP.NET Core
?   ??? Controllers/           # Endpoints REST API
?   ??? Services/              # Logica de negocio
?   ??? Models/                # Entidades y DTOs
?   ??? Data/                  # DbContext y migraciones
?   ??? Validators/            # Validaciones FluentValidation
?   ??? Middleware/            # Middleware personalizado
?   ??? Utils/                 # Utilidades
?   ??? Program.cs             # Configuracion de la app
??? backend.Tests/             # Pruebas (55 tests)
??? src/                       # Frontend React
?   ??? components/            # Componentes React
?   ?   ??? layout/           # Navbar, Footer
?   ?   ??? sections/         # Hero, Services, Gallery, etc.
?   ?   ??? ui/               # Componentes UI base
?   ??? config/                # Configuracion centralizada
?   ??? hooks/                 # Custom hooks
?   ??? lib/                   # Utilidades
?   ??? types/                 # Tipos TypeScript
?   ??? styles/                # Estilos globales
??? docs/                      # Documentacion del proyecto
?   ??? NEXT-STEPS.md         # Proximos pasos y guia de trabajo
?   ??? CUSTOMIZATION.md      # Guia de personalizacion
?   ??? STRUCTURE.md          # Arquitectura tecnica
?   ??? PERFORMANCE.md        # Metricas y optimizaciones
?   ??? ACCESSIBILITY.md      # Cumplimiento WCAG AA
?   ??? DEPLOYMENT.md         # Guia de despliegue
??? public/                    # Assets estaticos
??? .kiro/specs/              # Especificaciones tecnicas
??? README.md                 # Este archivo
```

## Pruebas

### Backend - Pruebas Unitarias

```bash
cd backend.Tests
dotnet test

# Con detalles
dotnet test --verbosity normal

# Con cobertura
dotnet test --collect:"XPlat Code Coverage"
```

**Estado actual:** 55 pruebas pasando (100% exito)

### Frontend - Linting

```bash
npm run lint
```

## Caracteristicas Principales

### Frontend
- Diseno responsive completo (mobile-first)
- Scroll spy en navegacion
- Lazy loading de imagenes
- Lightbox interactivo en galeria
- Validacion de formularios
- Animaciones optimizadas
- Bundle optimizado (80KB gzipped)
- Accesibilidad WCAG AA
- SEO completo (Open Graph, Twitter Cards)

### Backend
- API RESTful con ASP.NET Core
- Validacion robusta con FluentValidation
- Persistencia en base de datos (SQLite)
- Envio de emails (SendGrid/SMTP)
- Logging estructurado con Serilog
- Manejo global de excepciones
- Rate limiting (10 req/min)
- Sanitizacion de entrada (XSS prevention)
- CORS configurado
- Swagger UI para documentacion

## Documentación

### Primeros Pasos

- **[Getting Started](docs/GETTING-STARTED.md)** - Setup inicial completo
- **[Architecture](docs/ARCHITECTURE.md)** - Arquitectura del sistema
- **[Development Guide](docs/DEVELOPMENT-GUIDE.md)** - Convenciones y testing

### Referencia Técnica

- **[API Reference](docs/API-REFERENCE.md)** - Especificación de endpoints
- **[Security](docs/SECURITY.md)** - Controles de seguridad
- **[Performance](docs/PERFORMANCE.md)** - Optimizaciones
- **[Accessibility](docs/ACCESSIBILITY.md)** - Cumplimiento WCAG AA

### Operaciones

- **[Deployment](docs/DEPLOYMENT.md)** - Guías de despliegue
- **[Customization](docs/CUSTOMIZATION.md)** - Personalización del template
- **[Changelog](docs/CHANGELOG.md)** - Historial de cambios

### READMEs Específicos

- **[Backend README](backend/README.md)** - Configuración del backend
- **[Frontend README](src/README.md)** - Configuración del frontend

**Índice completo:** [docs/README.md](docs/README.md)

## Despliegue

### Opciones de Hosting

**Frontend:**
- Vercel (recomendado)
- Netlify
- GitHub Pages
- Cloudflare Pages

**Backend:**
- Azure App Service
- AWS Elastic Beanstalk
- Heroku
- Railway
- VPS (Linux + Nginx)

Ver `docs/DEPLOYMENT.md` para guias detalladas.

## Licencia

Proyecto privado - Ink Studio

## Soporte

Para preguntas o issues, consulta la documentacion en `docs/` o contacta al equipo de desarrollo.

---

**Nota:** Este proyecto mantiene una politica estricta de no uso de emojis en codigo y documentacion para mantener un perfil profesional.
