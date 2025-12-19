import { Button } from "../ui/button";
import { ImageWithFallback } from "../ui/ImageWithFallback";
import { heroContent } from "@/config/content";
import { heroImage } from "@/config/images";

/**
 * Sección principal de la landing con fondo a pantalla completa y CTA dobles.
 *
 * Renderiza una imagen hero con overlay oscuro, título destacado y botones de
 * acción primario/secundario que reutilizan el contenido definido en configuración.
 *
 * @component
 * @example
 * return (
 *   <Hero />
 * );
 */
export function Hero() {
  return (
    <section id="home" className="relative min-h-screen h-screen flex items-center justify-center overflow-hidden">
      <div className="absolute inset-0 z-0">
        <ImageWithFallback
          src={heroImage.src}
          fallback={heroImage.fallback}
          alt={heroImage.alt}
          width={heroImage.width}
          height={heroImage.height}
          className="w-full h-full object-cover"
          loading="eager"
          decoding="async"
          fetchPriority="high"
        />
        <div className="absolute inset-0 bg-black/70"></div>
      </div>

      <div className="relative z-10 text-center text-white px-4 sm:px-6 lg:px-8 max-w-4xl mx-auto">
        <h1 className="text-4xl sm:text-5xl md:text-6xl lg:text-7xl font-bold mb-4 sm:mb-6 animate-fade-in-up">
          {heroContent.title}
        </h1>
        <p className="text-base sm:text-lg md:text-xl mb-6 sm:mb-8 max-w-2xl mx-auto text-gray-200 animate-fade-in-up animation-delay-200">
          {heroContent.description}
        </p>
        <div className="flex flex-col sm:flex-row gap-3 sm:gap-4 justify-center items-center animate-fade-in-up animation-delay-300">
          <Button size="lg" className="bg-white text-black hover:bg-gray-200 w-full sm:w-auto">
            {heroContent.primaryButton}
          </Button>
          <Button size="lg" variant="outline" className="text-white border-white hover:bg-white/10 w-full sm:w-auto">
            {heroContent.secondaryButton}
          </Button>
        </div>
      </div>

      <div className="absolute bottom-8 left-1/2 transform -translate-x-1/2 animate-bounce">
        <svg
          className="w-6 h-6 text-white"
          fill="none"
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth="2"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path d="M19 14l-7 7m0 0l-7-7m7 7V3"></path>
        </svg>
      </div>
    </section>
  );
}
