# Convención de Commits

Este proyecto usa **Conventional Commits en español** para mantener un historial de commits claro, consistente y trazable.

---

## Formato General

```
tipo(ámbito): descripción breve

Descripción detallada opcional explicando el qué y el por qué

Cuerpo del mensaje (opcional) con más detalles si es necesario
```

---

## Tipos de Commit

### `feat` - Nueva Funcionalidad
**Cuándo usar:** Agregar una nueva característica o funcionalidad al proyecto

**Ejemplos:**
```
feat(contact): agregar validación de formulario con react-hook-form
feat(gallery): implementar modal de imágenes en galería
feat(navbar): agregar menú desplegable para servicios
```

### `fix` - Corrección de Bug
**Cuándo usar:** Corregir un error o bug en el código

**Ejemplos:**
```
fix(navbar): corregir menú móvil que no se cierra al hacer click
fix(contact): corregir validación de email que acepta valores inválidos
fix(gallery): corregir error al cerrar modal de imagen
```

### `refactor` - Refactorización
**Cuándo usar:** Mejorar código sin cambiar funcionalidad

**Ejemplos:**
```
refactor(components): reorganizar estructura de carpetas
refactor(contact): extraer lógica de validación a hook personalizado
refactor(styles): consolidar estilos duplicados en variables CSS
```

### `docs` - Documentación
**Cuándo usar:** Cambios solo en documentación

**Ejemplos:**
```
docs(readme): actualizar instrucciones de instalación
docs(components): agregar JSDoc a componente Hero
docs(workflow): agregar guía de flujo de trabajo
```

### `style` - Estilos
**Cuándo usar:** Cambios que no afectan la funcionalidad (formato, espacios, etc.)

**Ejemplos:**
```
style(components): corregir indentación en Navbar.tsx
style(config): aplicar formato consistente a archivos de configuración
style(globals): organizar variables CSS por categorías
```

### `chore` - Tareas de Mantenimiento
**Cuándo usar:** Tareas de mantenimiento, configuración, dependencias

**Ejemplos:**
```
chore(deps): actualizar dependencias a versiones más recientes
chore(ui): eliminar componentes UI no utilizados
chore(eslint): configurar reglas de ESLint adicionales
chore(git): agregar .gitignore para archivos temporales
```

### `perf` - Mejoras de Rendimiento
**Cuándo usar:** Optimizaciones de performance

**Ejemplos:**
```
perf(images): optimizar imágenes con WebP y lazy loading
perf(bundle): reducir tamaño de bundle eliminando dependencias no usadas
perf(components): implementar React.memo en componentes pesados
```

### `test` - Tests
**Cuándo usar:** Agregar o modificar tests (cuando se implementen)

**Ejemplos:**
```
test(contact): agregar tests de validación de formulario
test(components): agregar tests unitarios para Hero
test(utils): agregar tests para función de formateo
```

### `revert` - Revertir Cambios
**Cuándo usar:** Revertir un commit anterior

**Ejemplos:**
```
revert: revertir commit "feat(contact): agregar validación"
Revertir cambios que causaron problemas en producción
```

---

## Ámbitos (Scopes)

Los ámbitos identifican la parte del código afectada. Ámbitos comunes:

### Componentes
- `navbar` - Componente Navbar
- `footer` - Componente Footer
- `hero` - Componente Hero
- `services` - Componente Services
- `gallery` - Componente Gallery
- `about` - Componente About
- `contact` - Componente Contact

### Áreas Técnicas
- `config` - Archivos de configuración
- `styles` - Estilos CSS/Tailwind
- `types` - Tipos TypeScript
- `hooks` - React hooks
- `utils` - Utilidades
- `deps` - Dependencias
- `build` - Configuración de build
- `docs` - Documentación

### Ejemplos con Ámbitos
```
feat(contact): agregar validación de formulario
fix(navbar): corregir menú móvil
refactor(config): extraer información de negocio a archivo de configuración
docs(workflow): agregar guía de flujo de trabajo
chore(ui): eliminar componentes no utilizados
perf(images): optimizar carga de imágenes
```

---

## Descripción del Commit

### Reglas
1. **Primera línea:** Máximo 50-72 caracteres
2. **En minúsculas:** Excepto nombres propios o acrónimos
3. **Sin punto final:** Al final de la primera línea
4. **Imperativo:** "agregar" no "agregué" o "agregando"
5. **Específico:** Describir qué cambió, no solo "cambios"

### ✅ Buenos Ejemplos
```
feat(contact): agregar validación de email y teléfono
fix(navbar): corregir menú móvil que no se cierra
refactor(components): reorganizar estructura de carpetas
docs(readme): actualizar instrucciones de instalación
chore(ui): eliminar 40 componentes UI no utilizados
```

### ❌ Malos Ejemplos
```
fix: arreglar bug                    # Muy vago, sin ámbito
feat: nueva funcionalidad            # Sin ámbito ni descripción
cambios                               # Sin tipo ni ámbito
fix(navbar): bug fix                 # Inglés, no español
feat(contact): agregué validación    # Pasado, no imperativo
```

---

## Cuerpo del Mensaje (Opcional)

Para cambios complejos, agregar descripción detallada después de una línea en blanco:

```
feat(contact): agregar validación completa de formulario

Implementar validación usando react-hook-form con las siguientes reglas:
- Email: formato válido y requerido
- Teléfono: formato chileno (+56) opcional
- Mensaje: mínimo 10 caracteres, requerido

Agregar mensajes de error en español y feedback visual
al usuario durante la validación.
```

---

## Footer (Opcional)

Para referencias a issues o breaking changes:

```
feat(api): cambiar endpoint de contacto

BREAKING CHANGE: El endpoint ahora requiere autenticación.
El parámetro 'email' ahora es obligatorio.

Cierra #123
Refs #45
```

---

## Ejemplos Completos

### Ejemplo 1: Nueva Funcionalidad Simple
```
feat(contact): agregar validación de email
```

### Ejemplo 2: Corrección de Bug con Detalles
```
fix(navbar): corregir menú móvil que no se cierra

El menú móvil permanecía abierto después de hacer click
en un enlace. Agregar handler para cerrar el menú
automáticamente al navegar.
```

### Ejemplo 3: Refactorización Compleja
```
refactor(components): reorganizar estructura de carpetas

Reorganizar componentes siguiendo la estructura propuesta:
- Mover componentes de layout a src/components/layout/
- Mover secciones a src/components/sections/
- Mover ImageWithFallback a src/components/shared/

Actualizar todos los imports afectados.
```

### Ejemplo 4: Tarea de Mantenimiento
```
chore(ui): eliminar componentes UI no utilizados

Eliminar 40 componentes de shadcn/ui que no se están usando:
- accordion, alert, avatar, badge, etc.

Mantener solo: button, card, input, textarea, label.
Esto reduce el bundle size en aproximadamente 2.5 MB.
```

### Ejemplo 5: Mejora de Performance
```
perf(images): optimizar carga de imágenes con lazy loading

Implementar lazy loading para imágenes de la galería
usando Intersection Observer API. Esto mejora el tiempo
de carga inicial en aproximadamente 40%.
```

---

## Convenciones Adicionales

### Múltiples Cambios
Si un commit incluye múltiples cambios relacionados, agruparlos:

```
refactor(components): reorganizar y limpiar componentes

- Mover ImageWithFallback a carpeta shared
- Eliminar App.css no utilizado
- Actualizar imports en todos los componentes afectados
```

### Commits de Configuración
```
chore(config): agregar configuración de ESLint
chore(build): configurar variables de entorno para producción
chore(git): actualizar .gitignore para archivos de IDE
```

### Commits de Documentación
```
docs(readme): actualizar instrucciones de instalación
docs(components): agregar JSDoc a componente Hero
docs(workflow): agregar guía de flujo de trabajo
```

---

## Herramientas Útiles

### Commitizen (Opcional)
Para ayudar con el formato de commits, puedes usar Commitizen:

```bash
npm install -g commitizen
npm install -D @commitlint/cli @commitlint/config-conventional
```

### Hook de Pre-commit (Recomendado)
Configurar hook para validar formato de commits:

```bash
# .husky/commit-msg
npx --no -- commitlint --edit $1
```

---

## Beneficios

1. **Trazabilidad:** Fácil de entender qué cambió y por qué
2. **Historial Limpio:** Historial de Git más legible
3. **Automatización:** Permite generar CHANGELOG automáticamente
4. **Filtrado:** Fácil buscar commits por tipo
5. **Colaboración:** Facilita revisión de código

---

**Última actualización:** 2025-01-27

