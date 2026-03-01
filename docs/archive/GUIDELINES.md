# Lineamientos del Proyecto

Este documento define reglas de trabajo y convenciones para mantener el proyecto simple, reutilizable y consistente.

## Objetivos

- Mantener el template fácil de personalizar.
- Evitar duplicación de contenido: texto e identidad del negocio viven en `src/config/`.
- Priorizar type-safety y consistencia en estilos.

## Reglas de estructura

- No crear ni mover carpetas dentro de `src/` sin coordinación.
- Mantener la estructura:

```
src/
|-- components/
|   |-- layout/
|   |-- sections/
|   |-- ui/               # conjunto reducido
|   +-- shared/
|-- config/
|-- hooks/
|-- lib/
|-- styles/
+-- types/
```

## Importaciones

- Usar alias `@/` para imports internos.
- Evitar rutas relativas largas.

## UI y dependencias

- Tailwind CSS: mantener en v3.
- UI base: usar solo los componentes existentes en `src/components/ui/`.

## Configuración centralizada

- No hardcodear textos, URLs, datos del negocio ni listas de servicios dentro de componentes.
- Usar `src/config/*` como fuente de verdad.

## TypeScript

- Evitar `any`.
- Tipar props y datos compartidos usando `src/types/`.

## Calidad

Antes de abrir PR o merge:

- Frontend: `npm run build` y `npm run lint`
- Backend: `dotnet test backend.Tests/backend.Tests.csproj`

## Documentación

- Mantener `docs/README.md` como índice.
- Evitar duplicar reglas entre documentos; enlazar a la fuente de verdad cuando aplique.
