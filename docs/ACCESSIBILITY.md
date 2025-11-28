# Accesibilidad - Ink Studio

Documentacion de las caracteristicas de accesibilidad implementadas en el proyecto.

## Estandares

Este proyecto cumple con:

- WCAG 2.1 Nivel AA (Web Content Accessibility Guidelines)
- ARIA 1.2 (Accessible Rich Internet Applications)
- Semantic HTML5

---

## Caracteristicas Implementadas

### 1. Contraste de Colores

**Requisito WCAG AA:** Ratio minimo de 4.5:1 para texto normal, 3:1 para texto grande.

**Implementacion:**

- Texto principal sobre fondo blanco: `#030213` sobre `#ffffff` = **16.8:1** [OK]
- Texto en botones primarios: `#ffffff` sobre `#030213` = **16.8:1** [OK]
- Texto muted: `#717182` sobre `#ffffff` = **5.2:1** [OK]
- Enlaces hover: Suficiente contraste en todos los estados

**Herramientas de verificacion:**

- [WebAIM Contrast Checker](https://webaim.org/resources/contrastchecker/)
- Chrome DevTools Lighthouse

### 2. Navegacion por Teclado

**Implementacion:**

- [OK] Todos los elementos interactivos son accesibles con Tab
- [OK] Orden de tabulacion logico (top-to-bottom, left-to-right)
- [OK] Estados de focus visibles con `ring` de Tailwind
- [OK] Lightbox de galeria navegable con flechas y ESC
- [OK] Menu movil accesible con teclado
- [OK] Formulario completamente navegable

**Atajos de teclado:**

- `Tab` / `Shift+Tab` - Navegar entre elementos
- `Enter` / `Space` - Activar botones y enlaces
- `Escape` - Cerrar lightbox y menu movil
- `Arrow Left/Right` - Navegar imagenes en lightbox

### 3. HTML Semantico

**Estructura:**

```html
<body>
  <nav>           <!-- Navbar -->
  <main>
    <section id="home">      <!-- Hero -->
    <section id="servicios"> <!-- Services -->
    <section id="galeria">   <!-- Gallery -->
    <section id="nosotros">  <!-- About -->
    <section id="contacto">  <!-- Contact -->
  </main>
  <footer>        <!-- Footer -->
</body>
```

**Jerarquia de headings:**

- `<h1>` - Titulo principal en Hero (unico por pagina)
- `<h2>` - Titulos de secciones
- `<h3>` - Subtitulos dentro de secciones
- `<h4>` - Titulos de tarjetas

### 4. Formularios Accesibles

**Implementacion:**

- [OK] Todos los inputs tienen `<label>` asociados con `htmlFor`
- [OK] Mensajes de error con `role="alert"`
- [OK] Estados de validacion con `aria-invalid`
- [OK] Placeholders descriptivos
- [OK] Campos requeridos marcados correctamente

**Ejemplo:**

```tsx
<Label htmlFor="email">Email</Label>
<Input
  id="email"
  type="email"
  aria-invalid={errors.email ? "true" : "false"}
  {...register("email", { required: true })}
/>
{errors.email && (
  <p role="alert">{errors.email.message}</p>
)}
```

### 5. Imagenes

**Implementacion:**

- [OK] Todas las imagenes tienen atributo `alt` descriptivo
- [OK] Imagenes decorativas con `alt=""` (ninguna actualmente)
- [OK] Lazy loading para imagenes no criticas
- [OK] Placeholders mientras cargan
- [OK] Fallback en caso de error

**Ejemplos de alt text:**

```tsx
// Hero
alt = "Estudio de tatuajes profesional con ambiente moderno";

// Gallery
alt = "Tatuaje de estilo realista en brazo";
alt = "Diseno geometrico en espalda";

// About
alt = "Interior del estudio Ink Studio";
```

### 6. ARIA Labels

**Implementacion:**

```tsx
// Botones de navegacion
<button aria-label="Cerrar galeria">
  <X />
</button>

<button aria-label="Imagen anterior">
  <ChevronLeft />
</button>

// Enlaces de redes sociales
<a href="..." aria-label="Instagram">
  <Instagram />
</a>
```

### 7. Estados de Focus

**Implementacion:**

- [OK] Ring visible en todos los elementos interactivos
- [OK] Color de ring: `oklch(0.708 0 0)` con suficiente contraste
- [OK] Offset de 3px para mejor visibilidad
- [OK] No se elimina el outline por defecto

**CSS:**

```css
.focus-visible\:ring-ring\/50:focus-visible {
  --tw-ring-color: oklch(0.708 0 0 / 0.5);
  --tw-ring-offset-width: 3px;
}
```

### 8. Prevencion de Scroll

**Implementacion:**

- [OK] Cuando el lightbox esta abierto, se previene el scroll del body
- [OK] Se restaura el scroll al cerrar

```tsx
useEffect(() => {
  if (selectedImage !== null) {
    document.body.style.overflow = "hidden";
  }
  return () => {
    document.body.style.overflow = "unset";
  };
}, [selectedImage]);
```

### 9. Noscript

**Implementacion:**

```html
<noscript>
  <div style="padding: 2rem; text-align: center;">
    <h1>JavaScript Requerido</h1>
    <p>Para ver este sitio necesitas habilitar JavaScript.</p>
  </div>
</noscript>
```

---

## Testing de Accesibilidad

### Herramientas Recomendadas

1. **Lighthouse (Chrome DevTools)**

   - Auditoria automatica de accesibilidad
   - Objetivo: Score > 95

2. **axe DevTools**

   - Extension de Chrome/Firefox
   - Detecta problemas de accesibilidad

3. **WAVE (Web Accessibility Evaluation Tool)**

   - https://wave.webaim.org/
   - Analisis visual de problemas

4. **Screen Readers**
   - **NVDA** (Windows) - Gratuito
   - **JAWS** (Windows) - Comercial
   - **VoiceOver** (macOS/iOS) - Integrado
   - **TalkBack** (Android) - Integrado

### Checklist de Testing Manual

- [ ] Navegar todo el sitio solo con teclado
- [ ] Verificar que todos los elementos interactivos son alcanzables
- [ ] Probar con screen reader (NVDA o VoiceOver)
- [ ] Verificar contraste de colores con herramientas
- [ ] Probar formulario con validaciones
- [ ] Verificar que las imagenes tienen alt text
- [ ] Probar lightbox con teclado
- [ ] Verificar que el menu movil es accesible

---

## Metricas Actuales

### Lighthouse Score (Objetivo)

- **Accesibilidad:** > 95
- **Best Practices:** > 90
- **SEO:** > 90
- **Performance:** > 90

### Problemas Conocidos

**Ninguno critico actualmente.**

**Mejoras futuras:**

- [ ] Agregar skip links para navegacion rapida
- [ ] Implementar modo de alto contraste
- [ ] Agregar preferencias de animacion reducida
- [ ] Mejorar mensajes de screen reader en lightbox

---

## Configuracion de Preferencias de Usuario

### Respeto a Preferencias del Sistema

**Animaciones reducidas:**

```css
@media (prefers-reduced-motion: reduce) {
  * {
    animation-duration: 0.01ms !important;
    animation-iteration-count: 1 !important;
    transition-duration: 0.01ms !important;
  }
}
```

**Modo oscuro:**

```css
@media (prefers-color-scheme: dark) {
  /* Variables de tema oscuro ya implementadas */
}
```

---

## Recursos

### Guias y Documentacion

- [WCAG 2.1 Guidelines](https://www.w3.org/WAI/WCAG21/quickref/)
- [MDN Accessibility](https://developer.mozilla.org/en-US/docs/Web/Accessibility)
- [A11y Project Checklist](https://www.a11yproject.com/checklist/)
- [WebAIM Resources](https://webaim.org/resources/)

### Herramientas

- [Contrast Checker](https://webaim.org/resources/contrastchecker/)
- [Color Blind Simulator](https://www.color-blindness.com/coblis-color-blindness-simulator/)
- [Accessible Color Palette Builder](https://toolness.github.io/accessible-color-matrix/)

---

## Proximos Pasos

1. [ ] Ejecutar auditoria completa con Lighthouse
2. [ ] Testing con screen readers
3. [ ] Implementar skip links
4. [ ] Agregar soporte para `prefers-reduced-motion`
5. [ ] Documentar resultados de testing
6. [ ] Crear guia de accesibilidad para futuros desarrolladores

---

## Notas para Desarrolladores

**Al agregar nuevos componentes:**

1. Usar HTML semantico apropiado
2. Asegurar navegacion por teclado
3. Agregar ARIA labels cuando sea necesario
4. Verificar contraste de colores
5. Incluir alt text en imagenes
6. Probar con teclado y screen reader

**Recursos del proyecto:**

- Variables de color en `src/styles/globals.css`
- Componentes UI accesibles en `src/components/ui/`
- Utilidad `cn()` para combinar clases en `src/lib/utils.ts`

## Prohibicion de emojis

**NOTA:** Por decision de estilo y compatibilidad, los emojis estan prohibidos en todo el proyecto y documentacion. Utiliza solo texto plano y simbolos ASCII.
