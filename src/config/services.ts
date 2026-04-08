/**
 * Configuración de servicios
 * Centraliza la información de los servicios ofrecidos
 */

import { Palette, Sparkles, Users, Shield } from "lucide-react";
import type { Service } from "@/types";

/**
 * Lista de servicios ofrecidos
 */
export const services: Service[] = [
  {
    icon: Palette,
    title: "Tatuajes Personalizados",
    description: "Diseños únicos creados especialmente para ti. Trabajamos contigo para dar vida a tu visión."
  },
  {
    icon: Sparkles,
    title: "Cover-Up",
    description: "Transformamos tatuajes antiguos en nuevas obras de arte que amarás."
  },
  {
    icon: Users,
    title: "Artistas Especializados",
    description: "Equipo de profesionales con estilos diversos: realismo, tradicional, minimalista y más."
  },
  {
    icon: Shield,
    title: "Seguridad e Higiene",
    description: "Máximos estándares de esterilización y materiales de primera calidad."
  }
];

