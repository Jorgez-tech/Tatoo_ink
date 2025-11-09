# Instrucciones para Agentes de IA - Ink Studio Project

> **Última actualización:** 2025-11-09  
> **Proyecto:** Landing Page System - Ink Studio (Prototipo Demo)  
> **Estado:** Fase 2 COMPLETADA - En Fase 3 (Documentación)

---

## 🎯 Contexto del Proyecto

### Descripción
Landing page profesional para estudio de tatuajes "Ink Studio", construida como **prototipo reutilizable** para sistema de plantillas destinadas a pequeños negocios y profesionales en Chile.

### Stack Tecnológico
- **Frontend:** React 19 + TypeScript 5.9.3
- **Build Tool:** Vite 7.1.12  
- **Estilos:** Tailwind CSS 3.4.17 (NO v4)
- **UI Components:** shadcn/ui (6 componentes activos)
- **Iconos:** Lucide React
- **Backend (futuro):** ASP.NET Core + CMS headless

### Filosofía del Proyecto
- ✅ **Simplicidad sobre complejidad:** Código entendible
- ✅ **Reutilización:** Template base para múltiples clientes
- ✅ **Type-safety:** TypeScript estricto
- ✅ **Configuración centralizada:** Separar datos de presentación
- ✅ **Convencionalidad:** Commits en español, estructura predecible

---

## 📋 Reglas Fundamentales para Agentes de IA

### 1. SIEMPRE Leer Primero
Antes de hacer cualquier cambio, lee estos archivos en orden:

```
1. docs/STATUS.md           # Estado actual y progreso
2. docs/00-PLAN-MAESTRO.md  # Visión general
3. docs/[FASE-ACTUAL].md    # Documento de fase activa
4. docs/CHANGELOG.md        # Últimos cambios
```

### 2. NO Hacer Sin Confirmación
❌ **NUNCA hacer esto sin preguntar:**
- Instalar/desinstalar dependencias
- Cambiar versiones de packages (especialmente Tailwind)
- Eliminar archivos existentes
- Modificar configuración de Vite/TypeScript/Tailwind
- Crear nuevas carpetas en `src/`
- Refactorizar componentes completos

✅ **Puedes hacer directamente:**
- Corregir errores de TypeScript/sintaxis
- Actualizar documentación (STATUS.md, CHANGELOG.md)
- Pequeños ajustes de estilos
- Agregar comentarios/JSDoc
- Mejorar tipos existentes

### 3. Commits en Español
**IMPORTANTE:** Todos los commits deben estar en español con conventional commits:

```bash
# ✅ CORRECTO
feat(navbar): añade detección de scroll
fix(hero): corrige fetchPriority en imagen
docs: actualiza STATUS con progreso de Fase 2
refactor(gallery): mejora navegación del lightbox
perf(img): implementa lazy loading
chore: elimina componentes UI no utilizados

# ❌ INCORRECTO
feat: add scroll detection
fix: image loading issue
update docs
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

### 4. Estructura de Archivos FIJA
No crear ni mover carpetas sin aprobación. Estructura actual:

```
src/
├── components/
│   ├── layout/           # Navbar, Footer
│   ├── sections/         # Hero, Services, Gallery, About, Contact
│   ├── ui/               # Solo 6 componentes activos
│   └── shared/           # (vacía, reservada)
├── config/               # Archivos de configuración
│   ├── business-info.ts
│   ├── content.ts
│   ├── images.ts
│   ├── navigation.ts
│   ├── services.ts
│   └── api.ts
├── hooks/                # Custom hooks
│   ├── use-active-section.ts
│   └── use-mobile.ts
├── lib/                  # Utilidades
│   └── utils.ts
├── types/                # Tipos TypeScript
│   └── index.ts
└── styles/               # Estilos globales
    └── globals.css
```

### 5. Importaciones Correctas
Usa path aliases `@/` configurados:

```typescript
// ✅ CORRECTO
import { Button } from "@/components/ui/button";
import { heroContent } from "@/config/content";
import { cn } from "@/lib/utils";

// ❌ INCORRECTO
import { Button } from "../../../components/ui/button";
import { heroContent } from "../../config/content";
```

### 6. Componentes UI Activos
**Solo estos 6 componentes existen** en `components/ui/`:
- `button.tsx`
- `card.tsx`
- `input.tsx`
- `textarea.tsx`
- `label.tsx`
- `ImageWithFallback.tsx`

❌ NO sugerir ni usar otros componentes de shadcn/ui (fueron eliminados en Fase 1)

### 7. Configuración Centralizada
Todos los datos van en archivos de `config/`:

```typescript
// ❌ INCORRECTO - hardcodear en componente
<h1>Ink Studio</h1>

// ✅ CORRECTO - usar configuración
import { businessInfo } from "@/config/business-info";
<h1>{businessInfo.name}</h1>
```

---

## 🛠️ Convenciones de Código

### TypeScript
```typescript
// Siempre tipar props
interface HeroProps {
  title: string;
  description: string;
}

export function Hero({ title, description }: HeroProps) {
  // ...
}

// Usar tipos desde config/types
import type { Service, GalleryImage } from "@/types";
```

### React Components
```typescript
// Preferir named exports
export function ComponentName() { }

// Usar tipos explícitos para eventos
const handleClick = (e: React.MouseEvent<HTMLButtonElement>) => { };
```

### Tailwind CSS
```typescript
// Usar cn() para clases condicionales
import { cn } from "@/lib/utils";

<div className={cn(
  "base-classes",
  condition && "conditional-classes",
  variable === value ? "true-classes" : "false-classes"
)} />

// ❌ NO usar template literals para condicionales
<div className={`base ${condition ? 'yes' : 'no'}`} />
```

### Responsive Design
Orden de breakpoints:
```css
/* Mobile first */
className="text-base        /* Base = mobile */
          sm:text-lg        /* 640px+ */
          md:text-xl        /* 768px+ */  
          lg:text-2xl"      /* 1024px+ */
```

---

## 📊 Estado Actual del Proyecto

### Progreso Global: 55%
```
Fase 0: Configuración      [##########] 100% ✅
Fase 1: Auditoría          [##########] 100% ✅  
Fase 2: Optimización       [##########] 100% ✅
Fase 3: Documentación      [..........] 0%  🔄 ACTUAL
Fase 4: Finalización       [..........] 0%  ⏳
```

### Completado ✅
- 40 componentes UI eliminados (quedaron 6)
- Estructura de carpetas reorganizada (layout/, sections/, ui/, hooks/, lib/, config/, types/)
- Configuración centralizada implementada
- Scroll spy en Navbar con detección de scroll
- Lazy loading de imágenes con placeholders
- Lightbox de Gallery con navegación por teclado
- Smooth scroll global
- Responsive optimizado en todos los componentes
- Mejoras de performance (LCP, CLS, eager loading, fetchPriority)
- SEO básico (meta tags OG/Twitter/description)
- PWA manifest mínimo
- Git workflow con conventional commits en español

### En Progreso 🔄
- **Fase 3:** Documentación (JSDoc en componentes 0/7)

### Pendiente ⏳
- JSDoc en 7 componentes principales
- README.md completo
- CUSTOMIZATION.md
- STRUCTURE.md
- Optimización de imágenes (mover a local)
- Performance audit final
- Accesibilidad (WCAG AA)

---

## 🚨 Errores Comunes a Evitar

### 1. Tailwind CSS v4
❌ **NO actualizar a Tailwind v4** (tiene breaking changes en PostCSS)  
✅ Mantener en v3.4.17

### 2. Imports Rotos
❌ Al mover archivos, actualizar TODOS los imports  
✅ Usar búsqueda global (grep/search) antes de mover

### 3. Duplicar Código
❌ Hardcodear texto/datos en múltiples lugares  
✅ Centralizar en `config/`

### 4. Props HTML en React
❌ `fetchpriority="high"` (HTML attribute)  
✅ `fetchPriority="high"` (React prop - camelCase)

### 5. Componentes UI Inexistentes
❌ `import { Dialog } from "@/components/ui/dialog"`  
✅ Solo usar los 6 componentes activos listados arriba

---

## 📝 Workflow de Trabajo

### Inicio de Tarea
1. Lee `docs/STATUS.md` para contexto
2. Verifica la fase actual en `docs/00-PLAN-MAESTRO.md`
3. Lee el documento de la fase (`docs/0X-FASE-X-XXX.md`)
4. Confirma la tarea específica con el usuario

### Durante el Trabajo
1. Commits frecuentes con mensajes descriptivos en español
2. Mantén el build funcionando (`npm run build` debe pasar)
3. Actualiza documentación si haces cambios estructurales
4. Usa conventional commits siempre

### Final de Tarea
1. Verifica que `npm run build` pasa sin errores
2. Actualiza `docs/STATUS.md` con:
   - Tareas completadas con checkmarks
   - Problemas encontrados
   - Próximos pasos
3. Actualiza `docs/CHANGELOG.md` si hay cambios significativos
4. Commit de documentación: `docs: actualiza progreso de [TAREA]`

---

## 🎨 Guía de Estilos

### Componentes
```typescript
// Header del archivo
import { useState } from "react";
import { Button } from "@/components/ui/button";
import { heroContent } from "@/config/content";
import type { HeroProps } from "@/types";

// Comentarios solo si añaden valor
export function Hero() {
  // Estado local
  const [isOpen, setIsOpen] = useState(false);
  
  // Handlers
  const handleClick = () => {
    setIsOpen(!isOpen);
  };
  
  // Render
  return (
    <section className="...">
      {/* Contenido */}
    </section>
  );
}
```

### Configuración
```typescript
// config/content.ts
export const heroContent = {
  title: "Título del Hero",
  description: "Descripción del hero",
  ctaText: "Texto del botón"
} as const;

// ✅ Usar 'as const' para type inference
```

### Tipos
```typescript
// types/index.ts
export interface Service {
  id: string;
  title: string;
  description: string;
  icon: string;
}

// ✅ Interfaces descriptivas
// ✅ Propiedades requeridas por defecto
// ❌ NO usar 'any' o 'unknown' sin razón
```

---

## 🔍 Testing y Verificación

### Antes de Commit
```bash
# 1. Build debe pasar
npm run build

# 2. Dev server debe funcionar
npm run dev

# 3. No errores de TypeScript
# (si hay, aparecerán en el build)
```

### Checklist de Calidad
- [ ] TypeScript compila sin errores
- [ ] Build de Vite exitoso
- [ ] Imports correctos (con @/)
- [ ] Componentes renderizan correctamente
- [ ] Responsive funciona (mobile, tablet, desktop)
- [ ] No console.errors en navegador
- [ ] Commit message en español con conventional format
- [ ] Documentación actualizada si corresponde

---

## 📚 Recursos de Referencia

### Documentación del Proyecto
- `docs/STATUS.md` - **LEE PRIMERO**
- `docs/00-PLAN-MAESTRO.md` - Visión general
- `docs/CHANGELOG.md` - Historial de cambios
- `docs/BACKEND-INTEGRATION.md` - Plan de integración backend

### Documentación Externa
- [React 19 Docs](https://react.dev/)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)
- [Tailwind CSS v3](https://tailwindcss.com/docs)
- [Vite Guide](https://vite.dev/guide/)
- [shadcn/ui](https://ui.shadcn.com/)
- [Conventional Commits](https://www.conventionalcommits.org/)

---

## 💬 Comunicación con el Usuario

### Siempre Preguntar Antes de:
- Instalar/cambiar dependencias
- Eliminar archivos o código
- Modificar configuraciones (tailwind, vite, tsconfig)
- Crear nuevas estructuras de carpetas
- Refactorizar componentes enteros
- Cambiar nombres de archivos importantes

### Reportar Siempre:
- Errores encontrados durante el trabajo
- Warnings de TypeScript/ESLint
- Problemas de build
- Mejoras potenciales detectadas
- Tareas completadas con resumen

### Formato de Respuestas
```markdown
✅ **Tarea completada:** [descripción breve]

**Cambios realizados:**
1. Archivo X: cambio Y
2. Archivo Z: cambio W

**Verificación:**
- Build: ✅ Exitoso
- TypeScript: ✅ Sin errores
- Tests: ⏳ Pendiente

**Próximos pasos sugeridos:**
- [ ] Tarea siguiente A
- [ ] Tarea siguiente B
```

---

## 🎯 Fase Actual: Fase 3 - Documentación

### Objetivo
Documentar completamente el código para facilitar mantenimiento y reutilización.

### Tareas Pendientes
1. **JSDoc en componentes** (0/7):
   - [ ] Navbar.tsx
   - [ ] Hero.tsx
   - [ ] Services.tsx
   - [ ] Gallery.tsx
   - [ ] About.tsx
   - [ ] Contact.tsx
   - [ ] Footer.tsx

2. **Documentación de proyecto:**
   - [ ] README.md completo
   - [ ] CUSTOMIZATION.md (guía para adaptar a otros negocios)
   - [ ] STRUCTURE.md (explicación de arquitectura)

3. **Guías de uso:**
   - [ ] Cómo personalizar colores
   - [ ] Cómo cambiar contenido
   - [ ] Cómo agregar secciones

### Ejemplo de JSDoc
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

---

## ⚠️ Situaciones Especiales

### Si Encuentras Código Legacy
1. No lo elimines sin consultar
2. Comenta con `// TODO: revisar si se usa` o `// LEGACY: razón`
3. Reporta en siguiente actualización de STATUS.md

### Si el Build Falla
1. Lee el error completo
2. Identifica el archivo y línea
3. Verifica imports y tipos
4. Si no puedes resolver, reporta con contexto completo

### Si Hay Conflictos de Merge
1. **NO resuelvas automáticamente**
2. Reporta al usuario con archivos afectados
3. Espera instrucciones

---

## 📌 Recordatorios Finales

1. ✅ **Commits en español** con conventional commits
2. ✅ **Lee STATUS.md** antes de empezar
3. ✅ **Pregunta antes de cambios grandes**
4. ✅ **Mantén el build funcionando**
5. ✅ **Actualiza documentación**
6. ✅ **Usa path aliases (@/)**
7. ✅ **Solo 6 componentes UI activos**
8. ✅ **Configuración centralizada en config/**

---

**¡Gracias por seguir estas convenciones!** Ayudan a mantener el proyecto organizado y facilitan el trabajo colaborativo con otros agentes de IA y desarrolladores.

---

_Última actualización: 2025-11-09_  
_Próxima revisión: Al completar Fase 3_
