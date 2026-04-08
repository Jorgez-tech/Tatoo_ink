# Estrategia de Ramas

Este documento describe la estrategia completa de ramas para el proyecto, incluyendo nomenclatura, propósito y flujo de trabajo.

---

## Resumen Ejecutivo

El proyecto utiliza un modelo de ramas basado en **Git Flow** adaptado para un proyecto pequeño/mediano:

- **`main`**: Producción estable
- **`develop`**: Desarrollo activo
- **Ramificaciones temporales**: Para features, fixes, refactors, etc.

---

## Ramas Principales (Permanentes)

### `main`
**Propósito:** Código en producción, siempre estable y funcional

**Características:**
- ✅ Código completamente funcional
- ✅ Sin bugs conocidos críticos
- ✅ Todos los tests pasando (cuando se implementen)
- ✅ Documentación actualizada
- ✅ Listo para deployment

**Reglas:**
- ❌ **NUNCA** commitear directamente
- ✅ Solo merge desde `develop` o `hotfix/*`
- ✅ Requiere Pull Request con revisión
- ✅ Protegida en el repositorio
- ✅ Solo merge después de testing

**Tags:**
- Crear tags de versión en cada merge: `v1.0.0`, `v1.1.0`, etc.

### `develop`
**Propósito:** Rama principal de desarrollo, integración continua

**Características:**
- ✅ Código funcional pero puede tener bugs menores
- ✅ Nuevas features en desarrollo
- ✅ Punto de partida para nuevas ramas
- ✅ Código más actualizado que `main`

**Reglas:**
- ✅ Puede recibir merge directo para cambios menores
- ✅ Para cambios significativos, usar PR
- ✅ Mantener actualizada con `main` periódicamente
- ✅ Todas las features se integran aquí primero

---

## Ramas de Trabajo (Temporales)

### Prefijos de Ramas

#### `feature/` - Nuevas Funcionalidades
**Propósito:** Desarrollar nuevas características

**Nomenclatura:**
```
feature/descripcion-corta
feature/nombre-componente-descripcion
```

**Ejemplos:**
- `feature/formulario-validacion`
- `feature/sistema-temas`
- `feature/integracion-cms`
- `feature/galeria-lightbox`
- `feature/navbar-multinivel`

**Flujo:**
1. Crear desde `develop`
2. Desarrollar funcionalidad
3. Hacer commits frecuentes
4. Crear PR hacia `develop`
5. Eliminar después del merge

**Duración:** 1-5 días típicamente

---

#### `fix/` - Correcciones de Bugs
**Propósito:** Corregir errores o bugs

**Nomenclatura:**
```
fix/descripcion-bug
fix/componente-descripcion
```

**Ejemplos:**
- `fix/navbar-mobile-menu`
- `fix/formulario-email-validation`
- `fix/imagen-fallback-error`
- `fix/scroll-smooth-anchors`
- `fix/responsive-gallery`

**Flujo:**
1. Crear desde `develop` (o `main` si es crítico)
2. Corregir el bug
3. Agregar test si es posible
4. Crear PR hacia `develop`
5. Eliminar después del merge

**Duración:** Horas a 1 día típicamente

---

#### `refactor/` - Refactorización
**Propósito:** Mejorar código sin cambiar funcionalidad

**Nomenclatura:**
```
refactor/area-descripcion
refactor/componente-descripcion
```

**Ejemplos:**
- `refactor/reorganizar-carpetas`
- `refactor/optimizar-componentes`
- `refactor/extraer-configuracion`
- `refactor/mejorar-tipos-typescript`
- `refactor/consolidar-estilos`

**Flujo:**
1. Crear desde `develop`
2. Refactorizar código
3. Asegurar que funcionalidad no cambia
4. Actualizar documentación si es necesario
5. Crear PR hacia `develop`
6. Eliminar después del merge

**Duración:** 1-3 días típicamente

---

#### `chore/` - Tareas de Mantenimiento
**Propósito:** Tareas de configuración, dependencias, limpieza

**Nomenclatura:**
```
chore/descripcion-tarea
chore/area-descripcion
```

**Ejemplos:**
- `chore/eliminar-componentes-no-usados`
- `chore/actualizar-dependencias`
- `chore/configurar-eslint`
- `chore/agregar-gitignore`
- `chore/limpiar-codigo-comentado`

**Flujo:**
1. Crear desde `develop`
2. Realizar tarea de mantenimiento
3. Verificar que no rompe nada
4. Crear PR hacia `develop`
5. Eliminar después del merge

**Duración:** Horas a 1 día típicamente

---

#### `docs/` - Documentación
**Propósito:** Cambios solo en documentación

**Nomenclatura:**
```
docs/archivo-descripcion
docs/area-descripcion
```

**Ejemplos:**
- `docs/actualizar-readme`
- `docs/agregar-jsdoc-componentes`
- `docs/mejorar-guia-instalacion`
- `docs/actualizar-changelog`
- `docs/agregar-ejemplos-codigo`

**Flujo:**
1. Crear desde `develop`
2. Actualizar documentación
3. Revisar formato y claridad
4. Crear PR hacia `develop`
5. Eliminar después del merge

**Duración:** Horas típicamente

**Nota:** Para cambios menores en docs, puede hacerse commit directo en `develop`

---

#### `perf/` - Optimizaciones de Rendimiento
**Propósito:** Mejoras de performance

**Nomenclatura:**
```
perf/area-descripcion
perf/componente-descripcion
```

**Ejemplos:**
- `perf/optimizar-imagenes`
- `perf/reducir-bundle-size`
- `perf/lazy-loading-componentes`
- `perf/memoizar-componentes`
- `perf/optimizar-carga-inicial`

**Flujo:**
1. Crear desde `develop`
2. Implementar optimización
3. Medir mejoras (Lighthouse, bundle analyzer)
4. Documentar mejoras obtenidas
5. Crear PR hacia `develop`
6. Eliminar después del merge

**Duración:** 1-2 días típicamente

---

#### `hotfix/` - Correcciones Urgentes
**Propósito:** Correcciones críticas que deben ir directo a producción

**Nomenclatura:**
```
hotfix/descripcion-critica
```

**Ejemplos:**
- `hotfix/correccion-seguridad`
- `hotfix/error-produccion-critico`
- `hotfix/vulnerabilidad-dependencia`

**Flujo:**
1. Crear desde `main` (NO desde develop)
2. Corregir problema crítico
3. Probar exhaustivamente
4. Crear PR hacia `main`
5. Después del merge a `main`, hacer merge a `develop`
6. Eliminar después del merge

**Duración:** Horas típicamente

**Importante:** Solo para problemas críticos que afectan producción

---

## Reglas de Nomenclatura

### Formato General
```
tipo/descripcion-corta-kebab-case
```

### Reglas
1. **Minúsculas:** Todo en minúsculas
2. **Separador:** Usar guiones (`-`) para separar palabras
3. **Descriptivo:** Debe ser claro qué hace la rama
4. **Corto:** Máximo 3-4 palabras preferiblemente
5. **Sin espacios:** Nunca usar espacios
6. **Sin caracteres especiales:** Solo letras, números y guiones

### ✅ Buenos Nombres
```
feature/formulario-validacion
fix/navbar-mobile-menu
refactor/reorganizar-carpetas
chore/eliminar-componentes-no-usados
docs/actualizar-readme
```

### ❌ Malos Nombres
```
Feature/FormularioValidacion          # Mayúsculas incorrectas
fix/navbar mobile menu                # Espacios
feature/nueva_funcionalidad           # Guiones bajos
fix/bug                               # Muy vago
chore/actualizacion                   # Muy genérico
```

---

## Flujo de Trabajo Completo

### Desarrollo de Feature

```bash
# 1. Actualizar develop
git checkout develop
git pull origin develop

# 2. Crear rama de feature
git checkout -b feature/nueva-funcionalidad

# 3. Desarrollar (múltiples commits)
git add .
git commit -m "feat(componente): primer cambio"
# ... más trabajo ...
git commit -m "feat(componente): segundo cambio"

# 4. Mantener actualizada con develop
git fetch origin
git rebase origin/develop  # O merge según preferencia

# 5. Push y crear PR
git push origin feature/nueva-funcionalidad
# Crear PR en GitHub/GitLab hacia develop

# 6. Después del merge, limpiar
git checkout develop
git pull origin develop
git branch -d feature/nueva-funcionalidad  # Eliminar rama local
```

### Hotfix Urgente

```bash
# 1. Crear desde main
git checkout main
git pull origin main
git checkout -b hotfix/correccion-critica

# 2. Corregir
git add .
git commit -m "fix(area): corrección crítica"

# 3. Push y crear PR hacia main
git push origin hotfix/correccion-critica
# Crear PR hacia main

# 4. Después del merge a main, mergear a develop
git checkout develop
git pull origin develop
git merge main
git push origin develop

# 5. Limpiar
git branch -d hotfix/correccion-critica
```

---

## Sincronización de Ramas

### Mantener Feature Actualizada

```bash
# Opción 1: Rebase (historial más limpio)
git checkout feature/mi-feature
git fetch origin
git rebase origin/develop
git push origin feature/mi-feature --force-with-lease

# Opción 2: Merge (más seguro)
git checkout feature/mi-feature
git fetch origin
git merge origin/develop
git push origin feature/mi-feature
```

### Actualizar Develop desde Main

```bash
# Después de release a producción
git checkout develop
git pull origin develop
git merge main
git push origin develop
```

---

## Protección de Ramas

### Ramas Protegidas (Configurar en GitHub/GitLab)

#### `main`
- ✅ Requiere Pull Request
- ✅ Requiere revisión aprobada
- ✅ Requiere que tests pasen (cuando se implementen)
- ✅ No permite force push
- ✅ No permite eliminación

#### `develop`
- ✅ Requiere Pull Request (para cambios significativos)
- ✅ No permite force push a develop
- ⚠️ Permite commits directos para cambios menores

---

## Limpieza de Ramas

### Eliminar Ramas Locales

```bash
# Ver ramas locales
git branch

# Eliminar rama local
git branch -d nombre-rama

# Forzar eliminación (si no está merged)
git branch -D nombre-rama
```

### Eliminar Ramas Remotas

```bash
# Eliminar rama remota
git push origin --delete nombre-rama

# Limpiar referencias a ramas remotas eliminadas
git remote prune origin
```

### Limpieza Automática

Configurar en GitHub/GitLab para eliminar ramas automáticamente después del merge.

---

## Ejemplos de Escenarios

### Escenario 1: Nueva Feature Compleja
```
develop
  └── feature/sistema-temas
        ├── commit 1: agregar configuración de temas
        ├── commit 2: implementar selector de tema
        └── commit 3: agregar persistencia en localStorage
        └── PR → develop → merge
```

### Escenario 2: Múltiples Features en Paralelo
```
develop
  ├── feature/formulario-validacion
  │     └── PR → develop
  ├── feature/galeria-lightbox
  │     └── PR → develop
  └── fix/navbar-mobile-menu
        └── PR → develop
```

### Escenario 3: Hotfix Crítico
```
main (producción con bug)
  └── hotfix/correccion-seguridad
        └── PR → main → merge
             └── merge → develop
```

---

## Buenas Prácticas

1. **Ramas Cortas:** Mantener ramas activas por máximo 3-5 días
2. **Commits Frecuentes:** Commitear cambios relacionados frecuentemente
3. **Sincronización:** Mantener rama actualizada con develop
4. **Limpieza:** Eliminar ramas después del merge
5. **Nomenclatura:** Usar nombres descriptivos y consistentes
6. **PR Pequeños:** Preferir PRs pequeños y enfocados
7. **Documentación:** Actualizar docs si cambia la estructura

---

## Resumen Visual

```
main (producción)
  ↑
  │ (solo hotfix o release)
  │
develop (desarrollo)
  ↑
  ├── feature/* (nuevas funcionalidades)
  ├── fix/* (correcciones)
  ├── refactor/* (refactorización)
  ├── chore/* (mantenimiento)
  ├── docs/* (documentación)
  └── perf/* (optimizaciones)
```

---

**Última actualización:** 2025-01-27

