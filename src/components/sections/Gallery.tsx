import { ImageWithFallback } from "../ui/ImageWithFallback";
import { useState, useEffect, useCallback } from "react";
import { galleryContent } from "@/config/content";
import { ChevronLeft, ChevronRight, X, Loader2 } from "lucide-react";
import { galleryService } from "@/services/gallery";
import type { GalleryImage } from "@/types";

/**
 * Galería de imágenes con lightbox interactivo y navegación por teclado.
 *
 * PLAN ORIGINAL: Consume imágenes desde backend API (/api/gallery).
 * Las imágenes están en BD SQLite alimentadas por DbInitializer.cs
 *
 * NO USAR hardcodes de config/images.ts - eso es solo para Hero y About.
 * Gallery DEBE obtener datos desde /api/gallery endpoint.
 *
 * Muestra imágenes en grid responsive. Al hacer clic, abre lightbox modal
 * con navegación mediante botones, flechas del teclado (← →) o cierre con ESC.
 *
 * Gestiona tres estados:
 * - `loading`: Cargando imágenes desde la API
 * - `error`: Falló la carga, muestra mensaje de error
 * - `images`: Imágenes cargadas exitosamente desde /api/gallery
 *
 * El lightbox implementa:
 * - Navegación circular (última → primera imagen y viceversa)
 * - Bloqueo de scroll del body mientras está abierto
 * - Cierre al hacer clic en el overlay oscuro
 * - Lazy loading de imágenes con placeholder animado
 *
 * @component
 * @example
 * return (
 *   <Gallery />
 * );
 *
 * @see {@link config/content.ts} - Configuración de títulos y subtítulos
 * @see {@link services/gallery.ts} - Servicio de API para obtener imágenes
 * @see {@link backend/Controllers/GalleryController.cs} - Endpoint GET /api/gallery
 * @see {@link backend/Data/DbInitializer.cs} - Seed de imágenes de ejemplo
 * @see {@link types/index.ts} - Tipo GalleryImage
 */
export function Gallery() {
  const [selectedImage, setSelectedImage] = useState<number | null>(null);
  const [images, setImages] = useState<GalleryImage[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Cargar imágenes desde backend API (plan original)
  useEffect(() => {
    const fetchImages = async () => {
      try {
        const data = await galleryService.getAll();
        setImages(data);
        setError(null);
      } catch (err) {
        console.error("Failed to fetch gallery images:", err);
        setError("No se pudieron cargar las imágenes. Por favor, intenta más tarde.");
      } finally {
        setLoading(false);
      }
    };

    fetchImages();
  }, []);

  const navigatePrevious = useCallback(() => {
    setSelectedImage((current) => {
      if (current === null) return null;
      return current === 0 ? images.length - 1 : current - 1;
    });
  }, [images.length]);

  const navigateNext = useCallback(() => {
    setSelectedImage((current) => {
      if (current === null) return null;
      return current === images.length - 1 ? 0 : current + 1;
    });
  }, [images.length]);

  // ... (keep existing useEffects for keyboard navigation)

  // Cerrar lightbox con tecla ESC
  useEffect(() => {
    const handleEscape = (e: KeyboardEvent) => {
      if (e.key === "Escape" && selectedImage !== null) {
        setSelectedImage(null);
      }
    };

    if (selectedImage !== null) {
      document.addEventListener("keydown", handleEscape);
      document.body.style.overflow = "hidden";
    }

    return () => {
      document.removeEventListener("keydown", handleEscape);
      document.body.style.overflow = "unset";
    };
  }, [selectedImage]);

  // Navegación con flechas del teclado
  useEffect(() => {
    const handleArrowKeys = (e: KeyboardEvent) => {
      if (selectedImage === null) return;

      if (e.key === "ArrowLeft") {
        navigatePrevious();
      } else if (e.key === "ArrowRight") {
        navigateNext();
      }
    };

    if (selectedImage !== null) {
      document.addEventListener("keydown", handleArrowKeys);
    }

    return () => {
      document.removeEventListener("keydown", handleArrowKeys);
    };
  }, [selectedImage, navigatePrevious, navigateNext]);

  const handleBackdropClick = (e: React.MouseEvent<HTMLDivElement>) => {
    if (e.target === e.currentTarget) {
      setSelectedImage(null);
    }
  };

  // Estado: Cargando
  if (loading) {
    return (
      <section className="py-12 sm:py-16 md:py-20 px-4 bg-white min-h-[400px] flex items-center justify-center">
        <Loader2 className="w-8 h-8 animate-spin text-gray-400" />
      </section>
    );
  }

  // Estado: Error
  if (error) {
    return (
      <section className="py-12 sm:py-16 md:py-20 px-4 bg-white text-center">
        <p className="text-red-500">{error}</p>
      </section>
    );
  }

  return (
    <section className="py-12 sm:py-16 md:py-20 px-4 bg-white">
      <div className="max-w-7xl mx-auto">
        <div className="text-center mb-12 sm:mb-16">
          <h2 className="text-3xl sm:text-4xl md:text-5xl font-bold mb-3 sm:mb-4 animate-fade-in-up">{galleryContent.title}</h2>
          <p className="text-sm sm:text-base md:text-lg text-gray-600 max-w-2xl mx-auto animate-fade-in-up animation-delay-100">
            {galleryContent.description}
          </p>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-3 sm:gap-4">
          {images.map((image, index) => (
            <button
              key={index}
              type="button"
              className="relative aspect-square overflow-hidden rounded-lg cursor-pointer group w-full p-0 border-0 focus-visible:ring-2 focus-visible:ring-primary focus-visible:ring-offset-2"
              onClick={() => setSelectedImage(index)}
              aria-label={`Ver imagen ${index + 1}: ${image.alt}`}
            >
              <ImageWithFallback
                src={image.src}
                alt={image.alt}
                className="w-full h-full object-cover transition-transform duration-300 group-hover:scale-110"
                decoding="async"
                sizes="(min-width:1024px) 33vw, (min-width:640px) 50vw, 100vw"
              />
              <div className="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity flex items-center justify-center">
                <span className="text-white">Ver más</span>
              </div>
            </button>
          ))}
        </div>

        {selectedImage !== null && (
          <div
            className="fixed inset-0 bg-black/95 z-50 flex items-center justify-center p-4"
            onClick={handleBackdropClick}
          >
            {/* Botón cerrar */}
            <button
              className="absolute top-4 right-4 z-10 text-white hover:text-gray-300 transition-colors p-2 rounded-full hover:bg-white/10"
              onClick={() => setSelectedImage(null)}
              aria-label="Cerrar galería"
            >
              <X className="w-8 h-8" />
            </button>

            {/* Botón anterior */}
            {images.length > 1 && (
              <button
                className="absolute left-4 top-1/2 -translate-y-1/2 z-10 text-white hover:text-gray-300 transition-colors p-3 rounded-full hover:bg-white/10"
                onClick={(e) => {
                  e.stopPropagation();
                  navigatePrevious();
                }}
                aria-label="Imagen anterior"
              >
                <ChevronLeft className="w-8 h-8" />
              </button>
            )}

            {/* Botón siguiente */}
            {images.length > 1 && (
              <button
                className="absolute right-4 top-1/2 -translate-y-1/2 z-10 text-white hover:text-gray-300 transition-colors p-3 rounded-full hover:bg-white/10"
                onClick={(e) => {
                  e.stopPropagation();
                  navigateNext();
                }}
                aria-label="Imagen siguiente"
              >
                <ChevronRight className="w-8 h-8" />
              </button>
            )}

            {/* Contador de imágenes */}
            {images.length > 1 && (
              <div className="absolute top-4 left-1/2 -translate-x-1/2 z-10 text-white text-sm">
                {selectedImage + 1} / {images.length}
              </div>
            )}

            {/* Imagen */}
            <div className="relative max-w-full max-h-full">
              <ImageWithFallback
                src={images[selectedImage].src}
                alt={images[selectedImage].alt}
                className="max-w-full max-h-[90vh] object-contain"
                decoding="async"
                sizes="100vw"
              />
            </div>
          </div>
        )}
      </div>
    </section>
  );
}
