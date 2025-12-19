import React, { useState } from 'react'

const ERROR_IMG_SRC =
  'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iODgiIGhlaWdodD0iODgiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgc3Ryb2tlPSIjMDAwIiBzdHJva2UtbGluZWpvaW49InJvdW5kIiBvcGFjaXR5PSIuMyIgZmlsbD0ibm9uZSIgc3Ryb2tlLXdpZHRoPSIzLjciPjxyZWN0IHg9IjE2IiB5PSIxNiIgd2lkdGg9IjU2IiBoZWlnaHQ9IjU2IiByeD0iNiIvPjxwYXRoIGQ9Im0xNiA1OCAxNi0xOCAzMiAzMiIvPjxjaXJjbGUgY3g9IjUzIiBjeT0iMzUiIHI9IjciLz48L3N2Zz4KCg=='

interface ImageWithFallbackProps extends Omit<React.ImgHTMLAttributes<HTMLImageElement>, 'src'> {
  /** Ruta de la imagen principal (WebP preferido) */
  src: string;
  /** Ruta de fallback (JPG) para navegadores sin soporte WebP */
  fallback?: string;
  /** Texto alternativo (requerido para accesibilidad) */
  alt: string;
  /** Ancho de la imagen (para aspecto ratio) */
  width?: number;
  /** Alto de la imagen (para aspecto ratio) */
  height?: number;
}

/**
 * Imagen con placeholder animado, soporte WebP/JPG y fallback base64 al fallar la carga.
 *
 * Gestiona estados de carga y error para ofrecer feedback visual consistente.
 * Utiliza elemento `<picture>` para servir WebP con fallback JPG automático.
 * Mantiene compatibilidad con props estándar de `<img>`.
 *
 * @component
 * @example
 * ```tsx
 * // Con WebP y fallback JPG
 * <ImageWithFallback 
 *   src="/images/hero.webp" 
 *   fallback="/images/hero.jpg"
 *   alt="Hero" 
 *   width={1920}
 *   height={1080}
 *   className="h-64" 
 * />
 * 
 * // Solo imagen estándar
 * <ImageWithFallback src="/hero.jpg" alt="Hero" />
 * ```
 * 
 * @see {@link https://developer.mozilla.org/en-US/docs/Web/HTML/Element/picture}
 */
export function ImageWithFallback(props: ImageWithFallbackProps) {
  const [didError, setDidError] = useState(false)
  const [isLoading, setIsLoading] = useState(true)

  const handleError = () => {
    setDidError(true)
    setIsLoading(false)
  }

  const handleLoad = () => {
    setIsLoading(false)
  }

  const { src, fallback, alt, style, className, loading = 'lazy', width, height, ...rest } = props

  // Si falla la carga, mostrar placeholder de error
  if (didError) {
    return (
      <div
        className={`inline-block bg-gray-100 text-center align-middle ${className ?? ''}`}
        style={style}
      >
        <div className="flex items-center justify-center w-full h-full">
          <img src={ERROR_IMG_SRC} alt="Error loading image" {...rest} data-original-url={src} />
        </div>
      </div>
    )
  }

  // Si hay fallback, usar <picture> para WebP con fallback JPG
  if (fallback) {
    return (
      <div className={`relative ${className ?? ''}`} style={style}>
        {isLoading && (
          <div className="absolute inset-0 bg-gray-200 animate-pulse" />
        )}
        <picture>
          <source srcSet={src} type="image/webp" />
          <source srcSet={fallback} type="image/jpeg" />
          <img
            src={fallback}
            alt={alt}
            className={`w-full h-full object-cover ${isLoading ? 'opacity-0' : 'opacity-100'} transition-opacity duration-300`}
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

  // Sin fallback, imagen estándar
  return (
    <div className={`relative ${className ?? ''}`} style={style}>
      {isLoading && (
        <div className="absolute inset-0 bg-gray-200 animate-pulse" />
      )}
      <img
        src={src}
        alt={alt}
        className={`w-full h-full object-cover ${isLoading ? 'opacity-0' : 'opacity-100'} transition-opacity duration-300`}
        loading={loading}
        width={width}
        height={height}
        {...rest}
        onError={handleError}
        onLoad={handleLoad}
      />
    </div>
  )
}
