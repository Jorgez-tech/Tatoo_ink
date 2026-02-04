# REGLAS DE DESARROLLO - NO NEGOCIABLE

Este archivo documenta las restricciones para mantener la coherencia del proyecto.

## 1. HARDCODES PROHIBIDOS

❌ **NO hacer:**
```typescript
// Hardcodes in config/images.ts
const galleryImages = [ /* array de imágenes */ ];

// Alternativas "simplificadas" que usen config local
```

✅ **HACER:**
```typescript
// Gallery obtiene datos SOLO de /api/gallery
const data = await galleryService.getAll();
```

**Razón:** Plan original especifica arquitectura dinámica (BD → API → Frontend)

---

## 2. ESTRUCTURA DEFINIDA

| Componente | Fuente | Nota |
|-----------|--------|------|
| Hero | config/images.ts | Estático, OK hardcodear |
| About | config/images.ts | Estático, OK hardcodear |
| **Gallery** | **API /gallery** | **DINÁMICO, BD** |
| Contact | Backend API | Persistencia |

---

## 3. NO OPCIONES

La dirección es única:

```
Plan documentado en docs/API-REST.md
↓
Implementar EXACTAMENTE como está
↓
Sin alternativas, simplificaciones o "caminos más fáciles"
```

**Si algo necesita cambiar:**
1. Discutir primero
2. Actualizar documentación
3. LUEGO implementar

---

## 4. VALIDACIÓN EN COMMITS

Antes de comitear, verificar:

- [ ] ¿Hay hardcodes nuevos? → NO
- [ ] ¿Hay mocks/fantasía? → NO
- [ ] ¿Está todo en el plan? → SÍ
- [ ] ¿Se actualizó la doc? → SÍ

---

## 5. REFERENCIAS CLAVE

- **Arquitectura:** docs/ARCHITECTURE.md
- **API:** docs/API-REST.md
- **Plan:** docs/NEXT-STEPS.md
- **Código:** Siempre comentar `@see` a la documentación

---

**Última actualización:** 2025-12-19  
**Creado por:** Restauración de plan original  
**Estado:** ACTIVO - No desviarse
