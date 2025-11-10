# 📁 Estructura del Proyecto

## Resumen General

La aplicación está organizada por dominios funcionales; cada carpeta agrupa componentes, configuración y utilidades especializadas para la landing page.

```text
.
├── public/                  # Recursos estáticos servidos tal cual (favicon, manifest, imágenes)
├── src/
│   ├── components/          # Componentes React
│   │   ├── layout/          # Elementos estructurales (Navbar, Footer)
│   │   ├── sections/        # Secciones de página (Hero, Services, Gallery, About, Contact)
│   │   └── ui/              # Biblioteca reutilizable (Button, Card, Input, etc.)
│   ├── config/              # Contenido dinámico (textos, navegación, imágenes, servicios)
│   ├── hooks/               # Hooks personalizados para interacción/UI
│   ├── lib/                 # Helpers y utilidades puras
│   ├── styles/              # Estilos globales y tokens
│   ├── types/               # Tipos TypeScript compartidos
│   ├── App.tsx              # Composición principal de secciones
│   └── main.tsx             # Punto de entrada de React/Vite
├── docs/                    # Planes y documentación por fase
├── guidelines/              # Lineamientos adicionales para contributors
├── package.json             # Dependencias y scripts npm
└── vite.config.ts           # Configuración de bundling y alias
```

## Detalle por Carpeta

### `src/components`

- **layout/**: contiene `Navbar.tsx` y `Footer.tsx`, responsables de navegación, CTA y pie de página. Ambos consumen contenido de `config/` y exponen props propias.
- **sections/**: alberga secciones principales, cada una aislada con su lógica y estilos. Utilizan componentes UI y datos de `config/`.
- **ui/**: colección de componentes reutilizables (Button, Card, Input, Textarea, Label, ImageWithFallback). Incluye documentación JSDoc para integradores.

### `src/config`

Define el contenido editable sin tocar los componentes:

- `business-info.ts`: datos corporativos, contacto, métricas y redes.
- `content.ts`: textos y copy de cada sección.
- `navigation.ts`: enlaces del menú y texto de CTA.
- `images.ts`: referencias a assets utilizados.
- `services.ts`: catálogo con iconos y descripciones.
- `api.ts`: utilidades para alternar entre mock y endpoints reales.

### `src/hooks`

- `use-active-section.ts`: detecta la sección visible para resaltar en Navbar.
- `use-mobile.ts`: helpers para comportamiento responsive (si aplica).

### `src/lib`

- `utils.ts`: utilidades genéricas (p.ej. `cn` para combinar clases).

### `src/styles`

- `globals.css`: variables CSS, animaciones, reset y configuración Tailwind personalizada.

### `src/types`

- `index.ts`: repositorio central de interfaces TypeScript documentadas, reutilizadas por `config` y componentes.

## Flujo de Datos

1. **Contenido**: `config/` define textos, listas y medios.
2. **Tipos**: `types/index.ts` asegura coherencia en estructuras.
3. **Componentes de UI**: `components/ui` exponen piezas estilizadas.
4. **Secciones/Layout**: `components/sections` y `components/layout` ensamblan UI con datos provenientes de `config`.
5. **App.tsx**: importa y renderiza todas las secciones en orden definido.

## Directrices de Desarrollo

- Utiliza el alias `@` para importar desde `src/` (`vite.config.ts`).
- Documenta nuevos componentes con JSDoc siguiendo el patrón existente.
- Mantén la segregación: lógica de datos en `config`, presentación en `components`.
- Ejecuta `npm run build` antes de merges para garantizar compatibilidad.

## Recursos Relacionados

- `docs/03-FASE-3-DOCUMENTACION.md`: objetivos y checklist de documentación.
- `docs/CUSTOMIZATION.md`: guía para adaptar el proyecto a nuevos clientes.
