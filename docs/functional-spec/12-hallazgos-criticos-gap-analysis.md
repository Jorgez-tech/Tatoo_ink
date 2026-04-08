# Hallazgos Críticos: Discrepancias Especificación vs Realidad

Fecha: 2026-03-17
Estado: Crítico - Requiere corrección antes de implementación

## Resumen ejecutivo

La especificación funcional actual (v1 conceptual) tiene 4 brechas críticas que harían que el producto final sea incompleto y dependiente del desarrollador. Esto debe corregirse antes de pasar a implementación.

---

## 1. CRÍTICO: Panel Admin incompleto (Admin Panel useless)

### Problema

**Especificación (03-actores-casos-uso.md):**

```
Casos de uso internos:
1. Iniciar sesion en zona interna.
2. Listar solicitudes por estado.
3. Ver detalle de una solicitud.
4. Cambiar estado de solicitud.
5. Agregar notas internas.
6. Asignar responsable (admin).
```

⚠️ **NO INCLUYE:**
- Gestión de galería (subir, editar, borrar fotos)
- Edición de contenido del negocio
- Gestión de horarios o disponibilidad

### Consecuencia

Si se implementa así, el cliente tendrá un login seguro solo para leer correos, pero YOU seguirás siendo el único que puede:
- Subir fotos nuevas
- Cambiar el número de teléfono
- Actualizar horarios
- Editar descripciones de servicios

**Veredicto:** Mata el valor del producto. No es un producto independiente, es un buzón de mensajes.

### Corrección recomendada

El admin debe poder:
- Gestionar la galería: subir, editar metadata (Alt, categoría), borrar.
- Editar la información del negocio (desde base de datos, no hardcode).
- Gestionar usuarios internos (crear, desactivar).
- Cambiar configuración de horarios y disponibilidad.

---

## 2. CRÍTICO: Incompatibilidad técnica con código existente

### Problema A: IDs - UUID vs Int

**Especificación (05-modelo-datos-conceptual.md):**

```
- id (uuid)
- contactRequestId (uuid)
- authorUserId (uuid)
```

**Realidad (código existente):**

```csharp
// backend/Models/GalleryImage.cs
public int Id { get; set; }

// backend/Models/ContactMessage.cs
public int Id { get; set; }
```

⚠️ El código actual UTILIZA INTs (números), no UUID.

### Problema B: Entidades que ignora la especificación

**Especificación define:** ContactRequest, User, ContactNote, BusinessSettings.

**Realidad del código:**
- GalleryImage: YA EXISTE (25 líneas, funcional en BD)
- ContactMessage: YA EXISTE (campos: Name, Email, Phone, Message, WantsAppointment, EmailSent, EmailSentAt)

⚠️ La spec IGNORA GalleryImage pero luego dice que el admin debe gestionar galería. Contradicción.

### Consecuencia

- Reescribir todas las migraciones, DTOs y queries para cambiar Int → Guid es trabajo innecesario sin beneficio real.
- Romper el código front-end que ya espera Int IDs.
- Duplicar esfuerzo.

### Corrección recomendada

- Mantener Int para IDs (ya funciona, sin choque).
- Actualizar el modelo conceptual para incluir GalleryImage.
- Actualizar ContactRequest para alinearlo con ContactMessage existente (WantsAppointment, EmailSent, etc.).

---

## 3. CRÍTICO: Autenticación "Mágica" (no definida)

### Problema A: No existe endpoint de login

**Especificación (06-contrato-api-conceptual.md):**

```
## Endpoints internos (autenticados)
- GET /api/v1/internal/contact-requests
- GET /api/v1/internal/contact-requests/{id}
- etc.
```

⚠️ **PERO NO DEFINE:**
- POST /api/v1/auth/login
- POST /api/v1/auth/logout
- POST /api/v1/auth/refresh

### Problema B: No define creación de usuarios

**Especificación (04-permisos-acceso.md):**

```
Usuario tiene role: (admin, artista)
```

⚠️ **PERO NO DEFINE:**
- ¿Cómo se crea el primer admin?
- ¿Cómo se crean nuevos usuarios?
- ¿Quién puede crear usuarios?
- ¿Tiene que escribir código manual para cada nuevo usuario?

### Problema C: No define esquema de sesión

⚠️ **Faltan:**
- JWT vs Session cookies
- Token expiration
- Refresh token flow
- CORS y seguridad

### Consecuencia

La seguridad es la parte más difícil de implementar. Esto es "deuda técnica" que asumes subyacentemente.

### Corrección recomendada

Definir completamente:
1. Endpoint de login: POST /api/v1/auth/login con (email, password).
2. Tokens: JWT con expiration de 15min; refresh token de 7 días.
3. Creación de usuarios: POST /api/v1/internal/users (admin only).
4. Seeds iniciales: SQL/código para crear admin en la primera instalación.

---

## 4. CRÍTICO: Datos hardcodeados - No es un producto

### Problema

**Realidad (src/config/business-info.ts):**

```typescript
export const businessInfo: BusinessInfo = {
  name: "Ink Studio",
  tagline: "Arte en tu Piel",
  contact: {
    address: "Calle Principal 123, Ciudad",
    phone: "+34 123 456 789",
    email: "info@tattoostudio.com",
    schedule: "Lun - Sáb: 10:00 - 20:00"
  }
};
```

⚠️ **Especificación (01-vision-producto.md) dice:**

```
"El producto nace para operar un negocio real"
```

PERO todos estos datos están hardcodeados EN EL CÓDIGO. Para cambiar el teléfono, hay que:
1. Editar TypeScript
2. Recompilar el frontend
3. Redeploy

### Consecuencia

- No es un producto reconfigurable.
- El cliente no es autosuficiente.
- Si los datos cambian cada 2 semanas (horarios, teléfono), es un proceso manual tedioso.

### Corrección recomendada

- Crear tabla BusinessSettings en base de datos.
- Crear endpoint para que admin edite: nombre, teléfono, horarios, redes, descripción.
- Frontend consume la API, no config estática.

---

## Matriz de severidad

| Hallazgo | Severidad | Bloqueador | Estimado arreglo |
|----------|-----------|----------|-----------------|
| 1. Admin sin galería | CRÍTICA | Sí | 2-3 sprints |
| 2. IDs UUID vs Int | ALTA | No | 1-2 sprints |
| 3. Auth no definida | CRÍTICA | Sí | 1-2 sprints |
| 4. Datos hardcodeados | ALTA | No | 1 sprint |

---

## Recomendación

✅ **Antes de implementar** (Fase 1), debemos:
1. Revisar y corregir estos puntos.
2. Generar documentos de corrección específicos.
3. Validar con el stakeholder.
4. Rearmar el backlog.

Si avanzamos sin estas correcciones, el riesgo es entregar un producto que no es autosuficiente.
