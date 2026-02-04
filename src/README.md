# Frontend - Ink Studio

Frontend de la landing page de Ink Studio, construido con React 18 + TypeScript + Vite.

**Versión:** 0.95.0  
**Estado:** Producción Ready  
**Bundle:** 75.14 KB gzipped  
**Performance:** Lighthouse 100

---

## Requisitos

- Node.js 18+ y npm

---

## Desarrollo

Desde la raíz del repositorio:

```bash
npm install
npm run dev
```

La aplicación estará disponible en: `http://localhost:5173`

---

## Build

```bash
# Build de producción
npm run build

# Preview del build
npm run preview
```

---

## Lint

```bash
npm run lint
```

---

## Configuración

El frontend consume el backend usando variable de entorno:

```env
VITE_API_BASE_URL=http://localhost:5177
```

Crear archivo `.env` en la raíz del proyecto con la URL del backend.

---

## Estructura

Ver documentación completa en:

- [docs/ARCHITECTURE.md](../docs/ARCHITECTURE.md) - Arquitectura del sistema
- [docs/DEVELOPMENT-GUIDE.md](../docs/DEVELOPMENT-GUIDE.md) - Convenciones y testing

---

## Documentación Adicional

- **Setup inicial:** [docs/GETTING-STARTED.md](../docs/GETTING-STARTED.md)
- **API Reference:** [docs/API-REFERENCE.md](../docs/API-REFERENCE.md)
- **Personalización:** [docs/CUSTOMIZATION.md](../docs/CUSTOMIZATION.md)
- **Índice completo:** [docs/README.md](../docs/README.md)

---

**Última actualización:** 2025-01-09
