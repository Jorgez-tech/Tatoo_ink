# Instrucciones GitHub - Contexto del Proyecto

Este documento sirve como referencia rapida cada vez que retomemos el trabajo en el proyecto.

**NOTA:** Esta documentacion no usa emoticones para mantener profesionalismo y compatibilidad con todos los sistemas. Los emojis estan prohibidos en todo el proyecto y documentacion.

---

## Contexto del Proyecto

### Descripcion
Landing page profesional para estudio de tatuajes "Ink Studio", construida como **prototipo reutilizable** para futuros clientes en Chile.

### Stack Tecnologico
- **Frontend:** React 19.1.1 + TypeScript 5.9.3
- **Build Tool:** Vite 7.1.12
- **Estilos:** Tailwind CSS 3.4.17
- **UI Components:** shadcn/ui (Radix UI)
- **Iconos:** Lucide React 0.552.0
- **Backend (futuro):** ASP.NET Core

### Origen del Diseno
- Exportado desde **Figma** sin modificaciones
- Componentes adaptados de diseno visual a codigo React

---

## Estructura de Documentacion

```
docs/
|-- 00-PLAN-MAESTRO.md          # Plan general del proyecto (4 fases)
|-- 01-FASE-1-AUDITORIA.md      # Auditoria y limpieza de codigo
|-- 02-FASE-2-OPTIMIZACION.md   # Refactorizacion y separacion de datos
|-- 03-FASE-3-DOCUMENTACION.md  # Documentacion de codigo
|-- 04-FASE-4-FINALIZACION.md   # Performance, SEO y optimizaciones
|-- CHANGELOG.md                # Registro detallado de cambios
|-- STATUS.md                   # Estado actual del proyecto (LEER PRIMERO)
+-- GITHUB-INSTRUCTIONS.md      # Este archivo
```

---

## Inicio Rapido al Retomar el Trabajo

### 1. Leer Estado Actual
```bash
# Archivo mas importante para contexto rapido
cat docs/STATUS.md
```

### 2. Verificar Cambios Pendientes
```bash
git status
git log --oneline -10
```

### 3. Revisar Plan de Trabajo
```bash
# Ver la fase actual
cat docs/01-FASE-1-AUDITORIA.md
```

### 4. Actualizar STATUS.md
Siempre actualizar al terminar una sesion de trabajo con:
- Progreso de tareas completadas
- Problemas encontrados
- Proximos pasos
- Fecha de ultima actualizacion

---

## Convenciones de Commits

### Formato
```
<tipo>(<alcance>): <descripcion>

[cuerpo opcional]
```

### Tipos de Commit
- `feat` - Nueva funcionalidad
- `fix` - Correccion de bug
- `refactor` - Refactorizacion sin cambio funcional
- `style` - Cambios de formato/estilos
- `docs` - Actualizacion de documentacion
- `chore` - Tareas de mantenimiento
- `perf` - Mejoras de performance
- `test` - Agregar o modificar tests

### Ejemplos
```bash
git commit -m "feat(navbar): add sticky header on scroll"
git commit -m "refactor(services): extract data to config file"
git commit -m "docs: update STATUS.md with Phase 1 progress"
git commit -m "chore: remove unused UI components (40 files)"
```

---

## Workflow de Trabajo

### Al Iniciar Sesion
1. `git pull origin main`
2. Leer `docs/STATUS.md`
3. Revisar documento de fase actual
4. Identificar tarea especifica a realizar

### Durante el Trabajo
1. Commits frecuentes y descriptivos
2. Actualizar documentacion relevante
3. Ejecutar `npm run dev` para verificar cambios
4. Verificar que no hay errores: `npm run build`

### Al Terminar Sesion
1. Actualizar `docs/STATUS.md` con:
   - Tareas completadas
   - Estado de progreso (%)
   - Problemas encontrados
   - Proximos pasos
2. Actualizar `docs/CHANGELOG.md` si hay cambios significativos
3. Commit de documentacion: `docs: update session progress`
4. Push a repositorio: `git push origin main`

---

## Estado por Fases

### Fase 0: Configuracion Inicial - COMPLETADA
- Vite + React + TypeScript configurado
- Dependencias instaladas
- Tailwind CSS v3 funcionando
- Pagina renderizando correctamente

### Fase 1: Auditoria y Limpieza - EN PROGRESO (5%)
- [x] Auditoria de componentes UI
- [ ] Eliminar 40 componentes no utilizados
- [ ] Reorganizar estructura de carpetas
- [ ] Optimizar estilos

### Fase 2: Optimizacion - PENDIENTE
- Refactorizar 7 componentes principales
- Crear archivos de configuracion
- Separar datos de presentacion
- Mejorar tipos TypeScript

### Fase 3: Documentacion - PENDIENTE
- JSDoc en componentes
- README.md completo
- Guia de customizacion
- Documentacion de estructura

### Fase 4: Finalizacion - PENDIENTE
- Optimizacion de imagenes
- Performance y SEO
- Accesibilidad (WCAG AA)
- Sistema de temas

---

## Objetivos del Proyecto

### Corto Plazo (2-3 semanas)
- Completar las 4 fases del plan
- Tener template totalmente reutilizable
- Documentacion completa para customizacion

### Mediano Plazo (1-2 meses)
- Crear sistema de configuracion centralizado
- Integrar CMS headless (Strapi/Contentful)
- Migrar 2-3 plantillas HTML existentes
- Crear portfolio de demos

### Largo Plazo (3-6 meses)
- Migrar las 10 plantillas HTML/CSS/JS existentes
- Sistema multi-tenant para clientes
- Deployment automatizado
- Panel de administracion

---

## Problemas Conocidos y Soluciones

### 1. Componentes UI No Utilizados
**Problema:** 40 componentes shadcn/ui instalados pero no usados
**Impacto:** Bundle size inflado, codigo confuso
**Solucion:** Fase 1.1 - Eliminar todos los componentes no utilizados
**Estado:** Pendiente

### 2. Imagenes Externas
**Problema:** Todas las imagenes cargan desde Unsplash (URLs externas)
**Impacto:** Dependencia externa, posibles fallos, performance
**Solucion:** Fase 4.1 - Descargar y optimizar localmente
**Estado:** Pendiente

### 3. Formulario de Contacto
**Problema:** No tiene backend funcional
**Impacto:** Formulario no envia emails realmente
**Solucion:** Fase 2.1 - Preparar integracion con backend/API
**Estado:** Pendiente

### 4. Warning de Fast Refresh
**Problema:** `button.tsx` exporta componente + constante
**Impacto:** Warning en consola, no critico
**Solucion:** Separar exportaciones
**Estado:** Bajo prioridad

---

## Dependencias Criticas

### Produccion
```json
{
  "react": "^19.1.1",
  "react-dom": "^19.1.1",
  "lucide-react": "^0.552.0",
  "tailwindcss": "3.4.17",
  "class-variance-authority": "^0.7.1",
  "clsx": "^2.1.1",
  "tailwind-merge": "^3.3.1"
}
```

### Desarrollo
```json
{
  "@vitejs/plugin-react": "^4.3.4",
  "typescript": "~5.9.3",
  "vite": "^7.1.12",
  "autoprefixer": "^10.4.20",
  "postcss": "^8.4.49"
}
```

---

## Comandos Utiles

### Desarrollo
```bash
npm run dev              # Servidor de desarrollo
npm run build            # Build de produccion
npm run preview          # Preview del build
npm run lint             # Linter
```

### Git
```bash
git status               # Ver cambios
git log --oneline -10    # Ultimos 10 commits
git diff                 # Ver diferencias
git add .                # Agregar todo
git commit -m "mensaje"  # Commit
git push origin main     # Push
```

### Analisis
```bash
npm list --depth=0       # Ver dependencias instaladas
du -sh node_modules      # Tamano de node_modules (Linux/Mac)
npm run build -- --analyze  # Analizar bundle (si esta configurado)
```

---

## Recursos y Referencias

### Documentacion Oficial
- [React 19 Docs](https://react.dev/)
- [TypeScript Docs](https://www.typescriptlang.org/docs/)
- [Tailwind CSS Docs](https://tailwindcss.com/docs)
- [Vite Docs](https://vite.dev/)
- [shadcn/ui](https://ui.shadcn.com/)

### Proyecto
- **Desarrollador:** Jorge
- **Ubicacion:** Chile
- **Tipo:** Plantillas para profesionales y pequenos negocios
- **Portfolio:** 10 plantillas HTML/CSS/JS existentes

---

## Checklist Pre-Commit

Antes de cada commit importante, verificar:

- [ ] Codigo compila sin errores (`npm run build`)
- [ ] No hay errores de TypeScript
- [ ] Servidor de desarrollo funciona (`npm run dev`)
- [ ] Documentacion actualizada si corresponde
- [ ] Commit message descriptivo y en formato correcto
- [ ] `STATUS.md` actualizado si es final de sesion

---

## Proxima Sesion

**Tarea Principal:** Completar Fase 1.1 - Eliminar componentes UI no utilizados

**Pasos:**
1. Verificar lista de componentes a eliminar (docs/01-FASE-1-AUDITORIA.md)
2. Eliminar archivos uno por uno o en batch
3. Verificar que no hay imports rotos
4. Commit: `chore: remove 40 unused UI components`
5. Actualizar STATUS.md con progreso

**Tiempo estimado:** 30-45 minutos

---

_Ultima actualizacion: 2025-11-05_
_Proxima revision: Al iniciar proxima sesion_
