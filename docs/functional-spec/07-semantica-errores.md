# Semantica de Errores

## Objetivo

Definir respuestas de error predecibles para mejorar soporte, observabilidad y experiencia de integracion.

## Estructura de error (conceptual)

```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Validation failed",
    "details": [
      {
        "field": "email",
        "issue": "Invalid format"
      }
    ],
    "traceId": "abc-123"
  }
}
```

## Tabla de estados HTTP

- 400: request invalido o datos mal formados.
- 401: no autenticado.
- 403: autenticado sin permiso suficiente.
- 404: recurso inexistente.
- 409: conflicto de estado.
- 422: validacion de negocio no cumplida.
- 429: limite de solicitudes excedido.
- 500: error interno no controlado.

## Reglas

- `code` estable y documentado.
- `message` entendible por cliente tecnico.
- `details` opcional para validaciones por campo.
- `traceId` obligatorio para soporte y logs.
