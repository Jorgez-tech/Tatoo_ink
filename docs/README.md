# Ink Studio - Landing Page

Landing page profesional para estudio de tatuajes, construida con React + TypeScript + Tailwind CSS.

## Stack Tecnologico

- **Framework:** React 18 + Vite
- **Lenguaje:** TypeScript
- **Estilos:** Tailwind CSS v3 + utilidades personalizadas
- **Componentes UI:** Radix UI + Lucide Icons
- **Validacion:** React Hook Form
- **Iconos:** Lucide React

## Requisitos Previos

- Node.js 18 LTS o superior
- npm 9+ (instalado junto con Node)

## Instalacion

```bash
npm install
```

## Desarrollo

```bash
npm run dev
```

Abre [http://localhost:5173](http://localhost:5173) para ver la aplicacion en modo desarrollo.

## Build y Deploy

```bash
npm run build
npm run preview
```

El directorio `dist/` contiene los artefactos listos para despliegue. En escenarios con backend ASP.NET Core, publica el contenido de `dist/` dentro de `wwwroot/` o configura proxy inverso segun necesidades.

## Estructura del Proyecto

```text
src/
|-- components/
|   |-- layout/      # Navbar, Footer
|   |-- sections/    # Hero, Services, Gallery, About, Contact
|   |-- ui/          # Button, Card, Input, Textarea, Label, ImageWithFallback
|   +-- shared/      # Componentes compartidos futuros
|-- config/          # Configuracion centralizada
|   |-- business-info.ts    # Datos del negocio
|   |-- content.ts          # Textos de secciones
|   |-- images.ts           # Rutas de imagenes
|   |-- navigation.ts       # Menu y navegacion
|   |-- services.ts         # Servicios ofrecidos
|   +-- api.ts              # Configuracion de backend
|-- hooks/           # useActiveSection
|-- lib/             # utils.ts (cn helper)
|-- styles/          # globals.css
+-- types/           # index.ts (interfaces TypeScript)
```

Consulta `docs/STRUCTURE.md` para detalles completos.

## Personalizacion

Para adaptar esta landing a un nuevo cliente:

1. **Informacion del negocio:** Edita `src/config/business-info.ts`
2. **Contenido de secciones:** Edita `src/config/content.ts`
3. **Servicios:** Edita `src/config/services.ts`
4. **Imagenes:** Actualiza rutas en `src/config/images.ts`
5. **Colores y estilos:** Modifica variables CSS en `src/styles/globals.css`

Ver `docs/CUSTOMIZATION.md` para guia detallada.

## Caracteristicas

- Diseno responsive (movil, tablet, desktop)
- Navegacion con scroll spy (seccion activa destacada)
- Smooth scroll global
- Lazy loading de imagenes con placeholders
- Lightbox interactivo con navegacion por teclado
- Formulario validado con React Hook Form
- Animaciones fadeIn/fadeInUp
- Preparado para backend ASP.NET Core
- Componentes documentados con JSDoc

## Contribucion

1. Crea una rama feature desde `main` siguiendo el formato `feature/<nombre>`
2. Usa commits convencionales (`feat:`, `fix:`, `docs:`, `refactor:`, etc.)
3. Ejecuta `npm run build` antes de abrir un PR

## Documentacion

- `docs/STRUCTURE.md` - Arquitectura del proyecto
- `docs/CUSTOMIZATION.md` - Guia de personalizacion
- `docs/BACKEND-INTEGRATION.md` - Integracion con ASP.NET Core
- `docs/NEXT-STEPS.md` - Estado actual y proximos pasos

## Prohibicion de emojis

**NOTA IMPORTANTE:** Por decision de estilo y compatibilidad, los emojis estan prohibidos en todo el proyecto y documentacion. Utiliza solo texto plano y simbolos ASCII. Si encuentras algun emoji, eliminalo y registra el cambio en `docs/NEXT-STEPS.md`.

## Licencia

Este proyecto es un prototipo/demo para uso interno y clientes.
