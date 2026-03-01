# API REST - Ink Studio

Esta referencia describe la API del backend. Está pensada para consumo desde el frontend y para pruebas manuales (Postman/cURL).

## Base URL

En desarrollo local, la URL depende del puerto configurado por ASP.NET Core. Referencia:

- Revisar salida de `dotnet run --project backend`
- Swagger suele estar disponible en `/swagger` en entorno Development

En el frontend, la API se configura mediante:

- `VITE_API_BASE_URL`

## Convenciones

- Contenido JSON: `Content-Type: application/json`
- Errores de validación retornan `400` con `application/problem+json` (ValidationProblemDetails) cuando aplica.
- Controles de seguridad pueden retornar `413` (payload) o `429` (rate limiting).

## Endpoints

### POST /api/contact

Envía un mensaje de contacto. Puede incluir intención de agendar.

#### Request

Body:

```json
{
  "name": "Juan Perez",
  "email": "juan@example.com",
  "phone": "+56 9 1234 5678",
  "message": "Me gustaria agendar una cita.",
  "wantsAppointment": true
}
```

Campos:

- `name` (string, requerido)
- `email` (string, requerido)
- `phone` (string, requerido por reglas actuales del frontend; el backend valida formato y requeridos según configuración/validator)
- `message` (string, requerido)
- `wantsAppointment` (boolean, requerido)

#### Responses

200 OK

```json
{
  "success": true,
  "message": "Mensaje recibido correctamente. Nos pondremos en contacto contigo pronto.",
  "id": 123
}
```

400 Bad Request

- Casos típicos: faltan campos requeridos, email inválido, longitudes fuera de rango.
- Respuesta típica: `application/problem+json` con detalle por campo.

413 Payload Too Large

- Ocurre si el cuerpo excede el tamaño permitido por seguridad (por defecto ~10KB).

429 Too Many Requests

- Ocurre si se supera el límite de requests por IP (por defecto 10/min).

500 Internal Server Error

- Errores inesperados, o fallas no controladas al procesar.

### GET /api/gallery

Retorna la lista de imágenes de la galería.

#### Response

200 OK

```json
[
  {
    "id": 1,
    "src": "https://...",
    "alt": "Tattoo Art Design 1",
    "category": "Art",
    "createdAt": "2025-12-18T00:00:00Z"
  }
]
```

Notas:

- El backend inicializa imágenes de ejemplo en SQLite cuando la base está vacía.
- El endpoint retorna la lista ordenada por fecha de creación (desc).

## Pruebas manuales

### cURL (ejemplo contacto)

```bash
curl -X POST "$VITE_API_BASE_URL/api/contact" \
  -H "Content-Type: application/json" \
  -d '{"name":"Juan Perez","email":"juan@example.com","phone":"+56 9 1234 5678","message":"Hola","wantsAppointment":false}'
```

### Postman

El repositorio incluye una colección en `backend/Postman/`.
