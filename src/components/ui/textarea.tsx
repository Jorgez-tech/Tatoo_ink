import * as React from "react";

import { cn } from "../../lib/utils";

/**
 * Campo de texto multilínea con estilos cohesivos y estados accesibles.
 *
 * Mantiene altura mínima, muestra indicador de carga mediante utilidades y
 * propaga props estándar de `<textarea>` para integrarse con formularios.
 *
 * @component
 * @example
 * return (
 *   <Textarea rows={4} placeholder="Describe tu idea" />
 * );
 */
const Textarea = React.forwardRef<HTMLTextAreaElement, React.ComponentProps<"textarea">>(
  ({ className, ...props }, ref) => {
    return (
      <textarea
        className={cn(
          "resize-none border-input placeholder:text-muted-foreground focus-visible:border-ring focus-visible:ring-ring/50 aria-invalid:ring-destructive/20 dark:aria-invalid:ring-destructive/40 aria-invalid:border-destructive dark:bg-input/30 flex field-sizing-content min-h-16 w-full rounded-md border bg-input-background px-3 py-2 text-base transition-[color,box-shadow] outline-none focus-visible:ring-[3px] disabled:cursor-not-allowed disabled:opacity-50 md:text-sm",
          className,
        )}
        ref={ref}
        {...props}
      />
    );
  }
);
Textarea.displayName = "Textarea";

export { Textarea };
