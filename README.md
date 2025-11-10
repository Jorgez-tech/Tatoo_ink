# 🎨 Ink Studio - Landing Page

Landing page profesional para estudio de tatuajes, construida con React + TypeScript + Tailwind CSS.

## 🚀 Stack Tecnológico

- **Framework:** React 19 + Vite
- **Lenguaje:** TypeScript
- **Estilos:** Tailwind CSS v3 + utilidades personalizadas
- **Componentes UI:** Radix UI + Lucide Icons

## ✅ Requisitos Previos

- Node.js 18 LTS o superior
- npm 9+ (instalado junto con Node)

## 📦 Instalación

```bash
npm install
```

## 🔧 Desarrollo

```bash
npm run dev
```

Abre [http://localhost:5173](http://localhost:5173) para ver la aplicación en modo desarrollo.

## 🏗️ Build y Deploy

```bash
npm run build
npm run preview # opcional: sirve la build localmente
```

El directorio `dist/` contiene los artefactos listos para despliegue. En escenarios con backend ASP.NET Core, publica el contenido de `dist/` dentro de `wwwroot/` o configura proxy inverso según necesidades.

## 📁 Estructura del Proyecto

```text
src/
├── components/
│   ├── layout/      # Navbar, Footer y elementos de estructura
│   ├── sections/    # Secciones principales (Hero, Services, Gallery, About, Contact)
│   └── ui/          # Componentes reutilizables (Button, Card, Input, etc.)
├── config/          # Contenido y datos de negocio (textos, navegación, imágenes)
├── hooks/           # Hooks personalizados (p.ej. useActiveSection)
├── lib/             # Utilidades compartidas (helpers, formateadores)
├── styles/          # Estilos globales y configuración CSS
└── types/           # Definiciones TypeScript compartidas
```

Consulta `docs/STRUCTURE.md` para una explicación detallada de carpetas y flujos de datos.

## 🧩 Componentes Destacados

- **Navbar:** navegación fija con menú responsive y highlight de sección activa.
- **Hero:** bloque principal con imagen full-screen y CTAs configurables.
- **Services / Gallery / About / Contact:** secciones dinámicas basadas en datos de `src/config`.
- **UI Library:** componentes estilizados (Button, Card, Input, Textarea, Label, ImageWithFallback) con documentación JSDoc.

## 🤝 Contribución

1. Crea una rama feature desde `main` siguiendo el formato `feature/<nombre>`.
2. Asegura commits convencionales (`feat:`, `docs:`, `refactor:`, etc.).
3. Ejecuta `npm run build` antes de abrir un PR para validar que no existan errores.

## 📚 Documentación Relacionada

- `docs/03-FASE-3-DOCUMENTACION.md`: lineamientos completos de esta fase.
- `docs/CUSTOMIZATION.md`: guía para personalizar la landing para nuevos clientes.
- `docs/STRUCTURE.md`: referencia estructural ampliada del proyecto.
