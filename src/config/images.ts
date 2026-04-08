/**
 * Configuración de imágenes - SOLO PARA HERO Y ABOUT
 * 
 * IMPORTANTE: Las imágenes están HARDCODEADAS solo aquí (Hero y About).
 * Gallery DEBE consumir desde /api/gallery endpoint (plan original).
 * 
 * NO agregar galleryImages aquí - debe venir desde BD via backend.
 * Imágenes optimizadas a formato WebP con fallback JPG:
 * - Hero: 1920x1080 (80% reducción vs original)
 * - About: 1200x800 (52% reducción vs original)
 */

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

