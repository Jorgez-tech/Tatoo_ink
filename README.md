# Ink Studio - Landing Page Fullstack

Landing page profesional para estudio de tatuajes con backend ASP.NET Core y frontend React + TypeScript.

**Estado:** [OK] Casi listo para producción (95% completado) | [Ver estado detallado](docs/NEXT-STEPS.md)

## Descripcion General

Solución fullstack completa que permite a los clientes enviar mensajes de contacto y solicitar citas a través de un formulario web. El sistema persiste los datos en base de datos y envía notificaciones por correo electrónico al estudio.

**Stack Tecnológico:**
- **Frontend:** React 18.0 + TypeScript 5.9 + Vite 7 + Tailwind CSS 3.4
- **Backend:** ASP.NET Core Web API .NET 8.0
- **Base de Datos:** SQLite
- **Email:** SendGrid / SMTP
- **Testing:** xUnit (55 pruebas pasando)
- **Bundle:** 75.14 KB gzipped (optimizado)

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

## Ejecución del Proyecto Completo

### Backend

1. La base de datos se inicializa automáticamente con DbInitializer
2. Ejecutar:
   ```bash
   cd backend
   dotnet run --launch-profile http
   ```
3. Ejecutar API:
   ```bash
   dotnet run --project backend
   ```
4. API disponible en: `https://localhost:7000`
5. Swagger UI: `https://localhost:7000/swagger`

### Frontend

1. Instalar dependencias:
   ```bash
   npm install
   ```
2. Ejecutar en desarrollo:
   ```bash
   npm run dev
   ```
3. Frontend disponible en: `http://localhost:5173`

### Comunicacion Frontend-Backend

- El frontend llama al endpoint `POST /api/contact`
- CORS configurado en backend para permitir `http://localhost:5173`
- Variables de entorno en `.env` (ver `.env.example`)

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

## Documentacion Adicional

- Indice: `docs/README.md`
- Arquitectura: `docs/ARCHITECTURE.md`
- API REST: `docs/API-REST.md`
- Seguridad: `docs/SECURITY.md`
- QA y testing: `docs/QA.md`
- Backend: `backend/README.md`
- Despliegue: `docs/DEPLOYMENT.md`
- Personalización: `docs/CUSTOMIZATION.md`
- Performance: `docs/PERFORMANCE.md`
- Accesibilidad: `docs/ACCESSIBILITY.md`
- Estado y próximos pasos: `docs/NEXT-STEPS.md`

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
