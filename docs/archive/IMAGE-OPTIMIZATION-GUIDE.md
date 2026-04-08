# Guía de Optimización de Imágenes

**Fecha:** 2025-12-19  
**Tarea:** Migrar imágenes de Unsplash a assets locales optimizados

---

## IMÁGENES A DESCARGAR

### 1. Hero Image
**URL actual:** https://images.unsplash.com/photo-1761276297550-27567ed50a1e?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBhcnRpc3QlMjB3b3JraW5nfGVufDF8fHx8MTc2MjI1MDcwMHww&ixlib=rb-4.1.0&q=80&w=1080

**Descripción:** Tattoo Artist Working  
**Destino:** `public/images/hero/tattoo-artist.jpg`  
**Tamaño recomendado:** 1920x1080 (full HD)

### 2. About Image
**URL actual:** https://images.unsplash.com/photo-1760877611905-0f885a3ce551?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBzdHVkaW8lMjBpbnRlcmlvcnxlbnwxfHx8fDE3NjIyNDQ1OTl8MA&ixlib=rb-4.1.0&q=80&w=1080

**Descripción:** Tattoo Studio Interior  
**Destino:** `public/images/about/studio-interior.jpg`  
**Tamaño recomendado:** 1200x800

### 3. Gallery Images

#### Gallery 1
**URL:** https://images.unsplash.com/photo-1665085326630-b01fea9a613d?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBhcnQlMjBkZXNpZ258ZW58MXx8fHwxNzYyMjU2NTg5fDA&ixlib=rb-4.1.0&q=80&w=1080  
**Destino:** `public/images/gallery/tattoo-art-1.jpg`

#### Gallery 2
**URL:** https://images.unsplash.com/photo-1721160223584-b3a19f2e0e6a?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBibGFjayUyMGlua3xlbnwxfHx8fDE3NjIzMTcyMjh8MA&ixlib=rb-4.1.0&q=80&w=1080  
**Destino:** `public/images/gallery/tattoo-art-2.jpg`

#### Gallery 3
**URL:** https://images.unsplash.com/photo-1753260724749-25110c0ce91c?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBtYWNoaW5lJTIwY2xvc2UlMjB1cHxlbnwxfHx8fDE3NjIzMTcyMjh8MA&ixlib=rb-4.1.0&q=80&w=1080  
**Destino:** `public/images/gallery/tattoo-art-3.jpg`

#### Gallery 4
**URL:** https://images.unsplash.com/photo-1604374376934-2df6fad6519b?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBnZW9tZXRyaWMlMjB0YXR0b298ZW58MXx8fHwxNzYyMjUwNzAyfDA&ixlib=rb-4.1.0&q=80&w=1080  
**Destino:** `public/images/gallery/tattoo-art-4.jpg`

#### Gallery 5
**URL:** https://images.unsplash.com/photo-1760877611905-0f885a3ce551?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBzdHVkaW8lMjBpbnRlcmlvcnxlbnwxfHx8fDE3NjIyNDQ1OTl8MA&ixlib=rb-4.1.0&q=80&w=1080  
**Destino:** `public/images/gallery/tattoo-art-5.jpg`

#### Gallery 6
**URL:** https://images.unsplash.com/photo-1761276297550-27567ed50a1e?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBhcnRpc3QlMjB3b3JraW5nfGVufDF8fHx8MTc2MjI1MDcwMHww&ixlib=rb-4.1.0&q=80&w=1080  
**Destino:** `public/images/gallery/tattoo-art-6.jpg`

---

## PROCESO DE OPTIMIZACIÓN

### Opción 1: Manual con Squoosh (Recomendado para este caso)

1. **Descargar imágenes:**
   ```powershell
   # Abrir cada URL en navegador y descargar
   # O usar script PowerShell (ver abajo)
   ```

2. **Optimizar con Squoosh:**
   - Ir a https://squoosh.app/
   - Arrastrar imagen
   - Configurar:
     - Formato: WebP
     - Calidad: 80-85
     - Resize: Según tamaño recomendado
   - Descargar optimizada
   - Repetir para formato JPG (fallback)

3. **Renombrar archivos:**
   ```
   tattoo-artist.webp
   tattoo-artist.jpg
   studio-interior.webp
   studio-interior.jpg
   ...etc
   ```

### Opción 2: Automático con Sharp (Node.js)

Crear script `scripts/optimize-images.js`:

```javascript
const sharp = require('sharp');
const fs = require('fs');
const path = require('path');

const images = [
  {
    url: 'URL_IMAGEN',
    output: 'public/images/hero/tattoo-artist',
    width: 1920,
    height: 1080
  },
  // ... más imágenes
];

async function optimizeImage(input, output, width, height) {
  // JPG fallback
  await sharp(input)
    .resize(width, height, { fit: 'cover' })
    .jpeg({ quality: 85, mozjpeg: true })
    .toFile(`${output}.jpg`);

  // WebP optimizado
  await sharp(input)
    .resize(width, height, { fit: 'cover' })
    .webp({ quality: 80 })
    .toFile(`${output}.webp`);

  // AVIF (opcional, mejor compresión pero menos soporte)
  await sharp(input)
    .resize(width, height, { fit: 'cover' })
    .avif({ quality: 75 })
    .toFile(`${output}.avif`);
}

// Ejecutar para todas las imágenes
```

---

## SCRIPT POWERSHELL PARA DESCARGAR

```powershell
# Download-Images.ps1
$images = @(
    @{
        url = "https://images.unsplash.com/photo-1761276297550-27567ed50a1e?w=1920&q=85"
        dest = "public/images/hero/tattoo-artist-original.jpg"
    },
    @{
        url = "https://images.unsplash.com/photo-1760877611905-0f885a3ce551?w=1200&q=85"
        dest = "public/images/about/studio-interior-original.jpg"
    }
    # ... más imágenes
)

foreach ($img in $images) {
    Write-Host "Descargando: $($img.dest)"
    Invoke-WebRequest -Uri $img.url -OutFile $img.dest
}

Write-Host "Todas las imágenes descargadas"
```

---

## ACTUALIZAR CONFIGURACIÓN

Después de optimizar, actualizar `src/config/images.ts`:

```typescript
/**
 * Imagen del Hero (sección principal)
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
 */
export const galleryImages: GalleryImage[] = [
  {
    src: "/images/gallery/tattoo-art-1.webp",
    fallback: "/images/gallery/tattoo-art-1.jpg",
    alt: "Tattoo Art Design 1",
    width: 800,
    height: 600
  },
  // ... más imágenes
];
```

---

## ACTUALIZAR ImageWithFallback (Si es necesario)

Verificar si `ImageWithFallback` soporta fallback de WebP → JPG:

```typescript
export function ImageWithFallback(props: ImageProps) {
  const { src, fallback, ...restProps } = props;
  
  return (
    <picture>
      <source srcSet={src} type="image/webp" />
      {fallback && <source srcSet={fallback} type="image/jpeg" />}
      <img {...restProps} src={fallback || src} />
    </picture>
  );
}
```

---

## RESPONSIVE IMAGES (Opcional - Fase avanzada)

Para implementar `srcset`:

```typescript
export const heroImage = {
  srcset: {
    webp: {
      '1920w': '/images/hero/tattoo-artist-1920.webp',
      '1280w': '/images/hero/tattoo-artist-1280.webp',
      '640w': '/images/hero/tattoo-artist-640.webp',
    },
    jpg: {
      '1920w': '/images/hero/tattoo-artist-1920.jpg',
      '1280w': '/images/hero/tattoo-artist-1280.jpg',
      '640w': '/images/hero/tattoo-artist-640.jpg',
    }
  },
  sizes: '(max-width: 640px) 640px, (max-width: 1280px) 1280px, 1920px',
  alt: "Tattoo Artist Working"
};
```

---

## CHECKLIST DE COMPLETITUD

- [ ] Estructura de carpetas creada (`public/images/*`)
- [ ] 8 imágenes descargadas
- [ ] Imágenes optimizadas a WebP (calidad 80)
- [ ] Fallbacks JPG creados (calidad 85)
- [ ] Tamaños correctos (Hero: 1920x1080, About: 1200x800, Gallery: 800x600)
- [ ] `config/images.ts` actualizado con rutas locales
- [ ] `ImageWithFallback` actualizado si necesario
- [ ] Build funciona sin errores
- [ ] Hero renderiza correctamente
- [ ] About renderiza correctamente
- [ ] Gallery carga todas las imágenes
- [ ] Lightbox funciona con imágenes locales
- [ ] Performance mejorado (LCP reducido)

---

## MÉTRICAS ESPERADAS

**Antes (Unsplash):**
- Dependencia externa
- Sin control de tamaño
- LCP: ~2.5-3.0s

**Después (Local WebP):**
- Control total
- Imágenes optimizadas
- LCP: ~1.5-2.0s (objetivo)
- Reducción de peso: 40-60%

---

## PRÓXIMO PASO

Después de completar esta tarea:
1. Ejecutar Lighthouse audit (Tarea 2)
2. Validar mejora en LCP
3. Documentar resultados en PERFORMANCE.md
