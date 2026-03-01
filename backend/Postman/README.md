# Ink Studio API - Colección de Postman

Esta carpeta contiene una colección completa de Postman para probar el backend de Ink Studio.

## ?? Archivos

- **`Ink-Studio-API.postman_collection.json`** - Colección principal con 6 requests de prueba
- **`Ink-Studio-Local.postman_environment.json`** - Variables de entorno para desarrollo local
- **`README.md`** - Este archivo (guía de uso)

## ?? Cómo Usar

### 1. Importar en Postman

1. Abre Postman Desktop o Postman Web
2. Click en **Import** (botón superior izquierdo)
3. Arrastra los archivos `.json` o selecciónalos:
   - `Ink-Studio-API.postman_collection.json`
   - `Ink-Studio-Local.postman_environment.json`
4. Click en **Import**

### 2. Configurar Ambiente

1. En Postman, selecciona el ambiente **"Ink Studio - Local Development"** en el dropdown superior derecho
2. Verifica las variables:
   - `baseUrl`: `http://localhost:7000` (puerto del backend)
   - `frontendUrl`: `http://localhost:5173` (puerto del frontend Vite)

### 3. Iniciar el Backend

Antes de ejecutar los requests, asegúrate de que el backend esté corriendo:

```bash
cd backend
dotnet run
```

El backend debe estar escuchando en `http://localhost:7000`

### 4. Ejecutar Requests

Los requests están ordenados del 1 al 6 según su propósito:

---

## ?? Requests Incluidos

### ? 1. POST Contact - Valid Request
**Propósito:** Probar el flujo exitoso completo

**Datos:**
- Nombre: Juan Pérez
- Email: juan.perez@example.com (válido)
- Teléfono: +56912345678
- Mensaje: "Hola, me gustaría agendar una cita para un tatuaje"
- Quiere cita: Sí

**Resultado esperado:**
- ? HTTP 200 OK
- ? Respuesta JSON con `success: true`
- ? Mensaje persistido en base de datos
- ? Email enviado al estudio

---

### ? 2. POST Contact - Invalid Email
**Propósito:** Validar que la validación de email funciona

**Datos:**
- Email: "email-invalido" (sin @ ni dominio)

**Resultado esperado:**
- ? HTTP 400 Bad Request
- ? Mensaje de error de validación

---

### ? 3. POST Contact - Missing Fields
**Propósito:** Validar que los campos requeridos se validan

**Datos:**
- Name: "" (campo vacío)

**Resultado esperado:**
- ? HTTP 400 Bad Request
- ? Mensaje indicando que el nombre es requerido

---

### ?? 4. POST Contact - Large Payload (>10KB)
**Propósito:** Validar límite de tamaño de payload

**Nota:** Para probar correctamente, necesitas modificar el campo `message` con un texto de más de 11KB.

**Resultado esperado:**
- ?? HTTP 413 Payload Too Large O HTTP 400 Bad Request
- ?? El middleware debe rechazar el request antes de procesarlo

---

### ?? 5. POST Contact - Rate Limiting Test
**Propósito:** Validar que el rate limiting funciona (10 req/min)

**Cómo probar:**
1. Abre el **Collection Runner** en Postman
2. Selecciona este request
3. Configura **Iterations: 11**
4. Click en **Run**

**Resultado esperado:**
- ? Primeras 10 requests: HTTP 200 OK
- ? Request 11 en adelante: HTTP 429 Too Many Requests

---

### ?? 6. POST Contact - CORS Test (Invalid Origin)
**Propósito:** Validar configuración de CORS

**Headers:**
- `Origin: http://malicious-site.com` (origen no permitido)

**Resultado esperado:**
- ?? La respuesta NO debe incluir header `Access-Control-Allow-Origin` con valor `http://malicious-site.com`
- ?? El navegador bloquearía este request por CORS (Postman lo permite pero muestra los headers)

---

## ?? Variables de Entorno

Puedes modificar las variables según tu configuración:

### Local Development (default)
```json
{
  "baseUrl": "http://localhost:7000",
  "frontendUrl": "http://localhost:5173"
}
```

### Staging/Production
Crea un nuevo ambiente y configura:
```json
{
  "baseUrl": "https://api-ink-studio.azurewebsites.net",
  "frontendUrl": "https://ink-studio.vercel.app"
}
```

---

## ?? Resultados Esperados

### Flujo Exitoso (Request 1)
```json
{
  "success": true,
  "message": "Mensaje recibido correctamente. Nos pondremos en contacto contigo pronto.",
  "id": 1
}
```

### Error de Validación (Requests 2 y 3)
```json
{
  "success": false,
  "message": "Datos de entrada inválidos"
}
```

O respuesta de FluentValidation:
```json
{
  "errors": {
    "Email": ["El formato del correo electrónico no es válido"]
  }
}
```

### Rate Limiting (Request 5 después de 10 intentos)
```
HTTP 429 Too Many Requests
Retry-After: 60

Quota exceeded. Maximum allowed: 10 per 1m.
```

---

## ?? Troubleshooting

### ? Error: "Connection refused"
**Causa:** El backend no está corriendo  
**Solución:** Ejecuta `dotnet run` en la carpeta `backend/`

### ? Error: "CORS policy blocked"
**Causa:** Origin no permitido o CORS mal configurado  
**Solución:** Verifica `appsettings.json` -> `CorsSettings:AllowedOrigins`

### ? Error: "Database connection failed"
**Causa:** SQLite no puede crear/acceder al archivo de DB  
**Solución:** Verifica permisos en la carpeta del proyecto

### ? Error: "Email send failed"
**Causa:** Credenciales de SMTP/SendGrid incorrectas  
**Solución:** Verifica configuración de email en `appsettings.Development.json`

---

## ?? Recursos Adicionales

- **Documentación del API:** Ver `backend/README.md`
- **Design Document:** Ver `.kiro/specs/tattoo-studio-backend/design.md`
- **Pruebas de Integración:** Ver `backend.Tests/Integration/`

---

## ? Checklist de Pruebas

Antes de considerar el backend listo para producción, verifica que todos estos requests funcionen:

- [ ] ? Request 1: Valid Request ? 200 OK
- [ ] ? Request 2: Invalid Email ? 400 Bad Request
- [ ] ? Request 3: Missing Fields ? 400 Bad Request
- [ ] ?? Request 4: Large Payload ? 413 Payload Too Large
- [ ] ?? Request 5: Rate Limiting ? 429 después de 10 requests
- [ ] ?? Request 6: CORS ? Headers correctos

---

**Última actualización:** 02-12-2025  
**Versión del API:** 1.0.0  
**Backend:** ASP.NET Core 8.0
