# Development Guide - Ink Studio

Guía completa de desarrollo, testing, calidad y convenciones del proyecto Ink Studio.

## Tabla de Contenidos

1. [Filosofía y Objetivos](#filosofia-y-objetivos)
2. [Convenciones de Código](#convenciones-de-codigo)
3. [Estructura del Proyecto](#estructura-del-proyecto)
4. [Testing y QA](#testing-y-qa)
5. [Git Workflow](#git-workflow)
6. [Optimización de Imágenes](#optimizacion-de-imagenes)
7. [Code Review](#code-review)
8. [Troubleshooting](#troubleshooting)

---

## Filosofía y Objetivos

### Principios del Proyecto

- **Simplicidad sobre complejidad:** Código entendible por cualquier desarrollador
- **Reutilización:** Template base adaptable para múltiples clientes
- **Type-safety:** TypeScript estricto sin `any`
- **Configuración centralizada:** Separar datos de presentación
- **Convencionalidad:** Estructura predecible y commits en español

### Objetivos de Calidad

- **Frontend:** Build sin errores TypeScript, bundle < 80KB gzipped
- **Backend:** 100% tests pasando, sin errores de compilación
- **Documentación:** Actualizada y sincronizada con el código
- **Performance:** Lighthouse > 90 en todas las métricas
- **Accesibilidad:** WCAG AA compliance

---

## Convenciones de Código

### TypeScript

**Tipos explícitos:**

```typescript
// ? CORRECTO
interface HeroProps {
  title: string;
  description: string;
}

export function Hero({ title, description }: HeroProps) {
  // ...
}

// ? INCORRECTO
export function Hero(props: any) {
  // ...
}
```

**Usar tipos desde `types/`:**

```typescript
import type { Service, GalleryImage } from "@/types";
```

**Evitar `any` y `unknown`:**

```typescript
// ? CORRECTO
const handleClick = (e: React.MouseEvent<HTMLButtonElement>) => {
  // ...
};

// ? INCORRECTO
const handleClick = (e: any) => {
  // ...
};
```

### React Components

**Named exports:**

```typescript
// ? CORRECTO
export function ComponentName() { }

// ? INCORRECTO
export default function ComponentName() { }
```

**Documentación con JSDoc:**

```typescript
/**
 * Componente Hero - Sección principal de la landing page
 * 
 * Muestra título, descripción y CTA con imagen de fondo.
 * Incluye optimizaciones de LCP (eager loading, fetchPriority).
 * 
 * @component
 * @example
 * ```tsx
 * <Hero />
 * ```
 * 
 * @see {@link config/content.ts} para personalizar contenido
 * @see {@link config/images.ts} para cambiar imagen de fondo
 */
export function Hero() {
  // ...
}
```

### Tailwind CSS

**Usar `cn()` para clases condicionales:**

```typescript
import { cn } from "@/lib/utils";

<div className={cn(
  "base-classes",
  condition && "conditional-classes",
  variable === value ? "true-classes" : "false-classes"
)} />

// ? NO usar template literals para condicionales
<div className={`base ${condition ? 'yes' : 'no'}`} />
```

**Responsive design (mobile-first):**

```typescript
className="text-base        /* Base = mobile */
          sm:text-lg        /* 640px+ */
          md:text-xl        /* 768px+ */  
          lg:text-2xl"      /* 1024px+ */
```

### C# (.NET Backend)

**Convenciones de nombres:**

```csharp
// PascalCase para clases, métodos, propiedades
public class ContactService { }
public async Task<ServiceResult> ProcessMessageAsync() { }

// camelCase para parámetros y variables locales
public void Method(string userName) {
    var localVariable = "value";
}
```

**Async/Await:**

```csharp
// ? Siempre usar await con operaciones asíncronas
var result = await _service.ProcessAsync();

// ? NO bloquear con .Result o .Wait()
var result = _service.ProcessAsync().Result; // Evitar
```

**Dependency Injection:**

```csharp
// ? Inyectar dependencias por constructor
public class ContactService : IContactService
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;
    
    public ContactService(
        ApplicationDbContext context,
        IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }
}
```

---

## Estructura del Proyecto

### Frontend (src/)

**Estructura fija (NO modificar sin coordinación):**

```
src/
??? components/
?   ??? layout/           # Navbar, Footer
?   ??? sections/         # Hero, Services, Gallery, About, Contact
?   ??? ui/               # Solo 6 componentes activos
?   ??? shared/           # (vacía, reservada)
??? config/               # Configuración centralizada
?   ??? business-info.ts
?   ??? content.ts
?   ??? images.ts
?   ??? navigation.ts
?   ??? services.ts
?   ??? api.ts
??? hooks/                # Custom hooks
??? lib/                  # Utilidades
??? types/                # Tipos TypeScript
??? services/             # Servicios de API
??? styles/               # Estilos globales
```

### Componentes UI Activos (únicos permitidos)

1. `button.tsx`
2. `card.tsx`
3. `input.tsx`
4. `textarea.tsx`
5. `label.tsx`
6. `ImageWithFallback.tsx`

**Importante:** No agregar componentes de shadcn/ui sin coordinación. Los 40+ restantes fueron eliminados en Fase 1.

### Importaciones

**Usar path aliases `@/`:**

```typescript
// ? CORRECTO
import { Button } from "@/components/ui/button";
import { heroContent } from "@/config/content";
import { cn } from "@/lib/utils";

// ? INCORRECTO
import { Button } from "../../../components/ui/button";
import { heroContent } from "../../config/content";
```

### Configuración Centralizada

**Todos los datos van en `config/`:**

```typescript
// ? INCORRECTO - hardcodear en componente
<h1>Ink Studio</h1>

// ? CORRECTO - usar configuración
import { businessInfo } from "@/config/business-info";
<h1>{businessInfo.name}</h1>
```

### Backend (backend/)

```
backend/
??? Controllers/          # Endpoints REST API
??? Services/             # Lógica de negocio
??? Models/               # Entidades y DTOs
??? Data/                 # DbContext y migraciones
??? Validators/           # FluentValidation
??? Middleware/           # Middleware personalizado
??? Utils/                # Utilidades
??? Program.cs            # Configuración
```

---

## Testing y QA

### Backend - Tests Unitarios y de Integración

**Ejecutar todos los tests:**

```bash
dotnet test backend.Tests/backend.Tests.csproj
```

**Con detalles:**

```bash
dotnet test backend.Tests/backend.Tests.csproj --verbosity normal
```

**Con cobertura:**

```bash
dotnet test backend.Tests/backend.Tests.csproj --collect:"XPlat Code Coverage"
```

**Estado actual:** 55 tests pasando (100%)

**Tipos de pruebas:**

- Unitarias: Validación, sanitización, servicios
- Property-based: FsCheck para casos edge
- Integración: WebApplicationFactory para E2E

### Frontend - Build y Lint

**Build (TypeScript + Vite):**

```bash
npm run build
```

**Debe pasar sin errores TypeScript ni de compilación.**

**Lint:**

```bash
npm run lint
```

**Solucionar todos los errores antes de commit.**

### Testing Manual

**Smoke test checklist:**

- [ ] Navegación responsive (mobile/tablet/desktop)
- [ ] Scroll spy en Navbar
- [ ] Lazy loading de imágenes
- [ ] Lightbox de Gallery con teclado (flechas, ESC)
- [ ] Formulario de contacto:
  - [ ] Validación de campos
  - [ ] Estado de loading
  - [ ] Mensaje de éxito
  - [ ] Manejo de errores
- [ ] Smooth scroll al hacer clic en navegación

### Checklist Pre-Release

Antes de publicar a producción:

- [ ] `npm run build` (sin errores)
- [ ] `npm run lint` (sin errores)
- [ ] `dotnet build backend/backend.csproj` (sin errores)
- [ ] `dotnet test backend.Tests/backend.Tests.csproj` (55/55 ok)
- [ ] Variables de entorno configuradas
- [ ] CORS configurado para dominio de producción
- [ ] SSL/TLS configurado
- [ ] Rate limiting validado
- [ ] Lighthouse audit > 90 en todas las métricas
- [ ] Documentación actualizada

---

## Git Workflow

### Inicio de Sesión

1. Pull del remoto:
   ```bash
   git pull origin master
   ```

2. Leer documentación:
   - `docs/NEXT-STEPS.md` - Estado actual
   - Documento de la fase/tarea actual

3. Revisar estado:
   ```bash
   git status
   git log --oneline -10
   ```

### Durante el Desarrollo

**Commits frecuentes y descriptivos:**

```bash
git add .
git commit -m "feat(navbar): añade detección de scroll"
```

**Verificar calidad antes de cada commit:**

```bash
# Frontend
npm run build

# Backend
dotnet test backend.Tests/backend.Tests.csproj
```

### Conventional Commits (Español)

**Formato:**
```
<tipo>(<alcance>): <descripción>

[cuerpo opcional]
```

**Tipos válidos:**

- `feat` - Nueva funcionalidad
- `fix` - Corrección de bug  
- `refactor` - Refactorización sin cambio funcional
- `style` - Cambios de estilos visuales
- `docs` - Documentación
- `chore` - Mantenimiento
- `perf` - Performance
- `test` - Tests

**Alcances comunes:**

- `navbar`, `hero`, `services`, `gallery`, `about`, `contact`, `footer`
- `config`, `types`, `hooks`, `ui`
- `img`, `html`, `css`

**Ejemplos:**

```bash
feat(navbar): añade detección de scroll
fix(hero): corrige fetchPriority en imagen
docs: actualiza STATUS con progreso de Fase 2
refactor(gallery): mejora navegación del lightbox
perf(img): implementa lazy loading
chore: elimina componentes UI no utilizados
```

### Final de Sesión

1. **Actualizar `docs/NEXT-STEPS.md`:**
   - Tareas completadas con checkmarks
   - Progreso actualizado (%)
   - Problemas encontrados
   - Próximos pasos

2. **Commit de documentación:**
   ```bash
   git add docs/NEXT-STEPS.md
   git commit -m "docs: actualiza progreso de sesión"
   ```

3. **Push al remoto:**
   ```bash
   git push origin master
   ```

### Branches (Opcional)

Para features grandes, usar branches:

```bash
# Crear branch
git checkout -b feature/nueva-funcionalidad

# Trabajar y commit
git add .
git commit -m "feat: implementa nueva funcionalidad"

# Push del branch
git push origin feature/nueva-funcionalidad

# Merge a master (después de revisar)
git checkout master
git merge feature/nueva-funcionalidad
git push origin master
```

---

## Optimización de Imágenes

### Proceso Manual con Squoosh

1. **Descargar imágenes originales** (si son externas)

2. **Optimizar en Squoosh:**
   - Ir a https://squoosh.app/
   - Arrastrar imagen
   - Configurar formato WebP (calidad 80-85)
   - Descargar optimizada
   - Repetir para JPG (fallback, calidad 85)

3. **Guardar en estructura:**
   ```
   public/images/
   ??? hero/
   ?   ??? imagen.webp
   ?   ??? imagen.jpg
   ??? about/
   ?   ??? imagen.webp
   ?   ??? imagen.jpg
   ??? gallery/
       ??? imagen-1.webp
       ??? imagen-1.jpg
       ??? ...
   ```

4. **Actualizar `config/images.ts`:**
   ```typescript
   export const heroImage = {
     src: "/images/hero/imagen.webp",
     fallback: "/images/hero/imagen.jpg",
     alt: "Descripción",
     width: 1920,
     height: 1080
   };
   ```

### Proceso Automático con Sharp (Node.js)

**Crear script `scripts/optimize-images.mjs`:**

```javascript
import sharp from 'sharp';
import fs from 'fs';

const images = [
  {
    input: 'src-image.jpg',
    output: 'public/images/hero/imagen',
    width: 1920,
    height: 1080
  }
];

async function optimize(input, output, width, height) {
  // WebP
  await sharp(input)
    .resize(width, height, { fit: 'cover' })
    .webp({ quality: 80, effort: 6 })
    .toFile(`${output}.webp`);

  // JPG fallback
  await sharp(input)
    .resize(width, height, { fit: 'cover' })
    .jpeg({ quality: 85, mozjpeg: true })
    .toFile(`${output}.jpg`);
}

for (const img of images) {
  await optimize(img.input, img.output, img.width, img.height);
}
```

**Ejecutar:**

```bash
node scripts/optimize-images.mjs
```

### Tamaños Recomendados

| Tipo | Ancho | Alto | Formato | Calidad |
|------|-------|------|---------|---------|
| Hero | 1920px | 1080px | WebP + JPG | 80-85 |
| About | 1200px | 800px | WebP + JPG | 80-85 |
| Gallery | 800px | 600px | WebP + JPG | 80-85 |

### Componente ImageWithFallback

El componente soporta WebP con fallback a JPG:

```typescript
<ImageWithFallback
  src="/images/hero/imagen.webp"
  fallback="/images/hero/imagen.jpg"
  alt="Descripción"
  width={1920}
  height={1080}
  className="..."
/>
```

---

## Code Review

### Checklist de Revisión

**Código:**

- [ ] TypeScript sin errores ni `any`
- [ ] Props tipadas correctamente
- [ ] Imports con alias `@/`
- [ ] Componentes documentados con JSDoc
- [ ] Sin código duplicado
- [ ] Configuración centralizada (no hardcode)

**Estilos:**

- [ ] Usar `cn()` para clases condicionales
- [ ] Responsive mobile-first
- [ ] Sin inline styles innecesarios
- [ ] Clases de Tailwind consistentes

**Performance:**

- [ ] Imágenes optimizadas (WebP + fallback)
- [ ] Lazy loading donde corresponda
- [ ] Sin re-renders innecesarios
- [ ] Bundle size razonable

**Accesibilidad:**

- [ ] Alt text en imágenes
- [ ] Labels asociados a inputs
- [ ] Navegación por teclado funcional
- [ ] Contraste de colores adecuado

**Testing:**

- [ ] Tests del backend pasando
- [ ] Build del frontend exitoso
- [ ] Smoke test manual completado

**Documentación:**

- [ ] `docs/NEXT-STEPS.md` actualizado
- [ ] README actualizado si corresponde
- [ ] Comentarios en código complejo

---

## Troubleshooting

### Build Failures

**Error: "Cannot find module '@/...'"**

Verificar `tsconfig.json`:

```json
{
  "compilerOptions": {
    "paths": {
      "@/*": ["./src/*"]
    }
  }
}
```

**Error: "Type 'any' is not assignable"**

Tipar correctamente:

```typescript
// ? Incorrecto
const data: any = await fetch(...);

// ? Correcto
const data: ResponseType = await fetch(...);
```

### Test Failures

**Error: "Database locked"**

Cerrar conexiones y eliminar `inkstudio.db`, se recreará automáticamente.

**Error: "Nullable reference warnings"**

Habilitar nullable reference context en `backend.csproj`:

```xml
<Nullable>enable</Nullable>
```

### Runtime Errors

**Error: "CORS policy blocked"**

Verificar `CorsSettings` en `appsettings.json`:

```json
"CorsSettings": {
  "AllowedOrigins": ["http://localhost:5173"]
}
```

**Error: "Rate limit exceeded"**

Esperar 1 minuto o ajustar límite en `appsettings.json`.

---

## Recursos Adicionales

### Documentación del Proyecto

- [Getting Started](GETTING-STARTED.md) - Setup inicial
- [Architecture](ARCHITECTURE.md) - Arquitectura completa
- [API Reference](API-REFERENCE.md) - Especificación API
- [Customization](CUSTOMIZATION.md) - Personalización

### Herramientas

- **Squoosh:** https://squoosh.app/ (optimización de imágenes)
- **Lighthouse:** Chrome DevTools (performance audit)
- **axe DevTools:** https://www.deque.com/axe/ (accesibilidad)

### Documentación Externa

- [React 19 Docs](https://react.dev/)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)
- [Tailwind CSS v3](https://tailwindcss.com/docs)
- [Vite Guide](https://vite.dev/guide/)
- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)

---

**Última actualización:** 2025-01-09  
**Próxima revisión:** Al completar consolidación de documentación
