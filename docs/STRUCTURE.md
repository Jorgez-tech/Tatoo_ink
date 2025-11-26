# Estructura del Proyecto

Documentación detallada de la arquitectura y organización del proyecto Ink Studio.

## Tabla de Contenidos

1. [Visión General](#visión-general)
2. [Estructura de Carpetas](#estructura-de-carpetas)
3. [Componentes](#componentes)
4. [Configuración](#configuración)
5. [Hooks](#hooks)
6. [Utilidades](#utilidades)
7. [Estilos](#estilos)
8. [Tipos](#tipos)
9. [Flujo de Datos](#flujo-de-datos)

---

## Visión General

El proyecto sigue una arquitectura modular basada en componentes React con separación clara entre presentación y datos.

**Principios de diseño:**

- Separación de datos y presentación
- Componentes reutilizables y documentados
- Configuración centralizada
- Type-safety con TypeScript
- Responsive-first design

---

## Estructura de Carpetas

```
tatoo_ink.client/
├── docs/                           # Documentación del proyecto
│   ├── 00-PLAN-MAESTRO.md
│   ├── 01-FASE-1-AUDITORIA.md
│   ├── 02-FASE-2-OPTIMIZACION.md
│   ├── 03-FASE-3-DOCUMENTACION.md
│   ├── 04-FASE-4-FINALIZACION.md
│   ├── BACKEND-INTEGRATION.md
│   ├── CHANGELOG.md
│   ├── CUSTOMIZATION.md
│   ├── STATUS.md
│   └── STRUCTURE.md               # Este archivo
├── public/                         # Archivos estáticos
│   ├── site.webmanifest
│   └── vite.svg
├── src/
│   ├── components/                 # Componentes React
│   │   ├── layout/                 # Componentes de estructura
│   │   │   ├── Navbar.tsx
│   │   │   └── Footer.tsx
│   │   ├── sections/               # Secciones de la landing
│   │   │   ├── Hero.tsx
│   │   │   ├── Services.tsx
│   │   │   ├── Gallery.tsx
│   │   │   ├── About.tsx
│   │   │   └── Contact.tsx
│   │   ├── ui/                     # Componentes UI base
│   │   │   ├── button.tsx
│   │   │   ├── card.tsx
│   │   │   ├── input.tsx
│   │   │   ├── textarea.tsx
│   │   │   ├── label.tsx
│   │   │   └── ImageWithFallback.tsx
│   │   └── shared/                 # Componentes compartidos (futuro)
│   ├── config/                     # Configuración centralizada
│   │   ├── business-info.ts        # Datos del negocio
│   │   ├── content.ts              # Textos de secciones
│   │   ├── images.ts               # Rutas de imágenes
│   │   ├── navigation.ts           # Menú y navegación
│   │   ├── services.ts             # Servicios ofrecidos
│   │   └── api.ts                  # Configuración de backend
│   ├── hooks/                      # Custom hooks
│   │   └── use-active-section.ts   # Scroll spy
│   ├── lib/                        # Utilidades
│   │   └── utils.ts                # Helpers (cn)
│   ├── styles/                     # Estilos globales
│   │   └── globals.css
│   ├── types/                      # Tipos TypeScript
│   │   └── index.ts
│   ├── App.tsx                     # Componente raíz
│   ├── main.tsx                    # Entry point
│   └── index.css                   # Imports de Tailwind
├── .gitignore
├── eslint.config.js
├── index.html
├── package.json
├── postcss.config.js
├── tailwind.config.js
├── tsconfig.json
├── tsconfig.app.json
├── tsconfig.node.json
└── vite.config.ts
```

---

## Componentes

### Layout (`components/layout/`)

Componentes estructurales que aparecen en todas las páginas.

#### Navbar.tsx

- Navegación fija en la parte superior
- Menú responsive (desktop/móvil)
- Scroll spy (destaca sección activa)
- Smooth scroll al hacer clic
- Efecto de backdrop blur al hacer scroll

**Props:** Ninguna (usa configuración)

**Dependencias:**

- `config/navigation.ts` - Items del menú
- `config/business-info.ts` - Nombre del negocio
- `hooks/use-active-section.ts` - Detección de sección activa

#### Footer.tsx

- Pie de página con información del negocio
- Enlaces rápidos
- Redes sociales
- Copyright dinámico

**Props:** Ninguna (usa configuración)

**Dependencias:**

- `config/business-info.ts` - Datos del negocio
- `config/content.ts` - Textos del footer
- `config/navigation.ts` - Enlaces rápidos

---

### Sections (`components/sections/`)

Secciones principales de la landing page.

#### Hero.tsx

- Sección principal con imagen de fondo
- Título y descripción destacados
- Botones de CTA (primario y secundario)
- Indicador de scroll animado

**Props:** Ninguna (usa configuración)

**Dependencias:**

- `config/content.ts` - Textos del hero
- `config/images.ts` - Imagen de fondo

#### Services.tsx

- Grid de servicios ofrecidos
- Tarjetas con icono, título y descripción
- Responsive (1→2→4 columnas)

**Props:** Ninguna (usa configuración)

**Dependencias:**

- `config/services.ts` - Lista de servicios
- `config/content.ts` - Título y descripción de sección

#### Gallery.tsx

- Grid de imágenes responsive
- Lightbox interactivo
- Navegación por teclado (flechas, ESC)
- Lazy loading con placeholders

**Props:** Ninguna (usa configuración)

**Dependencias:**

- `config/images.ts` - Imágenes de la galería
- `config/content.ts` - Título y descripción

**Características:**

- Navegación circular (primera ↔ última)
- Contador de imágenes
- Cierre con backdrop o ESC
- Prevención de scroll del body

#### About.tsx

- Información del estudio
- Estadísticas destacadas
- Imagen corporativa con decoración

**Props:** Ninguna (usa configuración)

**Dependencias:**

- `config/business-info.ts` - Stats y datos
- `config/content.ts` - Textos de la sección
- `config/images.ts` - Imagen about

#### Contact.tsx

- Formulario de contacto validado
- Tarjetas con información de contacto
- Estados de loading/success/error
- Integración con backend o modo mock

**Props:** Ninguna (usa configuración)

**Dependencias:**

- `config/business-info.ts` - Datos de contacto
- `config/content.ts` - Textos del formulario
- `config/api.ts` - Configuración de backend
- `react-hook-form` - Validación

**Validaciones:**

- Nombre: mínimo 2 caracteres
- Email: formato válido
- Teléfono: formato numérico (opcional)
- Mensaje: mínimo 10 caracteres

---

### UI (`components/ui/`)

Componentes base reutilizables con estilos consistentes.

#### button.tsx

- Variantes: default, destructive, outline, secondary, ghost, link
- Tamaños: default, sm, lg, icon
- Soporte para `asChild` (Radix Slot)

#### card.tsx

- Componentes: Card, CardHeader, CardTitle, CardDescription, CardContent, CardFooter, CardAction
- Sistema de composición flexible

#### input.tsx

- Input base con estilos consistentes
- Estados: focus, disabled, invalid
- Soporte para type="file"

#### textarea.tsx

- Textarea con estilos consistentes
- Auto-resize con `field-sizing-content`
- Estados: focus, disabled, invalid

#### label.tsx

- Label accesible basado en Radix UI
- Asociación automática con inputs

#### ImageWithFallback.tsx

- Lazy loading por defecto
- Placeholder animado mientras carga
- Fallback SVG en caso de error
- Transición suave al cargar

---

## Configuración

Archivos de configuración centralizados en `src/config/`.

### business-info.ts

Información del negocio y datos de contacto.

```typescript
export const businessInfo = {
  name: string
  tagline: string
  description: string
  contact: {
    address: string
    phone: string
    email: string
    hours: string
  }
  social: {
    instagram: string
    facebook: string
    twitter: string
  }
  stats: Array<{
    icon: LucideIcon
    value: string
    label: string
  }>
}

export const contactInfo: Array<{
  icon: LucideIcon
  title: string
  value: string
}>
```

### content.ts

Textos de todas las secciones.

```typescript
export const heroContent: {
  title: string
  description: string
  primaryButton: string
  secondaryButton: string
}

export const servicesContent: {
  title: string
  description: string
}

export const galleryContent: {
  title: string
  description: string
}

export const aboutContent: {
  title: string
  paragraphs: string[]"
}

export const contactContent: {
  title: string
  description: string
  formTitle: string
  formDescription: string
  formFields: { ... }
  submitButton: string
  successMessage: string
}

export const footerContent: {
  description: string
  quickLinksTitle: string
  socialTitle: string
}
```

### images.ts

Rutas de todas las imágenes.

```typescript
export const heroImage: {
  src: string;
  alt: string;
};

export const aboutImage: {
  src: string;
  alt: string;
};

export const galleryImages: Array<{
  src: string;
  alt: string;
}>;
```

### navigation.ts

Estructura del menú de navegación.

```typescript
export const menuItems: Array<{
  label: string;
  href: string;
}>;

export const navbarCtaText: string;
```

### services.ts

Lista de servicios ofrecidos.

```typescript
export const services: Array<{
  icon: LucideIcon;
  title: string;
  description: string;
}>;
```

### api.ts

Configuración de backend y modo mock.

```typescript
export const USE_MOCK_API: boolean;
export const API_BASE_URL: string;
export const API_ENDPOINTS: {
  contact: string;
};

export function getApiUrl(endpoint: string): string;
export function mockApiCall(endpoint: string, data: any): Promise<Response>;
```

---

## Hooks

### use-active-section.ts

Hook personalizado para detectar la sección activa durante el scroll.

**Uso:**

```typescript
const activeSection = useActiveSection(["home", "servicios", "galeria"]);
```

**Funcionamiento:**

- Usa `IntersectionObserver` para detectar secciones visibles
- Actualiza el estado cuando una sección entra en el viewport
- Threshold: 50% de la sección visible

---

## Utilidades

### lib/utils.ts

Funciones helper compartidas.

#### cn()

Combina clases de Tailwind con `clsx` y `tailwind-merge`.

```typescript
cn("text-red-500", "text-blue-500"); // "text-blue-500"
cn("px-4", condition && "py-2"); // "px-4 py-2" o "px-4"
```

---

## Estilos

### globals.css

Estilos globales y variables CSS.

**Contenido:**

- Variables CSS para tema (colores, bordes, etc.)
- Animaciones personalizadas (fadeIn, fadeInUp)
- Clases de utilidad (animation-delay-\*)
- Smooth scroll global
- Reset de estilos base

**Animaciones:**

```css
@keyframes fade-in {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

@keyframes fade-in-up {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
```

**Delays:**

```css
.animation-delay-100 {
  animation-delay: 100ms;
}
.animation-delay-200 {
  animation-delay: 200ms;
}
.animation-delay-300 {
  animation-delay: 300ms;
}
.animation-delay-400 {
  animation-delay: 400ms;
}
.animation-delay-500 {
  animation-delay: 500ms;
}
```

### index.css

Imports de Tailwind CSS.

```css
@import "tailwindcss";
@import "./styles/globals.css";
```

---

## Tipos

### types/index.ts

Definiciones TypeScript compartidas.

```typescript
export interface Service {
  icon: LucideIcon;
  title: string;
  description: string;
}

export interface Stat {
  icon: LucideIcon;
  value: string;
  label: string;
}

export interface GalleryImage {
  src: string;
  alt: string;
}

export interface ContactInfo {
  icon: LucideIcon;
  title: string;
  value: string;
}

export interface MenuItem {
  label: string;
  href: string;
}

export interface ContactFormData {
  name: string;
  email: string;
  phone?: string;
  message: string;
}
```

---

## Flujo de Datos

### 1. Configuración → Componentes

```
config/business-info.ts
config/content.ts
config/images.ts
config/navigation.ts
config/services.ts
    ↓
components/sections/*
components/layout/*
```

Los componentes importan directamente desde los archivos de configuración.

### 2. Formulario de Contacto

```
Usuario completa formulario
    ↓
react-hook-form valida datos
    ↓
onSubmit() en Contact.tsx
    ↓
config/api.ts (mock o real)
    ↓
Backend ASP.NET Core (producción)
    ↓
Respuesta → Estado (success/error)
```

### 3. Scroll Spy

```
Usuario hace scroll
    ↓
IntersectionObserver detecta secciones
    ↓
use-active-section actualiza estado
    ↓
Navbar destaca enlace activo
```

### 4. Lightbox de Galería

```
Usuario hace clic en imagen
    ↓
setSelectedImage(index)
    ↓
Modal se abre con imagen
    ↓
Navegación: flechas/teclado
    ↓
Cierre: ESC/backdrop/botón X
```

---

## Configuración de Build

### Vite (vite.config.ts)

```typescript
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
});
```

**Alias:** `@/` apunta a `src/`

### TypeScript (tsconfig.json)

```json
{
  "compilerOptions": {
    "paths": {
      "@/*": ["./src/*"]
    }
  }
}
```

### Tailwind (tailwind.config.js)

```javascript
module.exports = {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      // Configuración personalizada
    },
  },
};
```

---

## Dependencias Principales

**Producción:**

- `react` + `react-dom` - Framework
- `@radix-ui/*` - Componentes UI accesibles
- `lucide-react` - Iconos
- `react-hook-form` - Validación de formularios
- `class-variance-authority` - Variantes de componentes
- `clsx` + `tailwind-merge` - Utilidades CSS

**Desarrollo:**

- `vite` - Build tool
- `typescript` - Type checking
- `tailwindcss` - Framework CSS
- `eslint` - Linting

---

## Scripts

```bash
npm run dev      # Servidor de desarrollo
npm run build    # Build de producción
npm run preview  # Preview de build
npm run lint     # Linting
```

---

## Convenciones

### Nombres de Archivos

- Componentes: `PascalCase.tsx`
- Configuración: `kebab-case.ts`
- Hooks: `use-kebab-case.ts`
- Utilidades: `kebab-case.ts`

### Imports

```typescript
// Externos primero
import { useState } from "react";
import { Button } from "@/components/ui/button";

// Configuración
import { heroContent } from "@/config/content";

// Tipos
import type { Service } from "@/types";
```

### Componentes

- Usar function declarations
- Documentar con JSDoc
- Props tipadas con TypeScript
- Exportar como named export

---

Para más información, consulta:

- `docs/CUSTOMIZATION.md` - Guía de personalización
- `docs/BACKEND-INTEGRATION.md` - Integración con backend
- `docs/00-PLAN-MAESTRO.md` - Plan general del proyecto

## Prohibición de emojis

**NOTA:** Por decisión de estilo y compatibilidad, los emojis están prohibidos en todo el proyecto y documentación. Utiliza solo texto plano y símbolos ASCII.
