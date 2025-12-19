# Ink Studio - Landing Page Fullstack

Landing page profesional para estudio de tatuajes con backend ASP.NET Core y frontend React + TypeScript.

## Descripcion General

Solucion fullstack completa que permite a los clientes enviar mensajes de contacto y solicitar citas a traves de un formulario web. El sistema persiste los datos en base de datos y envia notificaciones por correo electronico al estudio.

**Stack Tecnologico:**
- **Frontend:** React 18.0 + TypeScript 5.9 + Vite 7 + Tailwind CSS 3.4
- **Backend:** ASP.NET Core Web API .NET 8.0
- **Base de Datos:** SQLite
- **Email:** SendGrid / SMTP
- **Testing:** xUnit (55 pruebas)

## Requisitos Previos

- **.NET 8.0 SDK** - [Descargar aqui](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 18+** y npm - [Descargar aqui](https://nodejs.org/)
- **Git** - [Descargar aqui](https://git-scm.com/)
- **SQLite** (incluido con .NET)
- **Cuenta de SendGrid** (opcional, para email en produccion)

## Instalacion y Configuracion

### 1. Clonar el Repositorio

```bash
git clone https://github.com/Jorgez-tech/Tatoo_ink.git
cd Tatoo_ink
```

### 2. Configurar Backend

```bash
cd backend

# Copiar archivo de configuracion de ejemplo
cp appsettings.Development.json.example appsettings.Development.json

# Editar appsettings.Development.json con tus valores
# (ConnectionString, EmailSettings, etc.)

# Aplicar migraciones de base de datos
dotnet ef database update

# Ejecutar backend
dotnet run
```

El backend estara disponible en: `https://localhost:7000`

### 3. Configurar Frontend

```bash
# Desde la raiz del proyecto
npm install

# Copiar archivo de variables de entorno
cp .env.example .env

# Editar .env con la URL del backend
# VITE_API_BASE_URL=https://localhost:7000

# Ejecutar frontend
npm run dev
```

El frontend estara disponible en: `http://localhost:5173`

## Ejecucion del Proyecto Completo

### Backend

1. Configurar `appsettings.Development.json` (ver `backend/README.md`)
2. Aplicar migraciones:
   ```bash
   cd backend
   dotnet ef database update
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
