# Instrucciones de Trabajo (Git / GitHub)

Este documento sirve como referencia rápida para retomar el trabajo y mantener un flujo consistente.

---

## Contexto del proyecto

### Descripción
Landing page profesional para estudio de tatuajes "Ink Studio", construida como **prototipo reutilizable** para futuros clientes en Chile.

### Stack

- Frontend: React + TypeScript + Vite + Tailwind CSS v3
- Backend: ASP.NET Core (.NET 8) + SQLite (EF Core)

### Fuente de diseño

- Diseño exportado desde Figma y adaptado a React

---

## Documentación (qué leer)

```
docs/
|-- README.md                   # Índice de documentación
|-- NEXT-STEPS.md               # Estado actual y trabajo pendiente
|-- ARCHITECTURE.md             # Arquitectura (fuente de verdad)
|-- API-REST.md                 # API REST (fuente de verdad)
|-- SECURITY.md                 # Seguridad (fuente de verdad)
|-- QA.md                       # QA y testing (fuente de verdad)
|-- CUSTOMIZATION.md            # Personalización
|-- DEPLOYMENT.md               # Despliegue
|-- PERFORMANCE.md              # Performance
|-- ACCESSIBILITY.md            # Accesibilidad
|-- STRUCTURE.md                # Estructura frontend (detalle)
|-- BACKEND-QUICKSTART.md       # Arranque local backend
|-- BACKEND-INTEGRATION.md      # Integración frontend-backend
|-- CHECKPOINT-FINAL.md         # Checklist / release notes
+-- GITHUB-INSTRUCTIONS.md      # Este archivo
```

---

## Inicio rápido al retomar

1. Leer `docs/README.md` y `docs/NEXT-STEPS.md`.

2. Revisar estado de git:

```bash
git status
git log --oneline -10
```

3. Elegir tarea y mantener cambios pequeños.

## Cierre de sesión

Actualizar `docs/NEXT-STEPS.md` con:

- Qué se completó
- Qué quedó pendiente
- Qué riesgos o decisiones se tomaron
- Fecha de actualización

---

## Convenciones de commits

### Formato
```
<tipo>(<alcance>): <descripcion>

[cuerpo opcional]
```

### Tipos
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
git commit -m "feat(navbar): agrega header sticky en scroll"
git commit -m "refactor(config): extrae datos a archivos de configuracion"
git commit -m "docs: actualiza NEXT-STEPS con progreso de sesion"
git commit -m "chore(ui): elimina componentes UI no usados"
```

---

## Workflow

### Al Iniciar Sesion
1. `git pull origin main`
2. Leer `docs/NEXT-STEPS.md`
3. Revisar documento de fase actual
4. Identificar tarea especifica a realizar

### Durante el trabajo
1. Commits frecuentes y descriptivos
2. Actualizar documentacion relevante
3. Ejecutar `npm run dev` para verificar cambios
4. Verificar calidad:

- Frontend: `npm run build`
- Backend: `dotnet test backend.Tests/backend.Tests.csproj`

### Al Terminar Sesion
1. Actualizar `docs/NEXT-STEPS.md` con:
   - Tareas completadas
   - Estado de progreso (%)
   - Problemas encontrados
   - Proximos pasos
2. Commit de documentacion: `docs: actualiza progreso de sesion`
4. Push a repositorio: `git push origin main`

---

## Estado actual

- Frontend: estable y completo como template
- Backend: estable y probado
- Documentación: en proceso de consolidación (ver `docs/NEXT-STEPS.md`)

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

## Problemas conocidos

### Imágenes externas
**Problema:** Todas las imagenes cargan desde Unsplash (URLs externas)
**Impacto:** Dependencia externa, posibles fallos, performance
**Solución recomendada:** descargar y optimizar localmente (WebP/AVIF) para producción.

---

## Dependencias

Ver `package.json` para la lista actual.

---

## Comandos útiles

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
- [React Docs](https://react.dev/)
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
- [ ] `docs/NEXT-STEPS.md` actualizado si es final de sesion

---

## Proxima Sesion

Consulta `docs/NEXT-STEPS.md` para la tarea principal de la proxima sesion y el checklist de cierre.

---

_Ultima actualizacion: 2025-11-05_
_Proxima revision: Al iniciar proxima sesion_
