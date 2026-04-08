# Semantica de Errores

## Objetivo

Definir respuestas de error predecibles para mejorar soporte, observabilidad y experiencia de integracion.

## Estructura de error (conceptual)

```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Validacion fallida",
    "details": [
      {
        "field": "email",
        "issue": "Formato invalido"
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
- Claves tecnicas (`code`, `field`, `traceId`) en ingles; mensajes en espanol.

## Criterios de uso (conceptual)

- 400 para errores de formato/sintaxis (JSON invalido, tipo incorrecto).
- 422 para reglas de negocio (transicion de estado invalida, rol no permitido por regla funcional).
- 409 para colisiones de concurrencia o estado ya modificado.
- `code` debe mantenerse estable entre versiones para clientes.
