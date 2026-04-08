# Guia de Personalizacion

Esta guia te ayudara a adaptar la landing page de Ink Studio para un nuevo cliente o negocio.

## Tabla de Contenidos

1. Informacion del Negocio
2. Contenido de Secciones
3. Servicios
4. Imagenes
5. Navegacion
6. Colores y Estilos
7. Backend

---

## Informacion del Negocio

**Archivo:** `src/config/business-info.ts`

```typescript
export const businessInfo = {
  name: "Tu Negocio", // Nombre del negocio
  tagline: "Tu Eslogan", // Eslogan corto
  description: "Descripcion...", // Descripcion breve

  contact: {
    address: "Direccion completa",
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
    { icon: Award, value: "10+", label: "Anos" },
    { icon: Star, value: "5.0", label: "Rating" },
  ],
};
```

**Que cambiar:**

- Nombre y eslogan del negocio
- Datos de contacto (direccion, telefono, email, horarios)
- Enlaces a redes sociales (dejar vacio `""` si no aplica)
- Estadisticas destacadas (valores y etiquetas)

---

## Contenido de Secciones

**Archivo:** `src/config/content.ts`

### Hero (Seccion Principal)

```typescript
export const heroContent = {
  title: "Tu Titulo Principal",
  description: "Descripcion que capte la atencion...",
  primaryButton: "Accion Principal",
  secondaryButton: "Accion Secundaria",
};
```

### Services (Servicios)

```typescript
export const servicesContent = {
  title: "Nuestros Servicios",
  description: "Descripcion de tus servicios...",
};
```

### Gallery (Galeria)

```typescript
export const galleryContent = {
  title: "Galeria",
  description: "Muestra tu trabajo...",
};
```

### About (Sobre Nosotros)

```typescript
export const aboutContent = {
  title: "Sobre Nosotros",
  paragraphs: [
    "Primer parrafo de tu historia...",
    "Segundo parrafo con mas detalles...",
    "Tercer parrafo con tu propuesta de valor...",
  ],
};
```

### Contact (Contacto)

```typescript
export const contactContent = {
  title: "Contactanos",
  description: "Estamos aqui para ayudarte...",
  formTitle: "Envianos un mensaje",
  formDescription: "Responderemos pronto...",
  formFields: {
    name: { label: "Nombre", placeholder: "Tu nombre" },
    email: { label: "Email", placeholder: "tu@email.com" },
    phone: { label: "Telefono", placeholder: "+56 9 1234 5678" },
    message: { label: "Mensaje", placeholder: "Cuentanos..." },
  },
  submitButton: "Enviar Mensaje",
  successMessage: "Mensaje enviado con exito!",
};
```

### Footer

```typescript
export const footerContent = {
  description: "Descripcion breve para el footer...",
  quickLinksTitle: "Enlaces Rapidos",
  socialTitle: "Siguenos",
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
    description: "Descripcion del servicio...",
  },
  {
    icon: Sparkles,
    title: "Servicio 2",
    description: "Descripcion del servicio...",
  },
  // Agrega mas servicios segun necesites
];
```

**Iconos disponibles:** Busca en [Lucide Icons](https://lucide.dev/icons/) y actualiza los imports.

---

## Imagenes

**Archivo:** `src/config/images.ts`

```typescript
export const heroImage = {
  src: "https://images.unsplash.com/photo-...",
  alt: "Descripcion de la imagen hero",
};

export const aboutImage = {
  src: "https://images.unsplash.com/photo-...",
  alt: "Descripcion de la imagen about",
};

export const galleryImages = [
  { src: "url-imagen-1", alt: "Descripcion 1" },
  { src: "url-imagen-2", alt: "Descripcion 2" },
  // Agrega mas imagenes
];
```

**Recomendaciones:**

- Usa imagenes de alta calidad (minimo 1920x1080 para hero)
- Optimiza las imagenes antes de subirlas
- Considera usar un CDN o servicio de imagenes
- Para produccion, mueve las imagenes a `/public/images/`

**Migrar a imagenes locales:**

1. Coloca las imagenes en `/public/images/`
2. Actualiza las rutas:
   ```typescript
   src: "/images/hero.jpg";
   ```

---

## Navegacion

**Archivo:** `src/config/navigation.ts`

```typescript
export const menuItems = [
  { label: "Inicio", href: "#home" },
  { label: "Servicios", href: "#servicios" },
  { label: "Galeria", href: "#galeria" },
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

La URL del backend se configura con la variable de entorno `VITE_API_BASE_URL`.

Ejemplos:

```env
# Desarrollo
VITE_API_BASE_URL=http://localhost:5177

# Produccion
VITE_API_BASE_URL=https://tu-dominio-api.cl
```

Ver `docs/BACKEND-INTEGRATION.md` para detalles completos de integracion.

---

## Checklist de Personalizacion

Usa esta lista para asegurarte de personalizar todo:

- [ ] Nombre del negocio en `business-info.ts`
- [ ] Datos de contacto (direccion, telefono, email)
- [ ] Enlaces de redes sociales
- [ ] Estadisticas destacadas
- [ ] Titulo y descripcion del Hero
- [ ] Textos de todas las secciones
- [ ] Lista de servicios con iconos
- [ ] Imagenes (hero, about, galeria)
- [ ] Menu de navegacion
- [ ] Colores y estilos en `globals.css`
- [ ] Configuracion de API/backend
- [ ] Titulo y favicon en `index.html`
- [ ] Manifest en `public/site.webmanifest`

---

## Tips

1. **Manten la consistencia:** Usa el mismo tono de voz en todos los textos
2. **Optimiza imagenes:** Comprime las imagenes antes de subirlas
3. **Prueba responsive:** Verifica en movil, tablet y desktop
4. **Accesibilidad:** Asegurate de que los textos alternativos sean descriptivos
5. **SEO:** Actualiza meta tags en `index.html`

---

## Problemas Comunes

**Las imagenes no cargan:**

- Verifica que las URLs sean correctas
- Si usas imagenes locales, asegurate de que esten en `/public/`

**Los colores no cambian:**

- Limpia la cache del navegador
- Reinicia el servidor de desarrollo

**El formulario no envia:**

- Verifica la configuracion en `api.ts`
- Revisa la consola del navegador para errores

---

Para mas ayuda, consulta:

- `docs/STRUCTURE.md` - Arquitectura del proyecto
- `docs/BACKEND-INTEGRATION.md` - Integracion con backend

---

## Prohibicion de emojis

**NOTA:** Por decision de estilo y compatibilidad, los emojis estan prohibidos en todo el proyecto y documentacion. Utiliza solo texto plano y simbolos ASCII.
