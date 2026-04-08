# Enmiendas y Correcciones: Plan de acción

Fecha: 2026-03-17
Estatus: Crítico - Requiere revisión y aprobación antes de Fase 1

## Descubrimiento

Durante la revisión profunda de la especificación conceptual v1, se identificaron 4 brechas críticas que harían que el producto final sea **incompleto, inseguro y dependiente del desarrollador**.

## Documentos de análisis

1. **12-hallazgos-criticos-gap-analysis.md** - Resumen ejecutivo de los 4 problemas
2. **13-correccion-1-admin-gallery-settings.md** - El admin debe gestionar galería y config del negocio
3. **14-correccion-2-modelo-datos-alineacion.md** - Alinear modelo conceptual con código existente
4. **15-correccion-3-autenticacion-completa.md** - Definir completamente login y usuarios
5. **16-correccion-4-datos-negocio-editable.md** - Datos del negocio desde BD, no hardcode

## Cambios recomendados por prioridad

### INMEDIATO (Bloqueadores)

1. **Autenticación completa** (15-correccion-3...)
   - Definir endpoints de login/logout
   - Tabla User
   - JWT tokens
   - Duración estimada: 1-2 sprints

2. **Admin gestione galería + settings** (13-correccion-1...)
   - Endpoints para subir/editar/borrar fotos
   - Tabla BusinessSettings
   - Panel admin expandido
   - Duración estimada: 2-3 sprints

### IMPORTANTE (Evita deuda técnica)

3. **Datos del negocio editables** (16-correccion-4...)
   - Migrar de hardcode a API
   - Frontend consume dados desde BD
   - Duración estimada: 4-5 días

4. **Alinear modelos con código actual** (14-correccion-2...)
   - Mantener INT para IDs (no migrar a UUID)
   - Incluir GalleryImage en modelo
   - Mapear ContactRequest a ContactMessage
   - Duración estimada: 1-2 sprints

## Impacto financiero

| Corrección | Sprint | Horas | Costo |
|-----------|--------|-------|-------|
| Autenticación | 1-2 | 40-80 | Alto |
| Admin Gallery+Settings | 2-3 | 50-120 | Alto |
| Datos editables | 0.5 | 20-30 | Bajo |
| Alineación modelos | 1-2 | 30-60 | Medio |
| **TOTAL** | **5-7** | **140-290** | **Medio-Alto** |

## Recomendación

✅ **Revisar y aplicar estas correcciones ANTES de comenzar Fase 1**

Si se implementa sin estas enmiendas:
- ❌ Entregarás un buzón de mensajes, no un producto
- ❌ El cliente seguirá dependiendo de ti para cada cambio
- ❌ La seguridad será improvisada después (deuda técnica)
- ❌ Reescribirás código innecesariamente (UUID vs Int)

Si se implementa CON estas enmiendas:
- ✅ Producto independiente y autosuficiente
- ✅ Cliente puede gestionar su contenido y negocio
- ✅ Seguridad planificada desde el inicio
- ✅ Reutilización de código existente
- ✅ Mejor valor para el cliente

## Próximos pasos

1. **Revisar** (30 min): Usuario valida estos puntos
2. **Ajustar** (1-2 horas): Modificar si hay desacuerdos
3. **Aprobar**: Usuario aprueba plan de correcciones
4. **Regenerar backlog**: Con nuevas historias y estimaciones
5. **Comenzar Fase 1**: Con especificación corregida

## Decisiones pendientes

El usuario/cliente debe decidir:

- [ ] ¿Aplicar todas las enmiendas?
- [ ] ¿Priorizar "Autenticación" primero?
- [ ] ¿Diferir "Datos editables" para Phase 2?
- [ ] ¿Mantener INT ids o migrar a UUID?

**Recomendación:** Aplicar todas. Costo marginal bajo comparado con rectificar después.
