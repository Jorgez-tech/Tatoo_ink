# Contexto del Proyecto para Asistentes de Código

Este documento proporciona contexto esencial sobre el proyecto para ayudar a los modelos de asistencia de código a entender la estructura, convenciones y objetivos del proyecto.

## Información General

**Nombre del Proyecto:** Landing Page System - Ink Studio  
**Tipo:** Landing page profesional para estudio de tatuajes (prototipo/demo)  
**Stack Tecnológico:**
- React 18+ (TypeScript)
- Vite 7
- Tailwind CSS v3.4.17
- Radix UI (componentes base)
- Lucide React (iconos)

**Propósito:** Base reutilizable para futuros clientes. Sistema de landing pages profesionales para pequeños negocios y profesionales en Chile.

---

## Estructura del Proyecto

```
tatoo_ink/
├── .github/              # Instrucciones y workflows
├── docs/                 # Documentación del proyecto
├── src/
│   ├── components/
│   │   ├── layout/      # Componentes de layout (Navbar, Footer)
│   │   ├── sections/    # Secciones principales (Hero, Services, etc.)
│   │   ├── ui/          # Componentes UI reutilizables (shadcn/ui)
│   │   └── shared/      # Componentes compartidos
│   ├── config/          # Archivos de configuración (business-info, content, etc.)
│   ├── hooks/           # React hooks personalizados
│   ├── lib/             # Utilidades y helpers
│   ├── styles/          # Estilos globales
│   ├── types/           # Definiciones de tipos TypeScript
│   ├── App.tsx          # Componente principal
│   └── main.tsx         # Punto de entrada
├── public/              # Archivos estáticos
└── package.json         # Dependencias
```

---

## Convenciones de Código

### TypeScript
- **Siempre** usar TypeScript con tipos explícitos
- Preferir `interface` sobre `type` para objetos
- Usar tipos estrictos, evitar `any`
- Exportar tipos desde `src/types/`

### React
- Usar componentes funcionales con hooks
- Props tipadas con interfaces
- Usar `export function ComponentName()` para componentes
- Mantener componentes pequeños y enfocados

### Estilos
- **Siempre** usar Tailwind CSS (clases utility-first)
- No crear archivos CSS adicionales excepto `globals.css`
- Variables CSS en `globals.css` para valores reutilizables
- Usar design tokens del sistema de diseño

### Nomenclatura
- **Componentes:** PascalCase (`Navbar.tsx`, `Hero.tsx`)
- **Archivos de utilidades:** camelCase (`utils.ts`, `business-info.ts`)
- **Hooks:** camelCase con prefijo `use` (`use-mobile.ts`)
- **Tipos/Interfaces:** PascalCase (`ContactForm`, `ServiceItem`)

---

## Estado del Proyecto

**Fase Actual:** Fase 4 - Finalización  
**Progreso Global:** 95%  
**Versión:** 0.95.0 - Release Candidate

### Completado
- ✅ Configuración base (Vite + React + TypeScript)
- ✅ 7 componentes principales con JSDoc completo
- ✅ Limpieza de componentes UI (40 eliminados, 6 activos)
- ✅ Reorganización de estructura de carpetas
- ✅ Optimización de imágenes (WebP + fallback JPG, 65% reducción)
- ✅ Backend API REST funcional (2 endpoints)
- ✅ 55 tests unitarios y de integración (100% pasando)
- ✅ Documentación consolidada profesional (19 → 8 archivos core)
- ✅ Performance optimizado (Lighthouse 100)
- ✅ Gallery dinámica desde BD
- ✅ Formulario de contacto end-to-end funcional

### Pendiente
- ⏳ Configuración de producción (5% restante)
- ⏳ Variables de entorno para deployment

---

## Flujo de Trabajo

### Ramas
- **`main`**: Código estable y producción-ready
- **`develop`**: Desarrollo activo (rama principal de trabajo)
- **`feature/*`**: Nuevas funcionalidades
- **`fix/*`**: Correcciones de bugs
- **`refactor/*`**: Refactorizaciones
- **`docs/*`**: Cambios en documentación
- **`chore/*`**: Tareas de mantenimiento

### Commits
Usar **Conventional Commits en español**:
- `feat:` Nueva funcionalidad
- `fix:` Corrección de bug
- `refactor:` Refactorización de código
- `docs:` Cambios en documentación
- `style:` Cambios de formato (no afectan funcionalidad)
- `chore:` Tareas de mantenimiento
- `test:` Agregar o modificar tests
- `perf:` Mejoras de rendimiento

**Formato:**
```
tipo(ámbito): descripción breve

Descripción detallada (opcional)

Cuerpo del mensaje explicando el qué y el por qué
```

**Ejemplos:**
- `feat(contact): agregar validación de formulario`
- `fix(navbar): corregir menú móvil que no se cierra`
- `refactor(components): reorganizar estructura de carpetas`
- `chore(deps): actualizar dependencias`

---

## Principios de Desarrollo

### 1. Código Limpio
- Funciones pequeñas y con una sola responsabilidad
- Nombres descriptivos y auto-documentados
- Eliminar código comentado o no utilizado
- Mantener funciones puras cuando sea posible

### 2. Simplicidad
- Priorizar soluciones simples sobre complejas
- Evitar sobre-ingeniería
- Mantener el código entendible para principiantes

### 3. Mantenibilidad
- Separar datos de presentación
- Usar archivos de configuración para contenido
- Documentar decisiones técnicas importantes
- Mantener consistencia con el código existente

### 4. Performance
- Optimizar imágenes antes de usar
- Usar lazy loading cuando sea apropiado
- Minimizar bundle size
- Evitar re-renders innecesarios

---

## Áreas de Atención Especial

### Componentes UI No Utilizados
Actualmente hay **40 componentes UI no utilizados** de shadcn/ui que deben eliminarse en la Fase 1. Solo mantener:
- `button.tsx`
- `card.tsx`
- `input.tsx`
- `textarea.tsx`
- `label.tsx`
- `ImageWithFallback.tsx`

### Imágenes
- Actualmente todas las imágenes son URLs de Unsplash (externa)
- **Objetivo:** Mover a carpeta `public/images/` y optimizar
- Usar componente `ImageWithFallback` para todas las imágenes

### Formulario de Contacto
- Actualmente solo muestra `alert()` al enviar
- **Futuro:** Preparar para integración con backend/API
- Implementar validación robusta antes de conectar backend

### Configuración
- **Objetivo:** Extraer datos hardcodeados a archivos de configuración
- Crear `src/config/business-info.ts`
- Crear `src/config/content.ts`
- Crear `src/config/images.ts`
- Crear `src/config/navigation.ts`

---

## Recursos Importantes

### Documentación del Proyecto
- `docs/README.md` - Índice maestro de documentación
- `docs/CHANGELOG.md` - Historial de cambios y releases
- `docs/GETTING-STARTED.md` - Setup inicial completo
- `docs/DEVELOPMENT-GUIDE.md` - Convenciones, testing, workflow

### Configuración
- `tailwind.config.js` - Configuración de Tailwind CSS
- `vite.config.ts` - Configuración de Vite
- `tsconfig.json` - Configuración de TypeScript

### Guías
- `.github/WORKFLOW.md` - Flujo de trabajo detallado
- `.github/COMMIT_CONVENTION.md` - Convención de commits
- `.github/BRANCH_STRATEGY.md` - Estrategia de ramas

---

## Reglas Importantes

1. **NUNCA** modificar directamente la rama `main` sin PR
2. **SIEMPRE** crear una rama para cambios relevantes
3. **SIEMPRE** usar commits convencionales en español
4. **SIEMPRE** actualizar documentación si cambia la estructura
5. **NUNCA** eliminar código sin verificar que no se use
6. **SIEMPRE** probar cambios localmente antes de commit
7. **SIEMPRE** actualizar `docs/NEXT-STEPS.md` al completar tareas importantes

---

## Contacto y Soporte

**Desarrollador:** Jorge  
**Ubicación:** Chile  
**Proyecto:** Landing Pages para Profesionales y Pequeños Negocios

Para dudas sobre el proyecto, revisar primero:
1. Este documento (`.github/CONTEXT.md`)
2. `docs/NEXT-STEPS.md`
3. `docs/README.md`

---

**Última actualización:** 2025-01-09

