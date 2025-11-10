/**
 * Tipos TypeScript centralizados para el proyecto Ink Studio.
 *
 * Declara las interfaces compartidas entre configuraciones, componentes y hooks
 * para garantizar consistencia tipada.
 */

import type { LucideIcon } from "lucide-react";

/**
 * Datos de un método de contacto mostrado al usuario final.
 *
 * @property icon Icono representativo (Lucide) del método de contacto.
 * @property title Encabezado corto (por ejemplo, "Teléfono").
 * @property value Valor textual del contacto (número, email, dirección).
 */
export interface ContactInfo {
  icon: LucideIcon;
  title: string;
  value: string;
}

/**
 * Servicio destacado del estudio de tatuajes.
 *
 * @property icon Icono asociado al servicio.
 * @property title Nombre del servicio.
 * @property description Descripción breve orientada a marketing.
 */
export interface Service {
  icon: LucideIcon;
  title: string;
  description: string;
}

/**
 * Métrica o estadística del negocio mostrada en la sección About.
 *
 * @property icon Icono alusivo a la métrica.
 * @property value Cifra destacada (ej. "+500").
 * @property label Descripción que contextualiza el valor.
 */
export interface Stat {
  icon: LucideIcon;
  value: string;
  label: string;
}

/**
 * Imagen renderizada dentro de la galería interactiva.
 *
 * @property src Ruta o URL de la imagen.
 * @property alt Texto alternativo accesible.
 */
export interface GalleryImage {
  src: string;
  alt: string;
}

/**
 * Elemento utilizado en la navegación principal.
 *
 * @property label Texto visible del enlace.
 * @property href Ancla o URL de destino (ej. "#services").
 */
export interface MenuItem {
  label: string;
  href: string;
}

/**
 * Datos generales de la marca utilizados en múltiples secciones.
 *
 * @property name Nombre comercial mostrado en Navbar y Footer.
 * @property tagline Eslogan breve.
 * @property description Resumen del negocio.
 * @property foundedYear Año de fundación (número entero).
 * @property contact Información de contacto estructurada.
 * @property contact.address Dirección física.
 * @property contact.phone Número de contacto.
 * @property contact.email Correo electrónico.
 * @property contact.schedule Horario de atención.
 * @property stats Estadísticas destacadas.
 * @property social Redes sociales opcionales (URL completas).
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
 * Datos recolectados desde el formulario de contacto.
 *
 * @property name Nombre completo del remitente (obligatorio).
 * @property email Correo electrónico válido.
 * @property phone Número telefónico opcional.
 * @property message Mensaje principal (mínimo 10 caracteres).
 */
export interface ContactFormData {
  name: string;
  email: string;
  phone: string;
  message: string;
}

