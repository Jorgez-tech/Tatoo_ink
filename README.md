# 🎨 Ink Studio - Landing Page

Landing page profesional para estudio de tatuajes, construida con React + TypeScript + Tailwind CSS.

## 🚀 Stack Tecnológico

- **Framework:** React 19 + Vite
- **Lenguaje:** TypeScript
- **Estilos:** Tailwind CSS v3 + utilidades personalizadas
- **Componentes UI:** Radix UI + Lucide Icons
- **Validación:** React Hook Form
- **Iconos:** Lucide React

## ✅ Requisitos Previos

- Node.js 18 LTS o superior
- npm 9+ (instalado junto con Node)

## 📦 Instalación

```bash
npm install
```

## 🔧 Desarrollo

```bash
npm run dev
```

Abre [http://localhost:5173](http://localhost:5173) para ver la aplicación en modo desarrollo.

## 🏗️ Build y Deploy

```bash
npm run build
npm run preview
```

El directorio `dist/` contiene los artefactos listos para despliegue. En escenarios con backend ASP.NET Core, publica el contenido de `dist/` dentro de `wwwroot/` o configura proxy inverso según necesidades.

## 📁 Estructura del Proyecto

```text
src/
├── components/
│   ├── layout/      # Navbar, Footer
│   ├── sections/    # Hero, Services, Gallery, About, Contact
│   ├── ui/          # Button, Card, Input, Textarea, Label, ImageWithFallback
│   └── shared/      # Componentes compartidos futuros
├── config/          # Configuración centralizada
│   ├── business-info.ts    # Datos del negocio
│   ├── content.ts          # Textos de secciones
│   ├── images.ts           # Rutas de imágenes
│   ├── navigation.ts       # Menú y navegación
│   ├── services.ts         # Servicios ofrecidos
│   └── api.ts              # Configuración de backend
├── hooks/           # useActiveSection
├── lib/             # utils.ts (cn helper)
├── styles/          # globals.css
└── types/           # index.ts (interfaces TypeScript)
```

Consulta `docs/STRUCTURE.md` para detalles completos.

## 🎨 Personalización

Para adaptar esta landing a un nuevo cliente:

1. **Información del negocio:** Edita `src/config/business-info.ts`
2. **Contenido de secciones:** Edita `src/config/content.ts`
3. **Servicios:** Edita `src/config/services.ts`
4. **Imágenes:** Actualiza rutas en `src/config/images.ts`
5. **Colores y estilos:** Modifica variables CSS en `src/styles/globals.css`

Ver `docs/CUSTOMIZATION.md` para guía detallada.

## 🧩 Características

- ✅ Diseño responsive (móvil, tablet, desktop)
- ✅ Navegación con scroll spy (sección activa destacada)
- ✅ Smooth scroll global
- ✅ Lazy loading de imágenes con placeholders
- ✅ Lightbox interactivo con navegación por teclado
- ✅ Formulario validado con React Hook Form
- ✅ Animaciones fadeIn/fadeInUp
- ✅ Preparado para backend ASP.NET Core
- ✅ Modo mock para desarrollo sin backend
- ✅ Componentes documentados con JSDoc

## 🤝 Contribución

1. Crea una rama feature desde `main` siguiendo el formato `feature/<nombre>`
2. Usa commits convencionales (`feat:`, `fix:`, `docs:`, `refactor:`, etc.)
3. Ejecuta `npm run build` antes de abrir un PR

## 📚 Documentación

- `docs/STRUCTURE.md` - Arquitectura del proyecto
- `docs/CUSTOMIZATION.md` - Guía de personalización
- `docs/BACKEND-INTEGRATION.md` - Integración con ASP.NET Core
- `docs/00-PLAN-MAESTRO.md` - Plan general del proyecto

## 📝 Licencia

Este proyecto es un prototipo/demo para uso interno y clientes.
