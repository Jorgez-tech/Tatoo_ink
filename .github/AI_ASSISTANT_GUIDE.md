# 🤖 Guía Rápida para Asistentes de IA

Este documento es una guía rápida de referencia para modelos de asistencia de código que trabajen en este proyecto.

---

## ⚡ Inicio Rápido (30 segundos)

1. **Lee [CONTEXT.md](./CONTEXT.md)** - Contexto completo del proyecto
2. **Revisa [NEXT-STEPS.md](../../docs/NEXT-STEPS.md)** - Estado actual
3. **Consulta [COMMIT_CONVENTION.md](./COMMIT_CONVENTION.md)** - Antes de commitear
4. **Usa [BRANCH_STRATEGY.md](./BRANCH_STRATEGY.md)** - Al crear ramas

---

## 📋 Reglas Críticas

### ✅ SIEMPRE Hacer
- ✅ Crear rama para cambios relevantes (`feature/`, `fix/`, `refactor/`, etc.)
- ✅ Usar commits convencionales en español
- ✅ Actualizar `docs/NEXT-STEPS.md` al completar tareas importantes
- ✅ Seguir nomenclatura del proyecto (PascalCase para componentes)
- ✅ Usar TypeScript con tipos explícitos
- ✅ Usar Tailwind CSS (no archivos CSS adicionales)

### ❌ NUNCA Hacer
- ❌ Commitear directamente en `main` o `develop`
- ❌ Usar mensajes de commit genéricos ("cambios", "fix")
- ❌ Eliminar código sin verificar que no se use
- ❌ Crear archivos CSS adicionales (usar Tailwind)
- ❌ Usar `any` en TypeScript (preferir tipos explícitos)

---

## 🎯 Estado Actual del Proyecto

**Estado:** Ver `docs/NEXT-STEPS.md`

### En Progreso
- ⏳ Eliminar 40 componentes UI no utilizados
- ⏳ Reorganizar estructura de carpetas

### Componentes UI Utilizados (solo estos)
- `button.tsx`
- `card.tsx`
- `input.tsx`
- `textarea.tsx`
- `label.tsx`
- `ImageWithFallback.tsx` (está en `components/figma/`, debe moverse)

---

## 📝 Ejemplos de Uso

### Crear Rama y Hacer Cambios

```bash
# Crear rama
git checkout develop
git pull origin develop
git checkout -b chore/eliminar-componentes-no-usados

# Hacer cambios y commitear
git add .
git commit -m "chore(ui): eliminar componente accordion no utilizado"

# Push
git push origin chore/eliminar-componentes-no-usados
```

### Formato de Commits

```
✅ BUENO:
feat(contact): agregar validación de formulario
fix(navbar): corregir menú móvil que no se cierra
chore(ui): eliminar componentes no utilizados

❌ MALO:
cambios
fix bug
feat: nueva funcionalidad (sin ámbito)
```

---

## 🔍 Información Rápida

### Estructura de Componentes
```
src/components/
├── layout/        # Navbar, Footer
├── sections/      # Hero, Services, Gallery, About, Contact
├── ui/            # Componentes UI (solo los utilizados)
└── shared/        # Componentes compartidos
```

### Tecnologías
- **React 18+** con TypeScript
- **Vite 7** como build tool
- **Tailwind CSS v3.4.17** para estilos
- **Radix UI** para componentes base
- **Lucide React** para iconos

### Convenciones
- **Componentes:** PascalCase (`Navbar.tsx`)
- **Utilidades:** camelCase (`utils.ts`)
- **Hooks:** camelCase con `use` (`use-mobile.ts`)
- **Tipos:** PascalCase (`ContactForm`)

---

## 📚 Documentos de Referencia

| Documento | Propósito |
|-----------|-----------|
| [CONTEXT.md](./CONTEXT.md) | Contexto completo del proyecto |
| [WORKFLOW.md](./WORKFLOW.md) | Flujo de trabajo detallado |
| [COMMIT_CONVENTION.md](./COMMIT_CONVENTION.md) | Convención de commits |
| [BRANCH_STRATEGY.md](./BRANCH_STRATEGY.md) | Estrategia de ramas |
| [../../docs/NEXT-STEPS.md](../../docs/NEXT-STEPS.md) | Estado actual y próximos pasos |

---

## 🆘 Resolución de Problemas

### ¿Qué rama debo usar?
- `feature/*` - Nueva funcionalidad
- `fix/*` - Corregir bug
- `refactor/*` - Mejorar código
- `chore/*` - Mantenimiento
- `docs/*` - Documentación
- `perf/*` - Optimización

### ¿Cómo nombro mi commit?
```
tipo(ámbito): descripción breve
```
Ejemplo: `feat(contact): agregar validación de email`

### ¿Dónde va este componente?
- Layout → `src/components/layout/`
- Secciones → `src/components/sections/`
- UI reutilizable → `src/components/ui/`
- Compartido → `src/components/shared/`

---

## 💡 Tips

1. **Siempre verificar** que un componente no se use antes de eliminarlo
2. **Usar grep** para buscar referencias: `grep -r "ComponentName" src/`
3. **Mantener consistencia** con el código existente
4. **Documentar decisiones** importantes en los commits
5. **Actualizar docs/NEXT-STEPS.md** al completar tareas relevantes

---

**Última actualización:** 2025-01-27

