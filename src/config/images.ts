/**
 * Configuración de imágenes
 * Centraliza todas las URLs/rutas de imágenes del proyecto
 * 
 * NOTA: Imágenes optimizadas a formato WebP con fallback JPG
 * - Hero: 1920x1080 (reducción 80% vs original)
 * - About: 1200x800 (reducción 52% vs original)
 * - Gallery: 800x600 (reducción promedio 65% vs original)
 */

import type { GalleryImage } from "@/types";

/**
 * Imagen del Hero (sección principal)
 * Original: 0.98 MB → WebP: 0.19 MB (80.6% reducción)
 */
export const heroImage = {
  src: "/images/hero/tattoo-artist.webp",
  fallback: "/images/hero/tattoo-artist.jpg",
  alt: "Tattoo Artist Working",
  width: 1920,
  height: 1080
};

/**
 * Imagen de la sección About
 * Original: 0.24 MB → WebP: 0.12 MB (52.4% reducción)
 */
export const aboutImage = {
  src: "/images/about/studio-interior.webp",
  fallback: "/images/about/studio-interior.jpg",
  alt: "Tattoo Studio",
  width: 1200,
  height: 800
};

/**
 * Imágenes de la galería
 * Promedio reducción: ~65% en formato WebP
 */
export const galleryImages: GalleryImage[] = [
  {
    src: "/images/gallery/tattoo-art-1.webp",
    fallback: "/images/gallery/tattoo-art-1.jpg",
    alt: "Tattoo Art Design 1",
    width: 800,
    height: 600
  },
  {
    src: "/images/gallery/tattoo-art-2.webp",
    fallback: "/images/gallery/tattoo-art-2.jpg",
    alt: "Tattoo Art Design 2",
    width: 800,
    height: 600
  },
  {
    src: "/images/gallery/tattoo-art-3.webp",
    fallback: "/images/gallery/tattoo-art-3.jpg",
    alt: "Tattoo Art Design 3",
    width: 800,
    height: 600
  },
  {
    src: "/images/gallery/tattoo-art-4.webp",
    fallback: "/images/gallery/tattoo-art-4.jpg",
    alt: "Tattoo Art Design 4",
    width: 800,
    height: 600
  },
  {
    src: "/images/gallery/tattoo-art-5.webp",
    fallback: "/images/gallery/tattoo-art-5.jpg",
    alt: "Tattoo Art Design 5",
    width: 800,
    height: 600
  },
  {
    src: "/images/gallery/tattoo-art-6.webp",
    fallback: "/images/gallery/tattoo-art-6.jpg",
    alt: "Tattoo Art Design 6",
    width: 800,
    height: 600
  }
];

