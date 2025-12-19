# Documentacion - Ink Studio

Esta carpeta contiene la documentacion tecnica y operativa del proyecto Ink Studio. El objetivo es que cualquier persona pueda:

- Entender la arquitectura (frontend + backend)
- Integrar y consumir la API
- Ejecutar pruebas y validar calidad
- Desplegar a produccion
- Personalizar el template para nuevos clientes

## Indice

### Entrada rapida

- Estado y trabajo activo: `NEXT-STEPS.md`
- Despliegue: `DEPLOYMENT.md`
- Personalizacion: `CUSTOMIZATION.md`

### Documentacion tecnica (fuente de verdad)

- Arquitectura: `ARCHITECTURE.md`
- API REST: `API-REST.md`
- Seguridad: `SECURITY.md`
- QA y testing: `QA.md`

### Guías específicas

- Estructura del frontend: `STRUCTURE.md`
- Integracion frontend-backend: `BACKEND-INTEGRATION.md`
- Inicio rapido del backend (local): `BACKEND-QUICKSTART.md`
- Performance: `PERFORMANCE.md`
- Accesibilidad: `ACCESSIBILITY.md`
- Checklist de release: `CHECKPOINT-FINAL.md`

### Operacion y reglas de trabajo

- Flujo de trabajo y Git: `GITHUB-INSTRUCTIONS.md`
- Lineamientos de codigo y PRs: `GUIDELINES.md`

### Legal

- Atribuciones: `ATTRIBUTIONS.md`

### Especificaciones tecnicas originales

Para consultar las especificaciones que guiaron la implementacion del backend:

- `.kiro/specs/tattoo-studio-backend/design.md` - Arquitectura en capas, componentes e interfaces
- `.kiro/specs/tattoo-studio-backend/requirements.md` - Requisitos funcionales con criterios de aceptacion
- `.kiro/specs/tattoo-studio-backend/tasks.md` - Plan de implementacion (completado)

Nota: Estos archivos representan el diseño original. Para la documentación actualizada del backend consultar `ARCHITECTURE.md`, `API-REST.md` y `SECURITY.md`.

## Principios de mantenimiento

- Evitar duplicacion: si una regla vive en un doc tecnico, los demas deben enlazarla.
- Preferir comandos y ejemplos reproducibles.
- Mantener el tono profesional y consistente.
