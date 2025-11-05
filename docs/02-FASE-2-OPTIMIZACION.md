# ⚙️ FASE 2: Optimización de Código

**Estado:** ❌ Pendiente  
**Fecha inicio:** TBD

---

## 2.1 Mejorar Componentes Principales

### Navbar
**Archivo:** `src/components/Navbar.tsx`

**Mejoras Pendientes:**
- [ ] Extraer `menuItems` a archivo de configuración
- [ ] Agregar scroll spy (destacar sección activa)
- [ ] Mejorar animación de menú móvil
- [ ] Hacer sticky navbar con efecto de scroll
- [ ] Tipar props correctamente

### Hero
**Archivo:** `src/components/Hero.tsx`

**Mejoras Pendientes:**
- [ ] Mover imagen a `/public` o usar CDN
- [ ] Implementar lazy loading para imagen
- [ ] Extraer textos a configuración
- [ ] Mejorar responsive (tablet, mobile)
- [ ] Agregar animaciones de entrada

### Services
**Archivo:** `src/components/Services.tsx`

**Mejoras Pendientes:**
- [ ] Mover array `services` a `config/content.ts`
- [ ] Hacer componente data-driven
- [ ] Crear interface `Service`
- [ ] Permitir cantidad variable de servicios
- [ ] Optimizar grid responsive

### Gallery
**Archivo:** `src/components/Gallery.tsx`

**Mejoras Pendientes:**
- [ ] Mover `galleryImages` a configuración
- [ ] Implementar lazy loading de imágenes
- [ ] Mejorar lightbox (navegación, cerrar con ESC)
- [ ] Optimizar imágenes (NextGen formats)
- [ ] Agregar filtros/categorías (opcional)

### About
**Archivo:** `src/components/About.tsx`

**Mejoras Pendientes:**
- [ ] Separar `stats` a configuración
- [ ] Extraer textos a content.ts
- [ ] Crear interface `Stat`
- [ ] Mejorar responsive de estadísticas
- [ ] Optimizar imagen

### Contact
**Archivo:** `src/components/Contact.tsx`

**Mejoras Pendientes:**
- [ ] Mover `contactInfo` a `business-info.ts`
- [ ] Implementar validación de formulario (react-hook-form)
- [ ] Preparar integración con backend
- [ ] Agregar estados de loading/success/error
- [ ] Implementar envío real de emails

### Footer
**Archivo:** `src/components/Footer.tsx`

**Mejoras Pendientes:**
- [ ] Hacer enlaces configurables
- [ ] Extraer información a business-info.ts
- [ ] Agregar año dinámico en copyright
- [ ] Hacer redes sociales configurables
- [ ] Mejorar responsive

---

## 2.2 Separar Datos de Presentación

### Archivos de Configuración a Crear

#### `config/business-info.ts`
```typescript
export const businessInfo = {
  name: "Ink Studio",
  tagline: "Arte en tu Piel",
  description: "Transformamos tus ideas...",
  contact: {
    address: "Calle Principal 123, Ciudad",
    phone: "+34 123 456 789",
    email: "info@tattoostudio.com",
    hours: "Lun - Sáb: 10:00 - 20:00"
  },
  social: {
    instagram: "#",
    facebook: "#",
    twitter: "#"
  }
}
```

#### `config/content.ts`
```typescript
export const heroContent = {
  title: "Arte en tu Piel",
  subtitle: "Transformamos tus ideas...",
  cta: {
    primary: "Reservar Cita",
    secondary: "Ver Galería"
  }
}

export const services = [...]
export const aboutStats = [...]
```

#### `config/images.ts`
```typescript
export const images = {
  hero: "/images/hero.jpg",
  about: "/images/studio.jpg",
  gallery: [...]
}
```

#### `config/navigation.ts`
```typescript
export const menuItems = [
  { label: "Inicio", href: "#" },
  { label: "Servicios", href: "#servicios" },
  ...
]
```

---

## 2.3 Mejorar Tipos TypeScript

### Archivo: `types/index.ts`

**Interfaces a Crear:**
```typescript
export interface Service {
  icon: LucideIcon;
  title: string;
  description: string;
}

export interface Stat {
  icon: LucideIcon;
  value: string;
  label: string;
}

export interface GalleryImage {
  src: string;
  alt: string;
  category?: string;
}

export interface ContactInfo {
  icon: LucideIcon;
  title: string;
  value: string;
}

export interface MenuItem {
  label: string;
  href: string;
}

export interface BusinessInfo {
  name: string;
  tagline: string;
  description: string;
  contact: ContactDetails;
  social: SocialLinks;
}
```

---

## 📊 Progreso Fase 2

- [ ] 2.1 Componentes optimizados: 0/7
- [ ] 2.2 Archivos de configuración: 0/4
- [ ] 2.3 Tipos TypeScript: 0/1

**Siguiente:** Pendiente Fase 1
