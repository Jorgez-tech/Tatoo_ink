/**
 * Configuración de navegación
 * Centraliza la estructura de menús y enlaces
 */

import type { MenuItem } from "@/types";

/**
 * Items del menú principal
 */
export const menuItems: MenuItem[] = [
  { label: "Inicio", href: "#" },
  { label: "Servicios", href: "#servicios" },
  { label: "Galería", href: "#galeria" },
  { label: "Nosotros", href: "#nosotros" },
  { label: "Contacto", href: "#contacto" }
];

/**
 * Texto del botón CTA en el navbar
 */
export const navbarCtaText = "Reservar Cita";

