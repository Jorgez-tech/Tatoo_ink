/**
 * Tipos TypeScript para el proyecto
 * Centraliza todas las definiciones de tipos reutilizables
 */

import type { LucideIcon } from "lucide-react";

/**
 * Información de contacto
 */
export interface ContactInfo {
  icon: LucideIcon;
  title: string;
  value: string;
}

/**
 * Servicio ofrecido
 */
export interface Service {
  icon: LucideIcon;
  title: string;
  description: string;
}

/**
 * Estadística o métrica
 */
export interface Stat {
  icon: LucideIcon;
  value: string;
  label: string;
}

/**
 * Imagen de la galería
 */
export interface GalleryImage {
  src: string;
  alt: string;
}

/**
 * Item del menú de navegación
 */
export interface MenuItem {
  label: string;
  href: string;
}

/**
 * Información del negocio
 */
export interface BusinessInfo {
  name: string;
  tagline: string;
  description: string;
  foundedYear: number;
  contact: {
    address: string;
    phone: string;
    email: string;
    schedule: string;
  };
  stats: Stat[];
  social: {
    instagram?: string;
    facebook?: string;
    twitter?: string;
  };
}

/**
 * Datos del formulario de contacto
 */
export interface ContactFormData {
  name: string;
  email: string;
  phone: string;
  message: string;
}

