/**
 * Configuración de información del negocio
 * Centraliza todos los datos relacionados con el negocio
 */

import { Award, Clock, Heart, MapPin, Phone, Mail } from "lucide-react";
import type { BusinessInfo, Stat, ContactInfo } from "@/types";

/**
 * Información principal del negocio
 */
export const businessInfo: BusinessInfo = {
  name: "Ink Studio",
  tagline: "Arte en tu Piel",
  description: "Transformamos tus ideas en obras de arte permanentes. Más de 10 años de experiencia creando tatuajes únicos y personalizados.",
  foundedYear: 2014,
  contact: {
    address: "Calle Principal 123, Ciudad",
    phone: "+34 123 456 789",
    email: "info@tattoostudio.com",
    schedule: "Lun - Sáb: 10:00 - 20:00"
  },
  stats: [
    { icon: Award, value: "10+", label: "Años de Experiencia" },
    { icon: Clock, value: "5000+", label: "Tatuajes Realizados" },
    { icon: Heart, value: "100%", label: "Clientes Satisfechos" }
  ] as Stat[],
  social: {
    instagram: "#",
    facebook: "#",
    twitter: "#"
  }
};

/**
 * Información de contacto para mostrar en tarjetas
 */
export const contactInfo: ContactInfo[] = [
  {
    icon: MapPin,
    title: "Ubicación",
    value: businessInfo.contact.address
  },
  {
    icon: Phone,
    title: "Teléfono",
    value: businessInfo.contact.phone
  },
  {
    icon: Mail,
    title: "Email",
    value: businessInfo.contact.email
  },
  {
    icon: Clock,
    title: "Horario",
    value: businessInfo.contact.schedule
  }
];

