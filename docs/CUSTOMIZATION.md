# 🎨 Guía de Personalización

## Configuración de Información del Negocio

Actualiza `src/config/business-info.ts` para reflejar datos reales del estudio:

```typescript
export const businessInfo = {
  name: "Ink Studio",
  tagline: "Tinta que cuenta historias",
  contact: {
    address: "Av. Principal 123",
    phone: "+54 11 5555-5555",
    email: "contacto@inkstudio.com",
    schedule: "Lunes a Sábado, 10:00 a 20:00"
  },
  stats: [
    /* ... */
  ],
  social: {
    instagram: "https://instagram.com/inkstudio",
    facebook: "https://facebook.com/inkstudio",
    twitter: "https://twitter.com/inkstudio"
  }
};
```

- Ajusta `name` y `tagline` para la identidad del cliente.
- Actualiza `contact` con dirección, teléfono y correo válidos.
- Personaliza `stats` con métricas reales: tatuajes realizados, años de experiencia, artistas, etc.
- Deja en `undefined` o elimina las redes sociales no utilizadas.

## Contenido de Secciones

Modifica `src/config/content.ts` para adaptar textos y mensajes:

- `heroContent`: título, descripción y textos de botones principales.
- `servicesContent` y `services`: describen servicios o estilos de tatuajes.
- `aboutContent`: narrativa del negocio y logros clave.
- `galleryContent`: descripción de la galería y etiquetas de CTA.
- `contactContent`: mensajes del formulario y textos de confirmación.

Consejos:

1. Mantén un tono consistente con la marca.
2. Utiliza frases breves y orientadas a acción para CTAs.
3. Valida que las traducciones o localizaciones mantengan el formato (por ejemplo, plantillas con interpolaciones o placeholders).

## Imágenes y Recursos

Actualiza `src/config/images.ts` para apuntar a nuevos assets:

```typescript
export const heroImage = {
  src: "/assets/hero-nuevo.jpg",
  alt: "Artista tatuando a un cliente"
};
```

- Coloca las nuevas imágenes en `public/` o un CDN accesible.
- Mantén descripciones `alt` claras para accesibilidad.
- Para galerías, procura un mínimo de 6 imágenes para preservar la cuadrícula responsiva.

## Paleta de Colores y Estilos

Edita `src/styles/globals.css` o `tailwind.config.js` para ajustar colores primarios/secundarios:

```css
:root {
  --background: #fff9f4;
  --foreground: #1f1b18;
  --primary: #b34700;
  --primary-foreground: #fff;
}
```

- Utiliza la misma nomenclatura de variables para conservar consistencia.
- Ajusta sombras, gradientes y animaciones según la identidad visual.

## Navegación y Estructura

En `src/config/navigation.ts` modifica el menú principal y CTA:

- Reordena secciones según jerarquía deseada.
- Cambia `navbarCtaText` por el texto del botón principal.
- Asegúrate de que cada `href` coincida con las IDs definidas en componentes.

## Formulario de Contacto

Define los estados y mensajes personalizados en `contactContent.formFields` y `contactContent.successMessage`:

- Cambia placeholders y labels acorde al idioma.
- Ajusta mensajes de error/éxito para alinearse con el tono del cliente.
- Si existe backend propio, actualiza `src/config/api.ts` con la URL real y desactiva `USE_MOCK_API`.

## Checklist de Personalización

- [ ] Información del negocio actualizada
- [ ] Textos y CTAs revisados
- [ ] Navegación adaptada
- [ ] Paleta de colores ajustada
- [ ] Imágenes reemplazadas y optimizadas
- [ ] Formulario conectado a la API correspondiente
- [ ] Pruebas de accesibilidad básicas (contraste, textos alternativos)
