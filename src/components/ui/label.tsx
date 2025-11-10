"use client";

import * as React from "react";
import * as LabelPrimitive from "@radix-ui/react-label";

import { cn } from "../../lib/utils";

/**
 * Etiqueta accesible basada en Radix UI con estilos consistentes.
 *
 * Propaga props estándar de `LabelPrimitive.Root`, soporta estado deshabilitado
 * y asegura alineación con el diseño mediante utilidades `cn`.
 *
 * @component
 * @example
 * return (
 *   <Label htmlFor="email">Correo electrónico</Label>
 * );
 */
function Label({
  className,
  ...props
}: React.ComponentProps<typeof LabelPrimitive.Root>) {
  return (
    <LabelPrimitive.Root
      data-slot="label"
      className={cn(
        "flex items-center gap-2 text-sm leading-none font-medium select-none group-data-[disabled=true]:pointer-events-none group-data-[disabled=true]:opacity-50 peer-disabled:cursor-not-allowed peer-disabled:opacity-50",
        className,
      )}
      {...props}
    />
  );
}

export { Label };
