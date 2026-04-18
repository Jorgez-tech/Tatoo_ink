/**
 * Configuración de información del negocio
 * Centraliza todos los datos relacionados con el negocio
 */

import { Award, Clock, Heart, MapPin, Phone, Mail } from "lucide-react";
import type { BusinessInfo, BusinessSettings, Stat, ContactInfo } from "@/types";

/**
 * Información principal del negocio
 */
export const defaultBusinessInfo: BusinessInfo = {
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

export const defaultBusinessSettings: BusinessSettings = {
  businessName: defaultBusinessInfo.name,
  businessTagline: defaultBusinessInfo.tagline,
  businessDescription: defaultBusinessInfo.description,
  phoneNumber: defaultBusinessInfo.contact.phone,
  emailAddress: defaultBusinessInfo.contact.email,
  address: defaultBusinessInfo.contact.address,
  instagramUrl: defaultBusinessInfo.social.instagram,
  facebookUrl: defaultBusinessInfo.social.facebook,
  twitterUrl: defaultBusinessInfo.social.twitter,
  schedule: defaultBusinessInfo.contact.schedule,
};

/**
 * Alias legacy para compatibilidad con componentes que aún no migran a API.
 */
export const businessInfo = defaultBusinessInfo;

export const toBusinessInfo = (settings: BusinessSettings): BusinessInfo => ({
  name: settings.businessName,
  tagline: settings.businessTagline,
  description: settings.businessDescription,
  foundedYear: defaultBusinessInfo.foundedYear,
  contact: {
    address: settings.address,
    phone: settings.phoneNumber,
    email: settings.emailAddress,
    schedule: settings.schedule,
  },
  stats: defaultBusinessInfo.stats,
  social: {
    instagram: settings.instagramUrl,
    facebook: settings.facebookUrl,
    twitter: settings.twitterUrl,
  },
});

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

export const toContactInfo = (settings: BusinessSettings): ContactInfo[] => [
  {
    icon: MapPin,
    title: "Ubicación",
    value: settings.address,
  },
  {
    icon: Phone,
    title: "Teléfono",
    value: settings.phoneNumber,
  },
  {
    icon: Mail,
    title: "Email",
    value: settings.emailAddress,
  },
  {
    icon: Clock,
    title: "Horario",
    value: settings.schedule,
  },
];

