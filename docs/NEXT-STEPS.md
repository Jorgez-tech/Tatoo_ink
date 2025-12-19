# NEXT STEPS - Estado y Próximos Pasos

Documento vivo para registrar el estado actual y el trabajo pendiente.

Última actualización: 2025-12-18

---

## Estado actual

- Frontend: completo (template estable)
- Backend: estable y probado (suite de tests pasando)

---

## Fuente de verdad (documentación base)

- Índice: `docs/README.md`
- Arquitectura: `ARCHITECTURE.md`
- API: `API-REST.md`
- Seguridad: `SECURITY.md`
- QA: `QA.md`

---

## Trabajo pendiente (prioridad)

### Documentación

- Revisar consistencia de enlaces internos entre `docs/*.md` y los 3 READMEs (raíz, `src/`, `backend/`).
- Confirmar que no queda contenido duplicado/obsoleto en docs antiguas (solo referencias cruzadas válidas).
- Agregar JSDoc a los componentes principales del frontend (si se mantiene como requisito de Fase 3).

### Mantenimiento

- Reducir warnings en `dotnet test` (nullable y datos duplicados) sin cambiar comportamiento.
- Revisar despliegue productivo considerando persistencia del archivo SQLite y rotación de logs.

### Opcional

- Migrar imágenes externas a assets locales optimizados.

---

## Notas

- La configuración del backend (appsettings, email, CORS, rate limiting) está documentada en `backend/README.md`.

