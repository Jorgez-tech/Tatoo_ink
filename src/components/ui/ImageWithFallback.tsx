import React, { useState, memo } from 'react'

const ERROR_IMG_SRC =
  'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iODgiIGhlaWdodD0iODgiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgc3Ryb2tlPSIjMDAwIiBzdHJva2UtbGluZWpvaW49InJvdW5kIiBvcGFjaXR5PSIuMyIgZmlsbD0ibm9uZSIgc3Ryb2tlLXdpZHRoPSIzLjciPjxyZWN0IHg9IjE2IiB5PSIxNiIgd2lkdGg9IjU2IiBoZWlnaHQ9IjU2IiByeD0iNiIvPjxwYXRoIGQ9Im0xNiA1OCAxNi0xOCAzMiAzMiIvPjxjaXJjbGUgY3g9IjUzIiBjeT0iMzUiIHI9IjciLz48L3N2Zz4KCg=='

interface ImageWithFallbackProps extends Omit<React.ImgHTMLAttributes<HTMLImageElement>, 'src'> {
  src: string;
  fallback?: string;
  alt: string;
  width?: number;
  height?: number;
  /**
   * Controla el CSS object-fit del <img> interno.
   * - 'cover'   (default): recorta para llenar el contenedor — grids/cards
   * - 'contain': muestra imagen completa — lightbox/modal
   */
  objectFit?: 'cover' | 'contain' | 'fill' | 'none' | 'scale-down';
}

/**
 * Imagen con soporte WebP/JPG y fallback de error.
 *
 * PATRÓN DE USO — el padre DEBE tener `position: relative` y dimensiones definidas:
 *
 *   <div className="relative aspect-square overflow-hidden">
 *     <ImageWithFallback src="..." alt="..." />
 *   </div>
 *
 * El componente usa `absolute inset-0` internamente → siempre llena el padre.
 * NO pasar clases de dimensión (w-*, h-*) al componente — son ignoradas.
 */
export const ImageWithFallback = memo(function ImageWithFallback(props: ImageWithFallbackProps) {
  const [didError, setDidError] = useState(false)
  const [isLoading, setIsLoading] = useState(true)

  const { src, fallback, alt, style, className, loading = 'lazy', width, height, objectFit = 'cover', ...rest } = props

  const handleError = () => { setDidError(true); setIsLoading(false) }
  const handleLoad  = () => { setIsLoading(false) }

  // Clases del <img>: absolute fill + object-fit configurable
  const imgBase = `absolute inset-0 w-full h-full transition-opacity duration-300 object-${objectFit}`
  const imgClasses = `${imgBase} ${isLoading ? 'opacity-0' : 'opacity-100'}`

  // Error: ícono centrado, también absoluto para llenar el contenedor
  if (didError) {
    return (
      <div
        className={`absolute inset-0 bg-gray-100 flex items-center justify-center ${className ?? ''}`}
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

  // Con fallback WebP → JPG
  if (fallback) {
    return (
      <>
        {isLoading && <div className="absolute inset-0 bg-gray-200 animate-pulse" />}
        {/* picture como absolute fill — display:block resuelve el problema de inline */}
        <picture style={{ position: 'absolute', inset: 0, display: 'block', width: '100%', height: '100%' }}>
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
      </>
    )
  }

  // Sin fallback
  return (
    <>
      {isLoading && <div className="absolute inset-0 bg-gray-200 animate-pulse" />}
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
    </>
  )
})
