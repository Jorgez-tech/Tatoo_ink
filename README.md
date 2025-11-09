# Ink Studio (Frontend)

Landing page profesional construida con React + TypeScript + Vite + Tailwind CSS.

## Requisitos
- Node.js 18+ (recomendado LTS)

## Comandos
- Desarrollo: `npm install` y luego `npm run dev`
- Build producción: `npm run build` (salida en `dist/`)
- Preview local de build: `npm run preview`

## Estructura clave
- `src/App.tsx`: composición de secciones (Hero, Services, Gallery, About, Contact)
- `src/components/sections/*`: secciones de la landing
- `src/components/layout/*`: Navbar, Footer
- `src/components/ui/*`: componentes UI reutilizables (button, card, input, label, textarea, ImageWithFallback)
- `src/config/*`: contenido/textos, navegación, servicios, imágenes
- `src/styles/globals.css`: tokens de diseño, animaciones, tema

## Notas de desarrollo
- Alias `@` apunta a `src/` (ver `vite.config.ts`).
- Tailwind purga clases desde `./index.html` y `./src/**/*.{js,ts,jsx,tsx}` (ver `tailwind.config.js`).
- Scroll suave y “sección activa” en Navbar: `use-active-section` + IDs de sección (por ejemplo `#home`).

## Integración con backend (más adelante)
- Este repo solo cubre el frontend. Cuando exista ASP.NET Core, se podrá servir `dist/` desde `wwwroot/` o usar proxy en desarrollo.
