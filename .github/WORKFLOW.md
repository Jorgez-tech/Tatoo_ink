# Flujo de Trabajo del Proyecto

Este documento describe el flujo de trabajo completo para contribuir al proyecto, incluyendo el uso de ramas, commits y pull requests.

---

## Resumen Rápido

1. **Crear rama** desde `develop` para cada cambio relevante
2. **Trabajar** en la rama realizando cambios
3. **Commitear** usando Conventional Commits en español
4. **Push** de la rama al repositorio
5. **Crear Pull Request** hacia `develop` (o `main` si es hotfix)
6. **Revisar** y hacer merge después de aprobación

---

## Estrategia de Ramas

### Ramas Principales

#### `main`
- **Propósito:** Código estable, listo para producción
- **Protección:** Requiere PR y revisión
- **Merge desde:** Solo desde `develop` o `hotfix/*`
- **Nunca commitear directamente** (excepto merge de PRs)

#### `develop`
- **Propósito:** Rama principal de desarrollo
- **Protección:** Requiere PR para cambios significativos
- **Merge desde:** Ramas `feature/*`, `fix/*`, `refactor/*`, etc.
- **Punto de partida** para nuevas ramas

### Ramas de Trabajo

#### `feature/*` - Nuevas Funcionalidades
**Uso:** Agregar nuevas características o funcionalidades

**Ejemplos:**
- `feature/formulario-contacto-backend`
- `feature/sistema-temas`
- `feature/integracion-cms`

**Flujo:**
```bash
# Crear rama desde develop
git checkout develop
git pull origin develop
git checkout -b feature/nombre-funcionalidad

# Trabajar y commitear
git add .
git commit -m "feat(componente): descripción"

# Push y crear PR hacia develop
git push origin feature/nombre-funcionalidad
```

#### `fix/*` - Corrección de Bugs
**Uso:** Corregir errores o bugs en el código

**Ejemplos:**
- `fix/navbar-mobile-menu`
- `fix/imagen-fallback-error`
- `fix/formulario-validacion`

**Flujo:**
```bash
git checkout develop
git pull origin develop
git checkout -b fix/descripcion-bug

# Corregir y commitear
git commit -m "fix(componente): descripción de la corrección"

# Push y crear PR hacia develop
git push origin fix/descripcion-bug
```

#### `refactor/*` - Refactorización
**Uso:** Mejorar código sin cambiar funcionalidad

**Ejemplos:**
- `refactor/reorganizar-carpetas`
- `refactor/optimizar-componentes`
- `refactor/extraer-configuracion`

**Flujo:**
```bash
git checkout develop
git pull origin develop
git checkout -b refactor/descripcion

# Refactorizar y commitear
git commit -m "refactor(area): descripción de la refactorización"

# Push y crear PR hacia develop
git push origin refactor/descripcion
```

#### `chore/*` - Tareas de Mantenimiento
**Uso:** Tareas de mantenimiento, configuración, dependencias

**Ejemplos:**
- `chore/eliminar-componentes-no-usados`
- `chore/actualizar-dependencias`
- `chore/configurar-eslint`

**Flujo:**
```bash
git checkout develop
git pull origin develop
git checkout -b chore/descripcion

# Realizar cambios y commitear
git commit -m "chore(area): descripción"

# Push y crear PR hacia develop
git push origin chore/descripcion
```

#### `docs/*` - Documentación
**Uso:** Cambios solo en documentación

**Ejemplos:**
- `docs/actualizar-readme`
- `docs/agregar-jsdoc`
- `docs/mejorar-guias`

**Flujo:**
```bash
git checkout develop
git pull origin develop
git checkout -b docs/descripcion

# Actualizar documentación y commitear
git commit -m "docs(archivo): descripción"

# Push y crear PR hacia develop
git push origin docs/docs/descripcion
```

#### `perf/*` - Optimizaciones de Rendimiento
**Uso:** Mejoras de performance

**Ejemplos:**
- `perf/optimizar-imagenes`
- `perf/reducir-bundle-size`
- `perf/lazy-loading`

**Flujo:**
```bash
git checkout develop
git pull origin develop
git checkout -b perf/descripcion

# Optimizar y commitear
git commit -m "perf(area): descripción de la optimización"

# Push y crear PR hacia develop
git push origin perf/descripcion
```

#### `hotfix/*` - Correcciones Urgentes
**Uso:** Correcciones críticas que deben ir directo a producción

**Ejemplos:**
- `hotfix/correccion-critica-seguridad`
- `hotfix/error-produccion`

**Flujo:**
```bash
# Crear desde main
git checkout main
git pull origin main
git checkout -b hotfix/descripcion

# Corregir y commitear
git commit -m "fix(area): descripción crítica"

# Push y crear PR hacia main Y develop
git push origin hotfix/descripcion
# Crear PR hacia main primero, luego merge a develop
```

---

## Proceso de Desarrollo

### 1. Inicio de Trabajo

```bash
# Asegurarse de estar actualizado
git checkout develop
git pull origin develop

# Crear nueva rama
git checkout -b tipo/descripcion-corta
```

### 2. Durante el Desarrollo

```bash
# Hacer cambios en el código
# ... editar archivos ...

# Agregar cambios
git add .

# Commit con mensaje convencional
git commit -m "tipo(ámbito): descripción"

# Continuar trabajando...
```

### 3. Antes de Push

```bash
# Asegurarse de estar actualizado con develop
git fetch origin
git rebase origin/develop  # O merge según preferencia

# Resolver conflictos si los hay
# ... resolver conflictos ...

# Push de la rama
git push origin tipo/descripcion-corta
```

### 4. Crear Pull Request

1. Ir a GitHub/GitLab
2. Crear Pull Request desde tu rama hacia `develop`
3. Usar el template de PR
4. Describir cambios realizados
5. Asignar revisores si es necesario
6. Agregar labels apropiados

### 5. Después del Merge

```bash
# Limpiar rama local (opcional)
git checkout develop
git pull origin develop
git branch -d tipo/descripcion-corta  # Eliminar rama local
```

---

## Reglas Importantes

### ✅ Hacer
- Crear una rama para cada cambio relevante
- Usar nombres descriptivos para ramas
- Commitear frecuentemente con mensajes claros
- Actualizar documentación si cambia la estructura
- Sincronizar con `develop` antes de crear PR
- Revisar código propio antes de crear PR
- Usar el template de PR

### ❌ No Hacer
- Commitear directamente en `main` o `develop`
- Usar mensajes de commit genéricos ("cambios", "fix")
- Crear ramas muy largas o complejas
- Dejar código comentado o no utilizado
- Ignorar conflictos de merge
- Crear PRs sin descripción
- Hacer merge sin revisar

---

## Ejemplos de Flujo Completo

### Ejemplo 1: Agregar Nueva Funcionalidad

```bash
# 1. Actualizar develop
git checkout develop
git pull origin develop

# 2. Crear rama
git checkout -b feature/formulario-validacion

# 3. Trabajar
# ... editar Contact.tsx ...
git add src/components/sections/Contact.tsx
git commit -m "feat(contact): agregar validación de email y teléfono"

# ... agregar tests ...
git add src/__tests__/Contact.test.tsx
git commit -m "test(contact): agregar tests de validación"

# 4. Actualizar con develop
git fetch origin
git rebase origin/develop

# 5. Push y crear PR
git push origin feature/formulario-validacion
```

### Ejemplo 2: Corregir Bug

```bash
# 1. Actualizar develop
git checkout develop
git pull origin develop

# 2. Crear rama
git checkout -b fix/navbar-mobile-menu

# 3. Corregir
# ... editar Navbar.tsx ...
git add src/components/layout/Navbar.tsx
git commit -m "fix(navbar): corregir menú móvil que no se cierra al hacer click"

# 4. Push y crear PR
git push origin fix/navbar-mobile-menu
```

### Ejemplo 3: Refactorización

```bash
# 1. Actualizar develop
git checkout develop
git pull origin develop

# 2. Crear rama
git checkout -b refactor/reorganizar-componentes-ui

# 3. Refactorizar
# ... mover archivos ...
git mv src/components/ui/ImageWithFallback.tsx src/components/shared/
git commit -m "refactor(components): mover ImageWithFallback a carpeta shared"

# ... actualizar imports ...
git add .
git commit -m "refactor(imports): actualizar imports de ImageWithFallback"

# 4. Push y crear PR
git push origin refactor/reorganizar-componentes-ui
```

---

## Resolución de Conflictos

Si hay conflictos al hacer rebase/merge:

```bash
# Ver archivos en conflicto
git status

# Resolver conflictos manualmente
# ... editar archivos con conflictos ...

# Agregar archivos resueltos
git add archivo-resuelto.tsx

# Continuar rebase
git rebase --continue

# O abortar si es necesario
git rebase --abort
```

---

## Buenas Prácticas

1. **Commits Atómicos:** Un commit por cambio lógico
2. **Mensajes Claros:** Describir qué y por qué, no solo qué
3. **Ramas Cortas:** Trabajar en ramas por máximo 2-3 días
4. **Sincronización:** Actualizar con develop frecuentemente
5. **Testing:** Probar cambios localmente antes de commit
6. **Documentación:** Actualizar docs si cambia la estructura

---

**Última actualización:** 2025-01-27

