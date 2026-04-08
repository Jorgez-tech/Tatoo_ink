# Getting Started - Ink Studio

Guía de inicio rápido para configurar y ejecutar el proyecto Ink Studio (frontend + backend) en entorno de desarrollo local.

## Tabla de Contenidos

1. [Requisitos Previos](#requisitos-previos)
2. [Instalación](#instalacion)
3. [Configuración Backend](#configuracion-backend)
4. [Configuración Frontend](#configuracion-frontend)
5. [Ejecución del Proyecto](#ejecucion-del-proyecto)
6. [Verificación](#verificacion)
7. [Troubleshooting](#troubleshooting)
8. [Próximos Pasos](#proximos-pasos)

---

## Requisitos Previos

### Software necesario

- **.NET SDK 8.0** - [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 18+** y npm - [Descargar aquí](https://nodejs.org/)
- **Git** - [Descargar aquí](https://git-scm.com/)
- **Editor de código** - Visual Studio Code (recomendado) o Visual Studio 2022

### Herramientas opcionales

- **dotnet-ef** (para migraciones manuales): `dotnet tool install --global dotnet-ef`
- **Postman** o **Thunder Client** (VS Code) para probar la API

---

## Instalación

### 1. Clonar el repositorio

```bash
git clone https://github.com/Jorgez-tech/Tatoo_ink.git
cd Tatoo_ink
```

### 2. Instalar dependencias del frontend

```bash
npm install
```

### 3. Verificar instalación del backend

```bash
cd backend
dotnet restore
dotnet build
cd ..
```

---

## Configuración Backend

### 1. Base de datos

El proyecto usa **SQLite** por defecto. La base de datos se crea automáticamente en el primer arranque con datos de ejemplo.

**Ubicación:** `backend/inkstudio.db`

### 2. Configuración de appsettings

El backend incluye archivos de configuración base:

- `appsettings.json` - Configuración común
- `appsettings.Development.json` - Desarrollo local
- `appsettings.Production.json` - Producción

**Configuración mínima para desarrollo:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=inkstudio.db"
  },
  "EmailSettings": {
    "Provider": "Smtp",
    "SmtpServer": "localhost",
    "SmtpPort": 25,
    "SmtpUsername": "",
    "SmtpPassword": "",
    "StudioEmail": "info@inkstudio.local",
    "StudioName": "Ink Studio",
    "PickupDirectory": "./emails"
  },
  "CorsSettings": {
    "AllowedOrigins": [
      "http://localhost:5173",
      "http://localhost:3000"
    ]
  }
}
```

**Nota:** En desarrollo, los emails se guardan en la carpeta `./emails` en lugar de enviarse realmente.

### 3. Configuración de email (opcional para producción)

**Opción A: SendGrid (recomendado para producción)**

1. Crear cuenta en [SendGrid](https://sendgrid.com/)
2. Obtener API Key desde Settings ? API Keys
3. Actualizar `appsettings.Production.json`:

```json
{
  "EmailSettings": {
    "Provider": "SendGrid",
    "SendGridApiKey": "TU_API_KEY_AQUI",
    "StudioEmail": "contacto@inkstudio.com",
    "StudioName": "Ink Studio"
  }
}
```

**Opción B: SMTP (Gmail u otro proveedor)**

1. Habilitar autenticación de 2 factores en Gmail
2. Generar contraseña de aplicación
3. Actualizar configuración:

```json
{
  "EmailSettings": {
    "Provider": "Smtp",
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUsername": "tu-email@gmail.com",
    "SmtpPassword": "tu-password-de-aplicacion",
    "StudioEmail": "tu-email@gmail.com",
    "StudioName": "Ink Studio"
  }
}
```

### 4. Variables de entorno (alternativa a appsettings)

Para producción, se recomienda usar variables de entorno:

```bash
# Linux/macOS
export ConnectionStrings__DefaultConnection="Data Source=/app/data/inkstudio.db"
export EmailSettings__SendGridApiKey="tu-api-key"

# Windows PowerShell
$env:ConnectionStrings__DefaultConnection="Data Source=C:\app\data\inkstudio.db"
$env:EmailSettings__SendGridApiKey="tu-api-key"
```

---

## Configuración Frontend

### 1. Variables de entorno

Crear archivo `.env` en la raíz del proyecto:

```env
VITE_API_BASE_URL=http://localhost:5177
```

**Nota:** El puerto `5177` es el puerto por defecto del backend en modo HTTP. Ajustar según tu configuración.

### 2. Verificar configuración

El archivo `src/config/api.ts` usa la variable de entorno:

```typescript
export const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5177';
```

---

## Ejecución del Proyecto

### 1. Iniciar el Backend

**Opción A: Desde la raíz del proyecto**

```bash
cd backend
dotnet run --launch-profile http
```

**Opción B: Con Visual Studio**

1. Abrir `backend/backend.csproj`
2. Seleccionar perfil "http"
3. Presionar F5 o "Run"

El backend estará disponible en:
- API: `http://localhost:5177`
- Swagger UI: `http://localhost:5177/swagger`

**Salida esperada:**

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5177
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### 2. Iniciar el Frontend

**En otra terminal:**

```bash
npm run dev
```

El frontend estará disponible en: `http://localhost:5173`

**Salida esperada:**

```
VITE v7.1.12  ready in 523 ms

?  Local:   http://localhost:5173/
?  Network: use --host to expose
```

---

## Verificación

### 1. Health Check del Backend

Abrir en el navegador o con curl:

```bash
curl http://localhost:5177/health
```

**Respuesta esperada:** `Healthy`

### 2. Swagger UI

Abrir en el navegador: `http://localhost:5177/swagger`

Deberías ver la documentación interactiva de la API con 2 endpoints:
- `POST /api/contact`
- `GET /api/gallery`

### 3. Frontend - Página principal

Abrir en el navegador: `http://localhost:5173`

Deberías ver la landing page con:
- Hero section con imagen de fondo
- Servicios
- Galería de imágenes
- Sección Acerca de
- Formulario de contacto
- Footer

### 4. Probar integración Frontend-Backend

**Probar galería:**

1. Abrir `http://localhost:5173`
2. Scroll hasta la sección "Galería"
3. Verificar que se cargan 6 imágenes

**Consola del navegador esperada:**
```
Fetching gallery images from: http://localhost:5177/api/gallery
Gallery images loaded: 6
```

**Probar formulario de contacto:**

1. Scroll hasta "Contacto"
2. Llenar el formulario:
   - Nombre: "Juan Pérez"
   - Email: "juan@test.com"
   - Teléfono: "+56912345678"
   - Mensaje: "Prueba de contacto"
3. Click en "Enviar mensaje"

**Resultado esperado:**
- Mensaje de éxito en el frontend
- Log en el backend: `Mensaje de contacto guardado exitosamente. ID: 1`
- Email guardado en `backend/emails/` (desarrollo)

### 5. Verificar base de datos

```bash
cd backend
sqlite3 inkstudio.db

# Dentro de sqlite3:
.tables
SELECT * FROM ContactMessages;
SELECT * FROM GalleryImages;
.exit
```

---

## Troubleshooting

### Error: "No se puede conectar al backend"

**Síntomas:** 
- Error de red en el formulario de contacto
- Galería no carga imágenes
- Console error: `Failed to fetch`

**Soluciones:**

1. Verificar que el backend esté corriendo:
   ```bash
   curl http://localhost:5177/health
   ```

2. Verificar variable de entorno `.env`:
   ```env
   VITE_API_BASE_URL=http://localhost:5177
   ```

3. Verificar CORS en `backend/appsettings.Development.json`:
   ```json
   "CorsSettings": {
     "AllowedOrigins": ["http://localhost:5173"]
   }
   ```

4. Reiniciar ambos servidores (backend y frontend)

### Error: "CORS policy blocked"

**Síntomas:**
```
Access to fetch at 'http://localhost:5177/api/contact' from origin 'http://localhost:5173' 
has been blocked by CORS policy
```

**Solución:**

1. Abrir `backend/appsettings.Development.json`
2. Agregar el origen del frontend a `AllowedOrigins`:
   ```json
   "CorsSettings": {
     "AllowedOrigins": [
       "http://localhost:5173",
       "http://localhost:3000"
     ]
   }
   ```
3. Reiniciar el backend

### Error: "Database locked"

**Síntomas:**
```
Microsoft.Data.Sqlite.SqliteException: database is locked
```

**Solución:**

1. Cerrar todas las conexiones a la base de datos
2. Eliminar archivo `inkstudio.db` y reiniciar el backend (se recreará automáticamente)
3. Si persiste, verificar que no haya múltiples instancias del backend corriendo

### Error: "Email failed to send"

**Síntomas:**
- Log: `No se pudo enviar el email para el contacto {ContactId}, pero el mensaje fue guardado`

**Solución:**

1. En desarrollo, verificar que exista la carpeta `backend/emails/`
2. Para producción, verificar credenciales en `EmailSettings`
3. El mensaje se guarda en BD aunque falle el email

### Error: "Rate limit exceeded"

**Síntomas:**
```json
{
  "error": "Rate limit exceeded. Please try again later."
}
```

**Solución:**
- Esperar 1 minuto antes de reintentar
- En desarrollo, ajustar `RateLimiting:MaxRequestsPerMinute` en appsettings

### Puerto 5177 ya en uso

**Síntomas:**
```
Unable to bind to http://localhost:5177 on the IPv4 loopback interface: 'Address already in use'.
```

**Solución:**

1. Cambiar puerto en `backend/Properties/launchSettings.json`:
   ```json
   "applicationUrl": "http://localhost:5178"
   ```

2. Actualizar `.env` en el frontend:
   ```env
   VITE_API_BASE_URL=http://localhost:5178
   ```

---

## Próximos Pasos

### Desarrollo

- **Personalización:** Ver [CUSTOMIZATION.md](CUSTOMIZATION.md)
- **Arquitectura:** Ver [ARCHITECTURE.md](ARCHITECTURE.md)
- **API Reference:** Ver [API-REFERENCE.md](API-REFERENCE.md)
- **Guía de desarrollo:** Ver [DEVELOPMENT-GUIDE.md](DEVELOPMENT-GUIDE.md)

### Testing

```bash
# Tests del backend
cd backend.Tests
dotnet test

# Linting del frontend
npm run lint
```

### Despliegue

Ver [DEPLOYMENT.md](DEPLOYMENT.md) para guías de despliegue en:
- Vercel/Netlify (frontend)
- Azure/AWS/Railway (backend)

### Personalización

Ver [CUSTOMIZATION.md](CUSTOMIZATION.md) para:
- Cambiar colores y estilos
- Modificar contenido y textos
- Agregar nuevas secciones
- Configurar dominio personalizado

---

## Recursos Adicionales

### Documentación del proyecto

- [Índice de documentación](README.md)
- [Arquitectura completa](ARCHITECTURE.md)
- [Referencia API REST](API-REFERENCE.md)
- [Guía de personalización](CUSTOMIZATION.md)

### Herramientas útiles

- **Swagger UI:** `http://localhost:5177/swagger`
- **SQLite Browser:** [DB Browser for SQLite](https://sqlitebrowser.org/)
- **Postman:** [Colección de API](../backend/Postman/)

### Soporte

- Issues del proyecto: GitHub Issues
- Documentación técnica: [docs/](.)
- Backend README: [backend/README.md](../backend/README.md)
- Frontend README: [src/README.md](../src/README.md)

---

**Tiempo estimado de setup:** 15-30 minutos

**Última actualización:** 2025-01-09
