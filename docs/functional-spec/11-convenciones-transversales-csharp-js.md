# Convenciones Transversales C# y JavaScript (Conceptual)

## Objetivo

Evitar fricciones entre backend .NET y frontend JavaScript/TypeScript mediante reglas de nombre, tipado y estructura compartidas.

## Idioma y nomenclatura canonica

- Lenguaje de dominio tecnico: ingles.
- Texto de negocio y UX: espanol.
- Entidades y DTOs: nombres tecnicos en ingles.

## Politica oficial de idioma

- Todo identificador de codigo debe ir en ingles:
  - clases, interfaces, enums, propiedades, variables
  - tablas, columnas, migraciones
  - endpoints, rutas y parametros
  - claves JSON
- Solo los mensajes y textos para usuarios pueden ir en espanol:
  - mensajes de validacion
  - mensajes de error de negocio
  - textos de interfaz
- No mezclar espanol e ingles dentro del mismo identificador tecnico.

## Reglas de nombres

- Archivos de especificacion: `kebab-case`.
- Rutas API: `kebab-case` en recursos.
- JSON de API: `camelCase`.
- Clases C#: `PascalCase`.
- Propiedades C#: `PascalCase` (serializadas a `camelCase` para JSON).
- Variables locales C#: `camelCase`.
- Variables y funciones JavaScript/TypeScript: `camelCase`.
- Tipos TypeScript e interfaces: `PascalCase`.
- Constantes compartidas: `UPPER_SNAKE_CASE` cuando aplique.

## class, struct y var (C#)

- `class`: usar por defecto para entidades de dominio, DTOs y servicios.
- `struct`: solo para value objects pequenos, inmutables y sin identidad (uso excepcional).
- `record` o `record class`: recomendado para DTOs inmutables cuando tenga sentido.
- `var`: permitido solo cuando el tipo es obvio en la misma linea; evitar si oculta intencion.

## Espacios, minusculas y normalizacion

- No usar espacios en nombres tecnicos (campos, rutas, archivos).
- Evitar acentos en claves tecnicas y nombres de propiedades.
- Mantener minusculas en rutas y `camelCase` en JSON.
- Los enums serializados a string deben usar formato estable (ejemplo: `new`, `inReview`, `closed`).

## IDs, fechas y tipos base

- Identificadores: `int` en MVP por compatibilidad con sistema actual.
- Migracion futura a `Guid` solo si existe necesidad real de escalamiento/distribucion.
- Fechas: `ISO-8601` UTC.
- Flags booleanos: prefijo `is` o `has`.

## Integridad de modelo y negocio

- Definir transiciones validas de estado (state machine simple).
- Registrar auditoria minima de cambios de estado.
- Separar claramente:
  - validacion de formato (request)
  - validacion de negocio (dominio)

## Regla de compatibilidad

Si una decision rompe estas convenciones, debe documentarse el motivo y el mapeo explicito C# <-> JSON <-> TypeScript antes de implementacion.
