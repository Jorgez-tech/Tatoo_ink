# Instrucciones GitHub - Contexto del Proyecto

Este documento sirve como referencia rápida cada vez que retomemos el trabajo en el proyecto.

**NOTA:** Esta documentación no usa emoticones para mantener profesionalismo y compatibilidad con todos los sistemas.

---

## Contexto del Proyecto

### Descripción
Landing page profesional para estudio de tatuajes "Ink Studio", construida como **prototipo reutilizable** para futuros clientes en Chile.

### Stack Tecnológico
- **Frontend:** React 19.1.1 + TypeScript 5.9.3
- **Build Tool:** Vite 7.1.12
- **Estilos:** Tailwind CSS 3.4.17
- **UI Components:** shadcn/ui (Radix UI)
- **Iconos:** Lucide React 0.552.0
- **Backend (futuro):** ASP.NET Core

### Origen del Diseño
- Exportado desde **Figma** sin modificaciones
- Componentes adaptados de diseño visual a código React

---

## Estructura de Documentación

```
docs/
├── 00-PLAN-MAESTRO.md          # Plan general del proyecto (4 fases)
├── 01-FASE-1-AUDITORIA.md      # Auditoría y limpieza de código
├── 02-FASE-2-OPTIMIZACION.md   # Refactorización y separación de datos
├── 03-FASE-3-DOCUMENTACION.md  # Documentación de código
├── 04-FASE-4-FINALIZACION.md   # Performance, SEO y optimizaciones
├── CHANGELOG.md                # Registro detallado de cambios
├── STATUS.md                   # Estado actual del proyecto (LEER PRIMERO)
└── GITHUB-INSTRUCTIONS.md      # Este archivo
```

---

## Inicio Rápido al Retomar el Trabajo

### 1. Leer Estado Actual
```bash
# Archivo más importante para contexto rápido
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
Siempre actualizar al terminar una sesión de trabajo con:
- Progreso de tareas completadas
- Problemas encontrados
- Próximos pasos
- Fecha de última actualización

---

## Convenciones de Commits

### Formato
```
<tipo>(<alcance>): <descripción>

[cuerpo opcional]
```

### Tipos de Commit
- `feat` - Nueva funcionalidad
- `fix` - Corrección de bug
- `refactor` - Refactorización sin cambio funcional
- `style` - Cambios de formato/estilos
- `docs` - Actualización de documentación
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

### Al Iniciar Sesión
1. `git pull origin main`
2. Leer `docs/STATUS.md`
3. Revisar documento de fase actual
4. Identificar tarea específica a realizar

### Durante el Trabajo
1. Commits frecuentes y descriptivos
2. Actualizar documentación relevante
3. Ejecutar `npm run dev` para verificar cambios
4. Verificar que no hay errores: `npm run build`

### Al Terminar Sesión
1. Actualizar `docs/STATUS.md` con:
   - Tareas completadas
   - Estado de progreso (%)
   - Problemas encontrados
   - Próximos pasos
2. Actualizar `docs/CHANGELOG.md` si hay cambios significativos
3. Commit de documentación: `docs: update session progress`
4. Push a repositorio: `git push origin main`

---

## Estado por Fases

### Fase 0: Configuración Inicial - COMPLETADA
- Vite + React + TypeScript configurado
- Dependencias instaladas
- Tailwind CSS v3 funcionando
- Página renderizando correctamente

### Fase 1: Auditoría y Limpieza - EN PROGRESO (5%)
- [x] Auditoría de componentes UI
- [ ] Eliminar 40 componentes no utilizados
- [ ] Reorganizar estructura de carpetas
- [ ] Optimizar estilos

### Fase 2: Optimización - PENDIENTE
- Refactorizar 7 componentes principales
- Crear archivos de configuración
- Separar datos de presentación
- Mejorar tipos TypeScript

### Fase 3: Documentación - PENDIENTE
- JSDoc en componentes
- README.md completo
- Guía de customización
- Documentación de estructura

### Fase 4: Finalización - PENDIENTE
- Optimización de imágenes
- Performance y SEO
- Accesibilidad (WCAG AA)
- Sistema de temas

---

## Objetivos del Proyecto

### Corto Plazo (2-3 semanas)
- Completar las 4 fases del plan
- Tener template totalmente reutilizable
- Documentación completa para customización

### Mediano Plazo (1-2 meses)
- Crear sistema de configuración centralizado
- Integrar CMS headless (Strapi/Contentful)
- Migrar 2-3 plantillas HTML existentes
- Crear portfolio de demos

### Largo Plazo (3-6 meses)
- Migrar las 10 plantillas HTML/CSS/JS existentes
- Sistema multi-tenant para clientes
- Deployment automatizado
- Panel de administración

---

## Problemas Conocidos y Soluciones

### 1. Componentes UI No Utilizados
**Problema:** 40 componentes shadcn/ui instalados pero no usados  
**Impacto:** Bundle size inflado, código confuso  
**Solución:** Fase 1.1 - Eliminar todos los componentes no utilizados  
**Estado:** Pendiente

### 2. Imágenes Externas
**Problema:** Todas las imágenes cargan desde Unsplash (URLs externas)  
**Impacto:** Dependencia externa, posibles fallos, performance  
**Solución:** Fase 4.1 - Descargar y optimizar localmente  
**Estado:** Pendiente

### 3. Formulario de Contacto
**Problema:** No tiene backend funcional  
**Impacto:** Formulario no envía emails realmente  
**Solución:** Fase 2.1 - Preparar integración con backend/API  
**Estado:** Pendiente

### 4. Warning de Fast Refresh
**Problema:** `button.tsx` exporta componente + constante  
**Impacto:** Warning en consola, no crítico  
**Solución:** Separar exportaciones  
**Estado:** Bajo prioridad

---

## Dependencias Críticas

### Producción
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

## Comandos Útiles

### Desarrollo
```bash
npm run dev              # Servidor de desarrollo
npm run build            # Build de producción
npm run preview          # Preview del build
npm run lint             # Linter
```

### Git
```bash
git status               # Ver cambios
git log --oneline -10    # Últimos 10 commits
git diff                 # Ver diferencias
git add .                # Agregar todo
git commit -m "mensaje"  # Commit
git push origin main     # Push
```

### Análisis
```bash
npm list --depth=0       # Ver dependencias instaladas
du -sh node_modules      # Tamaño de node_modules (Linux/Mac)
npm run build -- --analyze  # Analizar bundle (si está configurado)
```

---

## Recursos y Referencias

### Documentación Oficial
- [React 19 Docs](https://react.dev/)
- [TypeScript Docs](https://www.typescriptlang.org/docs/)
- [Tailwind CSS Docs](https://tailwindcss.com/docs)
- [Vite Docs](https://vite.dev/)
- [shadcn/ui](https://ui.shadcn.com/)

### Proyecto
- **Desarrollador:** Jorge
- **Ubicación:** Chile
- **Tipo:** Plantillas para profesionales y pequeños negocios
- **Portfolio:** 10 plantillas HTML/CSS/JS existentes

---

## Checklist Pre-Commit

Antes de cada commit importante, verificar:

- [ ] Código compila sin errores (`npm run build`)
- [ ] No hay errores de TypeScript
- [ ] Servidor de desarrollo funciona (`npm run dev`)
- [ ] Documentación actualizada si corresponde
- [ ] Commit message descriptivo y en formato correcto
- [ ] `STATUS.md` actualizado si es final de sesión

---

## Próxima Sesión

**Tarea Principal:** Completar Fase 1.1 - Eliminar componentes UI no utilizados

**Pasos:**
1. Verificar lista de componentes a eliminar (docs/01-FASE-1-AUDITORIA.md)
2. Eliminar archivos uno por uno o en batch
3. Verificar que no hay imports rotos
4. Commit: `chore: remove 40 unused UI components`
5. Actualizar STATUS.md con progreso

**Tiempo estimado:** 30-45 minutos

---

_Última actualización: 2025-11-05_  
_Próxima revisión: Al iniciar próxima sesión_
