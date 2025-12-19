# Integración Frontend-Backend

Esta guía explica cómo configurar el frontend para consumir el backend actual de Ink Studio.

Referencias:

- API: `API-REST.md`
- Seguridad (CORS, límites, rate limiting): `SECURITY.md`

---

## 1) Configurar el frontend

El frontend usa una base URL configurable:

- `VITE_API_BASE_URL`

Ejemplo (desarrollo):

```env
VITE_API_BASE_URL=https://localhost:7000
```

Nota: el puerto puede variar. Usa el valor real entregado por `dotnet run --project backend`.

---

## 2) Configurar CORS en el backend

En el backend, agrega el origen del frontend a `CorsSettings:AllowedOrigins` (por ejemplo):

- `http://localhost:5173` (Vite dev server)
- Dominio productivo del frontend (cuando aplique)

Esto se define en `backend/appsettings*.json`.

---

## 3) Consumir endpoints desde el frontend

### Enviar contacto (POST /api/contact)

Ejemplo mínimo:

```ts
const baseUrl = import.meta.env.VITE_API_BASE_URL;

await fetch(`${baseUrl}/api/contact`, {
  method: "POST",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify({
    name: "Juan Perez",
    email: "juan@example.com",
    phone: "+56 9 1234 5678",
    message: "Hola, me interesa una cotizacion.",
    wantsAppointment: false,
  }),
});
```

### Obtener galería (GET /api/gallery)

```ts
const baseUrl = import.meta.env.VITE_API_BASE_URL;
const response = await fetch(`${baseUrl}/api/gallery`);
const images = await response.json();
```

---

## Verificación

Para verificar que la integración funciona:

1. Ejecutar backend: `dotnet run --project backend`
2. Ejecutar frontend: `npm run dev`
3. Probar formulario de contacto desde el navegador
4. Ver logs del backend para confirmar recepción

---

## Referencias adicionales

- Ejecutar backend local: `BACKEND-QUICKSTART.md`
- Configuración completa backend: `backend/README.md`
- Deployment: `DEPLOYMENT.md`
