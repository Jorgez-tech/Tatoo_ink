# Guía de Personalización

Esta guía te ayudará a adaptar la landing page de Ink Studio para un nuevo cliente o negocio.

## Tabla de Contenidos

1. Información del Negocio
2. Contenido de Secciones
3. Servicios
4. Imágenes
5. Navegación
6. Colores y Estilos
7. Backend

---

## Información del Negocio

**Archivo:** `src/config/business-info.ts`

```typescript
export const businessInfo = {
  name: "Tu Negocio", // Nombre del negocio
  tagline: "Tu Eslogan", // Eslogan corto
  description: "Descripción...", // Descripción breve

  contact: {
    address: "Dirección completa",
    phone: "+56 9 1234 5678",
    email: "contacto@tunegocio.cl",
    hours: "Lun - Vie: 9:00 - 18:00",
  },

  social: {
    instagram: "https://instagram.com/tunegocio",
    facebook: "https://facebook.com/tunegocio",
    twitter: "https://twitter.com/tunegocio",
  },

  stats: [
    { icon: Users, value: "500+", label: "Clientes" },
    { icon: Award, value: "10+", label: "Años" },
    { icon: Star, value: "5.0", label: "Rating" },
  ],
};
```

**Qué cambiar:**

- Nombre y eslogan del negocio
- Datos de contacto (dirección, teléfono, email, horarios)
- Enlaces a redes sociales (dejar vacío `""` si no aplica)
- Estadísticas destacadas (valores y etiquetas)

---

## Contenido de Secciones

**Archivo:** `src/config/content.ts`

### Hero (Sección Principal)

```typescript
export const heroContent = {
  title: "Tu Título Principal",
  description: "Descripción que capte la atención...",
  primaryButton: "Acción Principal",
  secondaryButton: "Acción Secundaria",
};
```

### Services (Servicios)

```typescript
export const servicesContent = {
  title: "Nuestros Servicios",
  description: "Descripción de tus servicios...",
};
```

### Gallery (Galería)

```typescript
export const galleryContent = {
  title: "Galería",
  description: "Muestra tu trabajo...",
};
```

### About (Sobre Nosotros)

```typescript
export const aboutContent = {
  title: "Sobre Nosotros",
  paragraphs: [
    "Primer párrafo de tu historia...",
    "Segundo párrafo con más detalles...",
    "Tercer párrafo con tu propuesta de valor...",
  ],
};
```

### Contact (Contacto)

```typescript
export const contactContent = {
  title: "Contáctanos",
  description: "Estamos aquí para ayudarte...",
  formTitle: "Envíanos un mensaje",
  formDescription: "Responderemos pronto...",
  formFields: {
    name: { label: "Nombre", placeholder: "Tu nombre" },
    email: { label: "Email", placeholder: "tu@email.com" },
    phone: { label: "Teléfono", placeholder: "+56 9 1234 5678" },
    message: { label: "Mensaje", placeholder: "Cuéntanos..." },
  },
  submitButton: "Enviar Mensaje",
  successMessage: "¡Mensaje enviado con éxito!",
};
```

### Footer

```typescript
export const footerContent = {
  description: "Descripción breve para el footer...",
  quickLinksTitle: "Enlaces Rápidos",
  socialTitle: "Síguenos",
};
```

---

## Servicios

**Archivo:** `src/config/services.ts`

```typescript
import { Palette, Sparkles, Heart, Shield } from "lucide-react";

export const services = [
  {
    icon: Palette,
    title: "Servicio 1",
    description: "Descripción del servicio...",
  },
  {
    icon: Sparkles,
    title: "Servicio 2",
    description: "Descripción del servicio...",
  },
  // Agrega más servicios según necesites
];
```

**Iconos disponibles:** Busca en [Lucide Icons](https://lucide.dev/icons/) y actualiza los imports.

---

## Imágenes

**Archivo:** `src/config/images.ts`

```typescript
export const heroImage = {
  src: "https://images.unsplash.com/photo-...",
  alt: "Descripción de la imagen hero",
};

export const aboutImage = {
  src: "https://images.unsplash.com/photo-...",
  alt: "Descripción de la imagen about",
};

export const galleryImages = [
  { src: "url-imagen-1", alt: "Descripción 1" },
  { src: "url-imagen-2", alt: "Descripción 2" },
  // Agrega más imágenes
];
```

**Recomendaciones:**

- Usa imágenes de alta calidad (mínimo 1920x1080 para hero)
- Optimiza las imágenes antes de subirlas
- Considera usar un CDN o servicio de imágenes
- Para producción, mueve las imágenes a `/public/images/`

**Migrar a imágenes locales:**

1. Coloca las imágenes en `/public/images/`
2. Actualiza las rutas:
   ```typescript
   src: "/images/hero.jpg";
   ```

---

## Navegación

**Archivo:** `src/config/navigation.ts`

```typescript
export const menuItems = [
  { label: "Inicio", href: "#home" },
  { label: "Servicios", href: "#servicios" },
  { label: "Galería", href: "#galeria" },
  { label: "Nosotros", href: "#nosotros" },
  { label: "Contacto", href: "#contacto" },
];

export const navbarCtaText = "Reservar Cita";
```

**Importante:** Los `href` deben coincidir con los `id` de las secciones en los componentes.

---

## Colores y Estilos

**Archivo:** `src/styles/globals.css`

### Variables CSS

```css
:root {
  /* Colores principales */
  --primary: #030213; /* Color principal (negro) */
  --secondary: #ececf0; /* Color secundario (gris claro) */

  /* Colores de fondo */
  --background: 0 0% 100%; /* Fondo blanco */
  --foreground: 240 10% 3.9%; /* Texto principal */

  /* Colores de acento */
  --accent: 240 4.8% 95.9%;
  --accent-foreground: 240 5.9% 10%;

  /* Bordes y separadores */
  --border: 240 5.9% 90%;
  --input: 240 5.9% 90%;
  --ring: 240 5.9% 10%;
}
```

### Cambiar Colores

**Ejemplo: Cambiar a tema azul**

```css
:root {
  --primary: #1e40af; /* Azul oscuro */
  --secondary: #dbeafe; /* Azul claro */
}
```

### Tailwind Config

**Archivo:** `tailwind.config.js`

Para colores personalizados adicionales:

```javascript
module.exports = {
  theme: {
    extend: {
      colors: {
        brand: {
          primary: "#1e40af",
          secondary: "#dbeafe",
        },
      },
    },
  },
};
```

---

## Backend

**Archivo:** `src/config/api.ts`

### Modo Mock (Desarrollo sin backend)

```typescript
export const USE_MOCK_API = true;
```

### Modo Producción (Con backend ASP.NET Core)

```typescript
export const USE_MOCK_API = false;

export const API_BASE_URL = "https://tu-api.com";
// o para desarrollo local:
// export const API_BASE_URL = "https://localhost:7001"
```

Ver `docs/BACKEND-INTEGRATION.md` para detalles completos de integración.

---

## 🚀 Checklist de Personalización

Usa esta lista para asegurarte de personalizar todo:

- [ ] Nombre del negocio en `business-info.ts`
- [ ] Datos de contacto (dirección, teléfono, email)
- [ ] Enlaces de redes sociales
- [ ] Estadísticas destacadas
- [ ] Título y descripción del Hero
- [ ] Textos de todas las secciones
- [ ] Lista de servicios con iconos
- [ ] Imágenes (hero, about, galería)
- [ ] Menú de navegación
- [ ] Colores y estilos en `globals.css`
- [ ] Configuración de API/backend
- [ ] Título y favicon en `index.html`
- [ ] Manifest en `public/site.webmanifest`

---

## 💡 Tips

1. **Mantén la consistencia:** Usa el mismo tono de voz en todos los textos
2. **Optimiza imágenes:** Comprime las imágenes antes de subirlas
3. **Prueba responsive:** Verifica en móvil, tablet y desktop
4. **Accesibilidad:** Asegúrate de que los textos alternativos sean descriptivos
5. **SEO:** Actualiza meta tags en `index.html`

---

## 🆘 Problemas Comunes

**Las imágenes no cargan:**

- Verifica que las URLs sean correctas
- Si usas imágenes locales, asegúrate de que estén en `/public/`

**Los colores no cambian:**

- Limpia la caché del navegador
- Reinicia el servidor de desarrollo

**El formulario no envía:**

- Verifica la configuración en `api.ts`
- Revisa la consola del navegador para errores

---

Para más ayuda, consulta:

- `docs/STRUCTURE.md` - Arquitectura del proyecto
- `docs/BACKEND-INTEGRATION.md` - Integración con backend

---

## Prohibición de emojis

**NOTA:** Por decisión de estilo y compatibilidad, los emojis están prohibidos en todo el proyecto y documentación. Utiliza solo texto plano y símbolos ASCII.
