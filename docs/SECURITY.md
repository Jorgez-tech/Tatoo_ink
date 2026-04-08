# Seguridad - Ink Studio

Este documento describe los controles de seguridad implementados y cómo operan en runtime.

## Principios

- Validar todo input (frontend y backend).
- Reducir superficie de ataque con límites explícitos.
- Preferir errores consistentes y logging útil para diagnóstico.

## Controles implementados

### Validación de entrada

- **Backend:** FluentValidation valida el DTO de contacto.
- **Frontend:** validación de formulario (React Hook Form) para feedback rápido al usuario.

### Sanitización (XSS)

- El backend inspecciona payload JSON y aplica sanitización.
- Si detecta contenido potencialmente peligroso en el cuerpo, responde con `400`.

Objetivo:

- Evitar almacenamiento o procesamiento de contenido HTML malicioso.

### Límite de tamaño de payload

- El backend aplica un límite configurable (por defecto ~10KB).
- Si el request excede el máximo permitido, puede retornar `413 Payload Too Large`.

Configuración:

- `Security:MaxPayloadSizeKB`

### Rate limiting

- Se limita el número de requests por IP por minuto.
- Cuando se excede el umbral, se responde con `429 Too Many Requests`.

Configuración:

- `RateLimiting:MaxRequestsPerMinute`
- `RateLimiting:EnableRateLimiting`

### CORS

- Se restringen orígenes permitidos para requests desde navegador.
- Orígenes permitidos configurados en `CorsSettings:AllowedOrigins`.

Buenas prácticas:

- En producción, permitir solo el dominio del frontend.
- Evitar `AllowAnyOrigin` cuando se usan credenciales.

### Manejo global de excepciones

- Middleware global captura excepciones no controladas.
- Evita filtrar stack traces al cliente.
- Centraliza logging de errores.

## Configuración recomendada (producción)

- Usar valores reales de email vía variables de entorno o secretos del proveedor.
- Mantener `AllowedOrigins` acotado.
- Asegurar persistencia del archivo SQLite (o migrar a un motor administrado si el proyecto crece).

## Qué NO cubre este proyecto (por diseño)

- Autenticación/autorización de usuarios (no es necesario para una landing).
- Gestión de roles.
- Protección CSRF (no aplica si no se usan cookies de sesión; si se incorporan, se debe revisar).
