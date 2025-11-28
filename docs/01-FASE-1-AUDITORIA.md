# FASE 1: Auditoria y Limpieza

**Estado:** [EN PROGRESO]
**Fecha inicio:** 2025-11-05

---

## 1.1 Identificar y Eliminar Codigo No Utilizado

### Componentes UI (shadcn/ui)
**Total:** 47 archivos en `src/components/ui/`

#### Componentes Utilizados (7)
- [x] `button.tsx` - Navbar, Hero, Contact
- [x] `card.tsx` - Services, Contact
- [x] `input.tsx` - Contact
- [x] `textarea.tsx` - Contact
- [x] `label.tsx` - Contact
- [x] `utils.ts` - Utilidad para clsx/tailwind-merge
- [x] `use-mobile.ts` - Hook para deteccion movil

#### Componentes NO Utilizados (40)
Pendiente de eliminar:
- accordion.tsx
- alert-dialog.tsx
- alert.tsx
- aspect-ratio.tsx
- avatar.tsx
- badge.tsx
- breadcrumb.tsx
- calendar.tsx
- carousel.tsx
- chart.tsx
- checkbox.tsx
- collapsible.tsx
- command.tsx
- context-menu.tsx
- dialog.tsx
- drawer.tsx
- dropdown-menu.tsx
- form.tsx
- hover-card.tsx
- input-otp.tsx
- menubar.tsx
- navigation-menu.tsx
- pagination.tsx
- popover.tsx
- progress.tsx
- radio-group.tsx
- resizable.tsx
- scroll-area.tsx
- select.tsx
- separator.tsx
- sheet.tsx
- sidebar.tsx
- skeleton.tsx
- slider.tsx
- sonner.tsx
- switch.tsx
- table.tsx
- tabs.tsx
- toggle-group.tsx
- toggle.tsx
- tooltip.tsx

**Ahorro estimado:** ~2.5 MB de codigo no utilizado

---

## 1.2 Optimizar Estructura de Carpetas

### Estructura Actual
```
src/
|-- components/
|   |-- About.tsx
|   |-- Contact.tsx
|   |-- Footer.tsx
|   |-- Gallery.tsx
|   |-- Hero.tsx
|   |-- Navbar.tsx
|   |-- Services.tsx
|   |-- figma/
|   |   +-- ImageWithFallback.tsx
|   +-- ui/ (47 archivos)
|-- styles/
|   +-- globals.css
|-- assets/
|   +-- react.svg
|-- App.css (ELIMINAR)
|-- App.tsx
|-- index.css
+-- main.tsx
```

### Estructura Propuesta
```
src/
|-- components/
|   |-- layout/
|   |   |-- Navbar.tsx
|   |   +-- Footer.tsx
|   |-- sections/
|   |   |-- Hero.tsx
|   |   |-- Services.tsx
|   |   |-- Gallery.tsx
|   |   |-- About.tsx
|   |   +-- Contact.tsx
|   |-- ui/
|   |   |-- Button.tsx (renombrado de button.tsx)
|   |   |-- Card.tsx
|   |   |-- Input.tsx
|   |   |-- Textarea.tsx
|   |   |-- Label.tsx
|   |   +-- ImageWithFallback.tsx (movido desde figma/)
|   +-- shared/
|       +-- (componentes reutilizables futuros)
|-- config/
|   |-- business-info.ts (CREAR)
|   |-- content.ts (CREAR)
|   |-- images.ts (CREAR)
|   +-- navigation.ts (CREAR)
|-- hooks/
|   +-- use-mobile.ts (mover desde ui/)
|-- lib/
|   +-- utils.ts (mover desde ui/)
|-- styles/
|   +-- globals.css
|-- types/
|   +-- index.ts (CREAR)
|-- App.tsx
|-- index.css
+-- main.tsx
```

**Eliminaciones:**
- [ELIMINAR] `App.css` - No se usa, estilos en globals.css
- [ELIMINAR] `assets/react.svg` - Logo de demo
- [ELIMINAR] `components/figma/` - Carpeta innecesaria

---

## 1.3 Limpiar Estilos

### Archivos de Estilos Actuales
1. **`index.css`** (5 lineas) - Imports Tailwind + globals.css [MANTENER]
2. **`App.css`** (40 lineas) - CSS de demo Vite [ELIMINAR]
3. **`globals.css`** (200+ lineas) - Variables CSS + Tailwind config [MANTENER]

### Acciones
- [x] Eliminar `App.css`
- [x] Remover import de `App.css` en `App.tsx`
- [ ] Documentar variables CSS en `globals.css`
- [ ] Optimizar configuracion de Tailwind

---

## Progreso Fase 1

- [x] 1.1 Auditoria de componentes [OK]
- [ ] 1.2 Reorganizacion de carpetas [PENDIENTE]
- [ ] 1.3 Limpieza de estilos [PENDIENTE]

**Siguiente:** Eliminar componentes UI no utilizados y reorganizar estructura
