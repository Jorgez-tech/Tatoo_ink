import React, { useState, memo } from 'react'

const ERROR_IMG_SRC =
  'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iODgiIGhlaWdodD0iODgiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgc3Ryb2tlPSIjMDAwIiBzdHJva2UtbGluZWpvaW49InJvdW5kIiBvcGFjaXR5PSIuMyIgZmlsbD0ibm9uZSIgc3Ryb2tlLXdpZHRoPSIzLjciPjxyZWN0IHg9IjE2IiB5PSIxNiIgd2lkdGg9IjU2IiBoZWlnaHQ9IjU2IiByeD0iNiIvPjxwYXRoIGQ9Im0xNiA1OCAxNi0xOCAzMiAzMiIvPjxjaXJjbGUgY3g9IjUzIiBjeT0iMzUiIHI9IjciLz48L3N2Zz4KCg=='

interface ImageWithFallbackProps extends Omit<React.ImgHTMLAttributes<HTMLImageElement>, 'src'> {
  /** Ruta de la imagen principal (WebP preferido) */
  src: string;
  /** Ruta de fallback (JPG) para navegadores sin soporte WebP */
  fallback?: string;
  /** Texto alternativo (requerido para accesibilidad) */
  alt: string;
  /** Ancho de la imagen (para aspect ratio) */
  width?: number;
  /** Alto de la imagen (para aspect ratio) */
  height?: number;
  /**
   * Controla el CSS object-fit del <img> interno.
   * - 'cover'   (default): recorta para llenar el contenedor — ideal para grids/cards
   * - 'contain': escala para mostrar la imagen completa — ideal para lightbox/modal
   */
  objectFit?: 'cover' | 'contain' | 'fill' | 'none' | 'scale-down';
}

/**
 * Imagen con placeholder animado, soporte WebP/JPG y fallback base64 al fallar la carga.
 *
 * IMPORTANTE — diseño de clases:
 * El componente renderiza un <div> contenedor + <img> interno.
 * Las clases recibidas vía `className` se aplican al <div> contenedor.
 * El <img> interno recibe `object-cover w-full h-full` siempre para ocupar
 * todo el espacio del contenedor sin distorsionarse.
 *
 * Uso correcto:
 *   <ImageWithFallback
 *     src="..."
 *     alt="..."
 *     className="w-full h-full"   ← al contenedor
 *   />
 *
 * El contenedor padre debe definir las dimensiones (aspect-ratio, h-X, etc.)
 */
export const ImageWithFallback = memo(function ImageWithFallback(props: ImageWithFallbackProps) {
  const [didError, setDidError] = useState(false)
  const [isLoading, setIsLoading] = useState(true)

  const handleError = () => {
    setDidError(true)
    setIsLoading(false)
  }

  const handleLoad = () => {
    setIsLoading(false)
  }

  // Extraemos className, style, loading y las propias del img — el resto va al img
  const { src, fallback, alt, style, className, loading = 'lazy', width, height, objectFit = 'cover', ...rest } = props

  // Clases del <img> interno — objectFit configurable, por defecto 'cover'
  const objectFitClass = `object-${objectFit}`
  const imgClasses = `w-full h-full ${objectFitClass} transition-opacity duration-300 ${isLoading ? 'opacity-0' : 'opacity-100'}`

  // Estado error: placeholder SVG centrado en el contenedor
  if (didError) {
    return (
      <div
        className={`relative overflow-hidden bg-gray-100 flex items-center justify-center ${className ?? ''}`}
        style={style}
      >
        <img
          src={ERROR_IMG_SRC}
          alt="Error al cargar imagen"
          className="w-16 h-16 opacity-40"
          data-original-url={src}
        />
      </div>
    )
  }

  // Con fallback WebP → JPG usando <picture>
  if (fallback) {
    return (
      <div className={`relative overflow-hidden ${className ?? ''}`} style={style}>
        {isLoading && (
          <div className="absolute inset-0 bg-gray-200 animate-pulse" />
        )}
        <picture className="w-full h-full">
          <source srcSet={src} type="image/webp" />
          <source srcSet={fallback} type="image/jpeg" />
          <img
            src={fallback}
            alt={alt}
            className={imgClasses}
            loading={loading}
            width={width}
            height={height}
            {...rest}
            onError={handleError}
            onLoad={handleLoad}
          />
        </picture>
      </div>
    )
  }

  // Sin fallback — imagen estándar
  return (
    <div className={`relative overflow-hidden ${className ?? ''}`} style={style}>
      {isLoading && (
        <div className="absolute inset-0 bg-gray-200 animate-pulse" />
      )}
      <img
        src={src}
        alt={alt}
        className={imgClasses}
        loading={loading}
        width={width}
        height={height}
        {...rest}
        onError={handleError}
        onLoad={handleLoad}
      />
    </div>
  )
});
