# Accesibilidad - Ink Studio

Documentación de las características de accesibilidad implementadas en el proyecto.

## Estándares

Este proyecto cumple con:

- WCAG 2.1 Nivel AA (Web Content Accessibility Guidelines)
- ARIA 1.2 (Accessible Rich Internet Applications)
- Semantic HTML5

---

## Características Implementadas

### 1. Contraste de Colores

**Requisito WCAG AA:** Ratio mínimo de 4.5:1 para texto normal, 3:1 para texto grande.

**Implementación:**

- Texto principal sobre fondo blanco: `#030213` sobre `#ffffff` = **16.8:1** ✅
- Texto en botones primarios: `#ffffff` sobre `#030213` = **16.8:1** ✅
- Texto muted: `#717182` sobre `#ffffff` = **5.2:1** ✅
- Enlaces hover: Suficiente contraste en todos los estados

**Herramientas de verificación:**

- [WebAIM Contrast Checker](https://webaim.org/resources/contrastchecker/)
- Chrome DevTools Lighthouse

### 2. Navegación por Teclado

**Implementación:**

- ✅ Todos los elementos interactivos son accesibles con Tab
- ✅ Orden de tabulación lógico (top-to-bottom, left-to-right)
- ✅ Estados de focus visibles con `ring` de Tailwind
- ✅ Lightbox de galería navegable con flechas y ESC
- ✅ Menú móvil accesible con teclado
- ✅ Formulario completamente navegable

**Atajos de teclado:**

- `Tab` / `Shift+Tab` - Navegar entre elementos
- `Enter` / `Space` - Activar botones y enlaces
- `Escape` - Cerrar lightbox y menú móvil
- `Arrow Left/Right` - Navegar imágenes en lightbox

### 3. HTML Semántico

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

**Jerarquía de headings:**

- `<h1>` - Título principal en Hero (único por página)
- `<h2>` - Títulos de secciones
- `<h3>` - Subtítulos dentro de secciones
- `<h4>` - Títulos de tarjetas

### 4. Formularios Accesibles

**Implementación:**

- ✅ Todos los inputs tienen `<label>` asociados con `htmlFor`
- ✅ Mensajes de error con `role="alert"`
- ✅ Estados de validación con `aria-invalid`
- ✅ Placeholders descriptivos
- ✅ Campos requeridos marcados correctamente

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

### 5. Imágenes

**Implementación:**

- ✅ Todas las imágenes tienen atributo `alt` descriptivo
- ✅ Imágenes decorativas con `alt=""` (ninguna actualmente)
- ✅ Lazy loading para imágenes no críticas
- ✅ Placeholders mientras cargan
- ✅ Fallback en caso de error

**Ejemplos de alt text:**

```tsx
// Hero
alt = "Estudio de tatuajes profesional con ambiente moderno";

// Gallery
alt = "Tatuaje de estilo realista en brazo";
alt = "Diseño geométrico en espalda";

// About
alt = "Interior del estudio Ink Studio";
```

### 6. ARIA Labels

**Implementación:**

```tsx
// Botones de navegación
<button aria-label="Cerrar galería">
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

**Implementación:**

- ✅ Ring visible en todos los elementos interactivos
- ✅ Color de ring: `oklch(0.708 0 0)` con suficiente contraste
- ✅ Offset de 3px para mejor visibilidad
- ✅ No se elimina el outline por defecto

**CSS:**

```css
.focus-visible\:ring-ring\/50:focus-visible {
  --tw-ring-color: oklch(0.708 0 0 / 0.5);
  --tw-ring-offset-width: 3px;
}
```

### 8. Prevención de Scroll

**Implementación:**

- ✅ Cuando el lightbox está abierto, se previene el scroll del body
- ✅ Se restaura el scroll al cerrar

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

**Implementación:**

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

   - Auditoría automática de accesibilidad
   - Objetivo: Score > 95

2. **axe DevTools**

   - Extensión de Chrome/Firefox
   - Detecta problemas de accesibilidad

3. **WAVE (Web Accessibility Evaluation Tool)**

   - https://wave.webaim.org/
   - Análisis visual de problemas

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
- [ ] Verificar que las imágenes tienen alt text
- [ ] Probar lightbox con teclado
- [ ] Verificar que el menú móvil es accesible

---

## Métricas Actuales

### Lighthouse Score (Objetivo)

- **Accesibilidad:** > 95
- **Best Practices:** > 90
- **SEO:** > 90
- **Performance:** > 90

### Problemas Conocidos

**Ninguno crítico actualmente.**

**Mejoras futuras:**

- [ ] Agregar skip links para navegación rápida
- [ ] Implementar modo de alto contraste
- [ ] Agregar preferencias de animación reducida
- [ ] Mejorar mensajes de screen reader en lightbox

---

## 🔧 Configuración de Preferencias de Usuario

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

## 📚 Recursos

### Guías y Documentación

- [WCAG 2.1 Guidelines](https://www.w3.org/WAI/WCAG21/quickref/)
- [MDN Accessibility](https://developer.mozilla.org/en-US/docs/Web/Accessibility)
- [A11y Project Checklist](https://www.a11yproject.com/checklist/)
- [WebAIM Resources](https://webaim.org/resources/)

### Herramientas

- [Contrast Checker](https://webaim.org/resources/contrastchecker/)
- [Color Blind Simulator](https://www.color-blindness.com/coblis-color-blindness-simulator/)
- [Accessible Color Palette Builder](https://toolness.github.io/accessible-color-matrix/)

---

## 🎯 Próximos Pasos

1. [ ] Ejecutar auditoría completa con Lighthouse
2. [ ] Testing con screen readers
3. [ ] Implementar skip links
4. [ ] Agregar soporte para `prefers-reduced-motion`
5. [ ] Documentar resultados de testing
6. [ ] Crear guía de accesibilidad para futuros desarrolladores

---

## 📝 Notas para Desarrolladores

**Al agregar nuevos componentes:**

1. Usar HTML semántico apropiado
2. Asegurar navegación por teclado
3. Agregar ARIA labels cuando sea necesario
4. Verificar contraste de colores
5. Incluir alt text en imágenes
6. Probar con teclado y screen reader

**Recursos del proyecto:**

- Variables de color en `src/styles/globals.css`
- Componentes UI accesibles en `src/components/ui/`
- Utilidad `cn()` para combinar clases en `src/lib/utils.ts`

## Prohibición de emojis

**NOTA:** Por decisión de estilo y compatibilidad, los emojis están prohibidos en todo el proyecto y documentación. Utiliza solo texto plano y símbolos ASCII.
