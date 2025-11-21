# ⚡ Performance - Ink Studio

Documentación de optimizaciones de rendimiento implementadas en el proyecto.

## 📊 Métricas Objetivo

### Core Web Vitals

- **LCP (Largest Contentful Paint):** < 2.5s ⚡
- **FID (First Input Delay):** < 100ms ⚡
- **CLS (Cumulative Layout Shift):** < 0.1 ⚡

### Lighthouse Scores

- **Performance:** > 90
- **Accessibility:** > 95
- **Best Practices:** > 90
- **SEO:** > 90

### Bundle Size

- **JavaScript:** < 250KB (gzipped)
- **CSS:** < 30KB (gzipped)
- **Total:** < 500KB (gzipped)

---

## ✅ Optimizaciones Implementadas

### 1. Optimización de Imágenes

#### Hero Image

```tsx
<ImageWithFallback
  src={heroImage.src}
  alt={heroImage.alt}
  loading="eager" // Carga inmediata (LCP)
  fetchPriority="high" // Prioridad alta
  decoding="async" // Decodificación asíncrona
/>
```

**Impacto:** Mejora LCP al priorizar la imagen hero.

#### Lazy Loading

```tsx
<ImageWithFallback
  src={image.src}
  alt={image.alt}
  loading="lazy" // Carga diferida
  decoding="async"
  sizes="(min-width:1024px) 33vw, (min-width:640px) 50vw, 100vw"
/>
```

**Impacto:** Reduce carga inicial, mejora tiempo de carga.

#### Placeholders Animados

```tsx
{
  isLoading && <div className="absolute inset-0 bg-gray-200 animate-pulse" />;
}
```

**Impacto:** Mejora CLS al reservar espacio, mejor UX.

### 2. DNS Prefetch y Preconnect

```html
<!-- index.html -->
<link rel="dns-prefetch" href="https://images.unsplash.com" />
<link rel="preconnect" href="https://images.unsplash.com" crossorigin />
```

**Impacto:** Reduce latencia de red para imágenes externas.

### 3. Smooth Scroll Optimizado

```css
html {
  scroll-behavior: smooth;
}

@media (prefers-reduced-motion: reduce) {
  html {
    scroll-behavior: auto !important;
  }
}
```

**Impacto:** Respeta preferencias de usuario, mejora accesibilidad.

### 4. Animaciones Optimizadas

```css
@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.animate-fade-in-up {
  animation: fadeInUp 0.6s ease-out forwards;
}
```

**Optimizaciones:**

- Uso de `transform` y `opacity` (GPU-accelerated)
- `will-change` solo cuando sea necesario
- Respeto a `prefers-reduced-motion`

### 5. Code Splitting

**Vite automático:**

- Chunks separados para vendor y app
- Lazy loading de rutas (si se implementan)

**Bundle actual:**

```
dist/assets/index-BHk0O8b-.js   231.25 kB │ gzip: 74.59 kB
dist/assets/index-COVSvm1d.css   25.08 kB │ gzip:  5.73 kB
dist/assets/api-BBOCQ1F-.js       0.35 kB │ gzip:  0.29 kB
```

**Total gzipped:** ~80KB ✅ (Excelente)

### 6. CSS Optimization

**Tailwind CSS:**

- Purge automático de clases no utilizadas
- Minificación en producción
- Variables CSS para temas

**Resultado:**

- CSS gzipped: 5.73 kB ✅

### 7. React Optimization

**Hooks optimizados:**

```tsx
// useCallback para funciones en lightbox
const navigatePrevious = useCallback(() => {
  setSelectedImage((current) => {
    if (current === null) return null;
    return current === 0 ? galleryImages.length - 1 : current - 1;
  });
}, []);
```

**Prevención de re-renders innecesarios:**

- Uso de `useCallback` en event handlers
- Configuración estática importada (no en state)

### 8. Scroll Spy Optimizado

```tsx
// use-active-section.ts
useEffect(() => {
  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          setActiveSection(entry.target.id);
        }
      });
    },
    { threshold: 0.5 }
  );
  // ...
}, [sectionIds]);
```

**Impacto:** Uso de IntersectionObserver (más eficiente que scroll events).

---

## 🔍 Análisis de Bundle

### Dependencias Principales

**Producción:**

```json
{
  "react": "^18.0.0", // ~6KB
  "react-dom": "^18.0.0", // ~130KB
  "lucide-react": "^0.552.0", // ~50KB (tree-shakeable)
  "react-hook-form": "^7.55.0", // ~25KB
  "@radix-ui/*": "varios", // ~20KB (solo lo usado)
  "tailwind-merge": "^3.3.1", // ~5KB
  "clsx": "^2.1.1" // ~1KB
}
```

**Total estimado:** ~237KB (sin gzip) → ~75KB (gzipped) ✅

### Oportunidades de Optimización

**Futuras mejoras:**

1. **Lazy load de Gallery:** Cargar componente solo cuando sea visible
2. **Optimizar Lucide Icons:** Importar solo iconos usados
3. **Imágenes locales:** Mover de Unsplash a local + WebP/AVIF
4. **Service Worker:** Cache de assets estáticos

---

## 📈 Métricas Actuales

### Build Stats

```bash
npm run build

✓ 1704 modules transformed.
dist/index.html                    1.56 kB │ gzip:  0.71 kB
dist/assets/index-COVSvm1d.css    25.08 kB │ gzip:  5.73 kB
dist/assets/api-BBOCQ1F-.js        0.35 kB │ gzip:  0.29 kB
dist/assets/index-BHk0O8b-.js    231.25 kB │ gzip: 74.59 kB
✓ built in 6.96s
```

**Análisis:**

- ✅ Bundle JS gzipped: 74.59 KB (Excelente)
- ✅ Bundle CSS gzipped: 5.73 KB (Excelente)
- ✅ Total: ~80 KB (Muy bueno)

### Lighthouse (Estimado)

**Performance:** 85-95

- LCP: ~2.0s (Hero image de Unsplash)
- FID: < 100ms
- CLS: < 0.1

**Accessibility:** 95-100

- Contraste de colores: ✅
- Navegación por teclado: ✅
- ARIA labels: ✅
- Alt text: ✅

**Best Practices:** 90-95

- HTTPS: ⚠️ (depende del hosting)
- Console errors: ✅
- Deprecated APIs: ✅

**SEO:** 90-100

- Meta tags: ✅
- Semantic HTML: ✅
- Mobile-friendly: ✅
- Structured data: ⚠️ (pendiente)

---

## 🚀 Optimizaciones Pendientes

### Alta Prioridad

1. **Imágenes Locales + WebP/AVIF**

   ```bash
   # Convertir imágenes
   npx @squoosh/cli --webp auto hero.jpg
   npx @squoosh/cli --avif auto hero.jpg
   ```

   ```tsx
   <picture>
     <source srcSet="/images/hero.avif" type="image/avif" />
     <source srcSet="/images/hero.webp" type="image/webp" />
     <img src="/images/hero.jpg" alt="..." />
   </picture>
   ```

2. **Responsive Images**
   ```tsx
   <img
     srcSet="
       /images/hero-400.webp 400w,
       /images/hero-800.webp 800w,
       /images/hero-1200.webp 1200w,
       /images/hero-1920.webp 1920w
     "
     sizes="100vw"
     src="/images/hero-1920.webp"
     alt="..."
   />
   ```

### Media Prioridad

3. **Lazy Load de Secciones**

   ```tsx
   const Gallery = lazy(() => import("./sections/Gallery"));
   const About = lazy(() => import("./sections/About"));
   ```

4. **Service Worker**

   ```bash
   npm install vite-plugin-pwa -D
   ```

5. **Preload de Fuentes**
   ```html
   <link
     rel="preload"
     href="/fonts/inter.woff2"
     as="font"
     type="font/woff2"
     crossorigin
   />
   ```

### Baja Prioridad

6. **Structured Data (JSON-LD)**

   ```html
   <script type="application/ld+json">
     {
       "@context": "https://schema.org",
       "@type": "LocalBusiness",
       "name": "Ink Studio",
       "description": "...",
       "address": { ... }
     }
   </script>
   ```

7. **Critical CSS Inline**
   - Extraer CSS crítico del hero
   - Inline en `<head>`

---

## 🧪 Testing de Performance

### Herramientas

1. **Lighthouse (Chrome DevTools)**

   ```bash
   # Abrir DevTools > Lighthouse > Generate report
   ```

2. **WebPageTest**

   - https://www.webpagetest.org/
   - Test desde múltiples ubicaciones

3. **Chrome DevTools Performance**

   - Grabar interacciones
   - Analizar bottlenecks

4. **Bundle Analyzer**
   ```bash
   npm run build -- --analyze
   ```

### Checklist de Testing

- [ ] Lighthouse audit (Performance > 90)
- [ ] WebPageTest (LCP < 2.5s)
- [ ] Network throttling (3G, 4G)
- [ ] Diferentes dispositivos (móvil, tablet, desktop)
- [ ] Diferentes navegadores (Chrome, Firefox, Safari)
- [ ] Bundle size analysis

---

## 📚 Recursos

### Guías

- [Web.dev Performance](https://web.dev/performance/)
- [Core Web Vitals](https://web.dev/vitals/)
- [Vite Performance](https://vitejs.dev/guide/performance.html)

### Herramientas

- [Lighthouse](https://developers.google.com/web/tools/lighthouse)
- [WebPageTest](https://www.webpagetest.org/)
- [Bundle Analyzer](https://www.npmjs.com/package/rollup-plugin-visualizer)
- [Squoosh](https://squoosh.app/) - Optimización de imágenes

---

## 📝 Notas

**Estado actual:** Excelente base de performance

- Bundle size muy optimizado (80KB gzipped)
- Lazy loading implementado
- Animaciones optimizadas
- Código limpio y eficiente

**Próximos pasos:**

1. Migrar imágenes a local + WebP/AVIF
2. Ejecutar auditoría completa con Lighthouse
3. Implementar mejoras según resultados
4. Documentar métricas reales
