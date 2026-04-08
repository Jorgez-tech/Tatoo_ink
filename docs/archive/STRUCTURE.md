# Estructura del Proyecto

Documentacion detallada de la arquitectura y organizacion del proyecto Ink Studio.

## Tabla de Contenidos

1. [Vision General](#vision-general)
2. [Estructura de Carpetas](#estructura-de-carpetas)
3. [Componentes](#componentes)
4. [Configuracion](#configuracion)
5. [Hooks](#hooks)
6. [Utilidades](#utilidades)
7. [Estilos](#estilos)
8. [Tipos](#tipos)
9. [Flujo de Datos](#flujo-de-datos)

---

## Vision General

El proyecto sigue una arquitectura modular basada en componentes React con separacion clara entre presentacion y datos.

**Principios de diseno:**

- Separacion de datos y presentacion
- Componentes reutilizables y documentados
- Configuracion centralizada
- Type-safety con TypeScript
- Responsive-first design

---

## Estructura de Carpetas

```text
tatoo_ink/
|-- docs/                           # Documentacion del proyecto
|   |-- NEXT-STEPS.md              # Estado actual y proximos pasos
|   |-- README.md                  # Indice de documentacion
|   |-- STRUCTURE.md               # Este archivo
|   |-- CUSTOMIZATION.md
|   |-- DEPLOYMENT.md
|   |-- BACKEND-INTEGRATION.md
|   +-- BACKEND-QUICKSTART.md
|-- public/                         # Archivos estaticos
|   |-- site.webmanifest
|   +-- vite.svg
|-- src/
|   |-- components/                 # Componentes React
|   |   |-- layout/                 # Componentes de estructura
|   |   |   |-- Navbar.tsx
|   |   |   +-- Footer.tsx
|   |   |-- sections/               # Secciones de la landing
|   |   |   |-- Hero.tsx
|   |   |   |-- Services.tsx
|   |   |   |-- Gallery.tsx
|   |   |   |-- About.tsx
|   |   |   +-- Contact.tsx
|   |   |-- ui/                     # Componentes UI base
|   |   |   |-- button.tsx
|   |   |   |-- card.tsx
|   |   |   |-- input.tsx
|   |   |   |-- textarea.tsx
|   |   |   |-- label.tsx
|   |   |   +-- ImageWithFallback.tsx
|   |   +-- shared/                 # Componentes compartidos (futuro)
|   |-- config/                     # Configuracion centralizada
|   |   |-- business-info.ts        # Datos del negocio
|   |   |-- content.ts              # Textos de secciones
|   |   |-- images.ts               # Rutas de imagenes
|   |   |-- navigation.ts           # Menu y navegacion
|   |   |-- services.ts             # Servicios ofrecidos
|   |   +-- api.ts                  # Configuracion de backend
|   |-- hooks/                      # Custom hooks
|   |   +-- use-active-section.ts   # Scroll spy
|   |-- lib/                        # Utilidades
|   |   +-- utils.ts                # Helpers (cn)
|   |-- styles/                     # Estilos globales
|   |   +-- globals.css
|   |-- types/                      # Tipos TypeScript
|   |   +-- index.ts
|   |-- App.tsx                     # Componente raiz
|   |-- main.tsx                    # Entry point
|   +-- index.css                   # Imports de Tailwind
|-- .gitignore
|-- eslint.config.js
|-- index.html
|-- package.json
|-- postcss.config.js
|-- tailwind.config.js
|-- tsconfig.json
|-- tsconfig.app.json
|-- tsconfig.node.json
+-- vite.config.ts
```

---

## Componentes

### Layout (`components/layout/`)

Componentes estructurales que aparecen en todas las paginas.

#### Navbar.tsx

- Navegacion fija en la parte superior
- Menu responsive (desktop/movil)
- Scroll spy (destaca seccion activa)
- Smooth scroll al hacer clic
- Efecto de backdrop blur al hacer scroll

**Props:** Ninguna (usa configuracion)

**Dependencias:**

- `config/navigation.ts` - Items del menu
- `config/business-info.ts` - Nombre del negocio
- `hooks/use-active-section.ts` - Deteccion de seccion activa

#### Footer.tsx

- Pie de pagina con informacion del negocio
- Enlaces rapidos
- Redes sociales
- Copyright dinamico

**Props:** Ninguna (usa configuracion)

**Dependencias:**

- `config/business-info.ts` - Datos del negocio
- `config/content.ts` - Textos del footer
- `config/navigation.ts` - Enlaces rapidos

---

### Sections (`components/sections/`)

Secciones principales de la landing page.

#### Hero.tsx

- Seccion principal con imagen de fondo
- Titulo y descripcion destacados
- Botones de CTA (primario y secundario)
- Indicador de scroll animado

**Props:** Ninguna (usa configuracion)

**Dependencias:**

- `config/content.ts` - Textos del hero
- `config/images.ts` - Imagen de fondo

#### Services.tsx

- Grid de servicios ofrecidos
- Tarjetas con icono, titulo y descripcion
- Responsive (1->2->4 columnas)

**Props:** Ninguna (usa configuracion)

**Dependencias:**

- `config/services.ts` - Lista de servicios
- `config/content.ts` - Titulo y descripcion de seccion

#### Gallery.tsx

- Grid de imagenes responsive
- Lightbox interactivo
- Navegacion por teclado (flechas, ESC)
- Lazy loading con placeholders

**Props:** Ninguna (usa configuracion)

**Dependencias:**

- `config/images.ts` - Imagenes de la galeria
- `config/content.ts` - Titulo y descripcion

**Caracteristicas:**

- Navegacion circular (primera <-> ultima)
- Contador de imagenes
- Cierre con backdrop o ESC
- Prevencion de scroll del body

#### About.tsx

- Informacion del estudio
- Estadisticas destacadas
- Imagen corporativa con decoracion

**Props:** Ninguna (usa configuracion)

**Dependencias:**

- `config/business-info.ts` - Stats y datos
- `config/content.ts` - Textos de la seccion
- `config/images.ts` - Imagen about

#### Contact.tsx

- Formulario de contacto validado
- Tarjetas con informacion de contacto
- Estados de loading/success/error
- Integracion con backend

**Props:** Ninguna (usa configuracion)

**Dependencias:**

- `config/business-info.ts` - Datos de contacto
- `config/content.ts` - Textos del formulario
- `config/api.ts` - Configuracion de backend
- `react-hook-form` - Validacion

**Validaciones:**

- Nombre: minimo 2 caracteres
- Email: formato valido
- Telefono: requerido, formato numerico
- Mensaje: minimo 10 caracteres
- Solicitud de cita: checkbox (true/false)

---

### UI (`components/ui/`)

Componentes base reutilizables con estilos consistentes.

#### button.tsx

- Variantes: default, destructive, outline, secondary, ghost, link
- Tamanos: default, sm, lg, icon
- Soporte para `asChild` (Radix Slot)

#### card.tsx

- Componentes: Card, CardHeader, CardTitle, CardDescription, CardContent, CardFooter, CardAction
- Sistema de composicion flexible

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
- Asociacion automatica con inputs

#### ImageWithFallback.tsx

- Lazy loading por defecto
- Placeholder animado mientras carga
- Fallback SVG en caso de error
- Transicion suave al cargar

---

## Configuracion

Archivos de configuracion centralizados en `src/config/`.

### business-info.ts

Informacion del negocio y datos de contacto.

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

Rutas de todas las imagenes.

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

Estructura del menu de navegacion.

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

Configuracion de backend.

```typescript
export const API_BASE_URL: string;
export const API_ENDPOINTS: {
  contact: string;
  gallery: string;
};

export function getApiUrl(endpoint: keyof typeof API_ENDPOINTS): string;
```

---

## Hooks

### use-active-section.ts

Hook personalizado para detectar la seccion activa durante el scroll.

**Uso:**

```typescript
const activeSection = useActiveSection(["home", "servicios", "galeria"]);
```

**Funcionamiento:**

- Escucha el evento `scroll` del window
- Calcula la posicion actual (`window.scrollY`) con un offset
- Marca como activa la primera seccion cuyo rango vertical contenga el scroll

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
- Clases de utilidad (animation-delay-*)
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

### 1. Configuracion -> Componentes

```
config/business-info.ts
config/content.ts
config/images.ts
config/navigation.ts
config/services.ts
    |
    v
components/sections/*
components/layout/*
```

Los componentes importan directamente desde los archivos de configuracion.

### 2. Formulario de Contacto

```
Usuario completa formulario
    |
    v
react-hook-form valida datos
    |
    v
onSubmit() en Contact.tsx
    |
    v
config/api.ts (URL base + endpoints)

    |
    v
Backend ASP.NET Core (produccion)
    |
    v
Respuesta -> Estado (success/error)
```

### 3. Scroll Spy

```
Usuario hace scroll
    |
    v
window.scrollY + offset determina seccion
    |
    v
use-active-section actualiza estado
    |
    v
Navbar destaca enlace activo
```

### 4. Lightbox de Galeria

```
Usuario hace clic en imagen
    |
    v
setSelectedImage(index)
    |
    v
Modal se abre con imagen
    |
    v
Navegacion: flechas/teclado
    |
    v
Cierre: ESC/backdrop/boton X
```

---

## Configuracion de Build

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
      // Configuracion personalizada
    },
  },
};
```

---

## Dependencias Principales

**Produccion:**

- `react` + `react-dom` - Framework
- `@radix-ui/*` - Componentes UI accesibles
- `lucide-react` - Iconos
- `react-hook-form` - Validacion de formularios
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
npm run build    # Build de produccion
npm run preview  # Preview de build
npm run lint     # Linting
```

---

## Convenciones

### Nombres de Archivos

- Componentes: `PascalCase.tsx`
- Configuracion: `kebab-case.ts`
- Hooks: `use-kebab-case.ts`
- Utilidades: `kebab-case.ts`

### Imports

```typescript
// Externos primero
import { useState } from "react";
import { Button } from "@/components/ui/button";

// Configuracion
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

- `docs/README.md` - Índice de documentación
- `docs/ARCHITECTURE.md` - Arquitectura del sistema
- `docs/CUSTOMIZATION.md` - Guía de personalización
- `docs/BACKEND-INTEGRATION.md` - Integración con backend

## Prohibicion de emojis

**NOTA:** Por decision de estilo y compatibilidad, los emojis estan prohibidos en todo el proyecto y documentacion. Utiliza solo texto plano y simbolos ASCII.
