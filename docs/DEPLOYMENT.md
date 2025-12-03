# Guia de Deployment

Guia completa para desplegar Ink Studio en diferentes plataformas.

## Pre-requisitos

Antes de desplegar, asegurate de:

- [ ] Todas las imagenes estan optimizadas
- [ ] Variables de entorno configuradas
- [ ] Meta tags actualizados en `index.html`
- [ ] `robots.txt` y `sitemap.xml` configurados
- [ ] Build de produccion funciona: `npm run build`
- [ ] No hay errores de TypeScript: `npm run build`
- [ ] Lighthouse score > 90 en todas las categorias

---

## Opciones de Hosting

### 1. Vercel (Recomendado)

**Ventajas:**

- Deploy automatico desde Git
- CDN global
- HTTPS automatico
- Preview deployments
- Gratis para proyectos personales

**Pasos:**

1. **Instalar Vercel CLI**

   ```bash
   npm install -g vercel
   ```

2. **Login**

   ```bash
   vercel login
   ```

3. **Deploy**

   ```bash
   vercel
   ```

4. **Configuracion (vercel.json)**

   ```json
   {
     "buildCommand": "npm run build",
     "outputDirectory": "dist",
     "framework": "vite",
     "rewrites": [{ "source": "/(.*)", "destination": "/index.html" }]
   }
   ```

5. **Variables de Entorno**
   - Dashboard de Vercel > Settings > Environment Variables
   - Agregar `VITE_API_URL` si usas backend

**Deploy desde GitHub:**

1. Push a GitHub
2. Importar proyecto en Vercel
3. Deploy automatico en cada push

---

### 2. Netlify

**Ventajas:**

- Similar a Vercel
- Formularios integrados
- Functions serverless
- Gratis para proyectos personales

**Pasos:**

1. **Instalar Netlify CLI**

   ```bash
   npm install -g netlify-cli
   ```

2. **Login**

   ```bash
   netlify login
   ```

3. **Deploy**

   ```bash
   netlify deploy --prod
   ```

4. **Configuracion (netlify.toml)**

   ```toml
   [build]
     command = "npm run build"
     publish = "dist"

   [[redirects]]
     from = "/*"
     to = "/index.html"
     status = 200
   ```

---

### 3. GitHub Pages

**Ventajas:**

- Gratis
- Integrado con GitHub
- Simple para proyectos estaticos

**Pasos:**

1. **Instalar gh-pages**

   ```bash
   npm install -D gh-pages
   ```

2. **Agregar scripts en package.json**

   ```json
   {
     "scripts": {
       "predeploy": "npm run build",
       "deploy": "gh-pages -d dist"
     }
   }
   ```

3. **Configurar base en vite.config.ts**

   ```typescript
   export default defineConfig({
     base: "/nombre-repo/",
     // ...
   });
   ```

4. **Deploy**

   ```bash
   npm run deploy
   ```

5. **Configurar GitHub Pages**
   - Repo > Settings > Pages
   - Source: gh-pages branch

---

### 4. Cloudflare Pages

**Ventajas:**

- CDN ultra-rapido
- Gratis ilimitado
- Workers para funciones serverless

**Pasos:**

1. **Dashboard de Cloudflare Pages**

   - Conectar repositorio de GitHub
   - Build command: `npm run build`
   - Output directory: `dist`

2. **Deploy automatico**
   - Push a GitHub -> Deploy automatico

---

### 5. Servidor Propio (VPS)

**Para proyectos con backend ASP.NET Core**

#### Opcion A: Nginx + Static Files

1. **Build del proyecto**

   ```bash
   npm run build
   ```

2. **Copiar dist/ al servidor**

   ```bash
   scp -r dist/* user@server:/var/www/inkstudio/
   ```

3. **Configurar Nginx**

   ```nginx
   server {
     listen 80;
     server_name inkstudio.cl www.inkstudio.cl;
     root /var/www/inkstudio;
     index index.html;

     location / {
       try_files $uri $uri/ /index.html;
     }

     # Gzip compression
     gzip on;
     gzip_types text/css application/javascript image/svg+xml;
     gzip_min_length 1000;

     # Cache static assets
     location ~* \.(js|css|png|jpg|jpeg|gif|ico|svg|woff|woff2)$ {
       expires 1y;
       add_header Cache-Control "public, immutable";
     }
   }
   ```

4. **SSL con Let's Encrypt**
   ```bash
   sudo certbot --nginx -d inkstudio.cl -d www.inkstudio.cl
   ```

#### Opcion B: ASP.NET Core + wwwroot

1. **Build del frontend**

   ```bash
   npm run build
   ```

2. **Copiar dist/ a wwwroot/**

   ```bash
   cp -r dist/* ../TatooInk.Server/wwwroot/
   ```

3. **Configurar ASP.NET Core**

   ```csharp
   // Program.cs
   app.UseDefaultFiles();
   app.UseStaticFiles();

   app.MapFallbackToFile("index.html");
   ```

4. **Deploy del backend**
   ```bash
   dotnet publish -c Release
   ```

---

## Configuracion de Dominio

### DNS Records

```
Type    Name    Value               TTL
A       @       IP_DEL_SERVIDOR     3600
A       www     IP_DEL_SERVIDOR     3600
CNAME   www     inkstudio.cl        3600
```

### Vercel/Netlify Custom Domain

1. Dashboard > Settings > Domains
2. Add custom domain: `inkstudio.cl`
3. Configurar DNS segun instrucciones
4. Esperar propagacion (hasta 48h)

---

## Variables de Entorno

### Desarrollo (.env.local)

```env
VITE_API_URL=http://localhost:5177
VITE_USE_MOCK_API=true
```

### Produccion

```env
VITE_API_URL=https://api.inkstudio.cl
VITE_USE_MOCK_API=false
```

**Importante:** No commitear archivos `.env` con datos sensibles.

---

## Post-Deployment Checklist

### Funcionalidad

- [ ] Pagina carga correctamente
- [ ] Todas las secciones visibles
- [ ] Navegacion funciona
- [ ] Formulario envia datos
- [ ] Lightbox funciona
- [ ] Responsive en movil/tablet/desktop
- [ ] Imagenes cargan correctamente

### SEO

- [ ] Meta tags correctos
- [ ] Open Graph funciona (test: https://www.opengraph.xyz/)
- [ ] Twitter Card funciona
- [ ] Sitemap accesible: `/sitemap.xml`
- [ ] Robots.txt accesible: `/robots.txt`
- [ ] Google Search Console configurado
- [ ] Google Analytics (opcional)

### Performance

- [ ] Lighthouse score > 90
- [ ] LCP < 2.5s
- [ ] FID < 100ms
- [ ] CLS < 0.1
- [ ] Imagenes optimizadas
- [ ] Gzip/Brotli habilitado

### Seguridad

- [ ] HTTPS habilitado
- [ ] SSL certificate valido
- [ ] Headers de seguridad configurados
- [ ] CORS configurado (si hay backend)

### Monitoreo

- [ ] Google Analytics configurado
- [ ] Error tracking (Sentry, opcional)
- [ ] Uptime monitoring (UptimeRobot, opcional)

---

## CI/CD con GitHub Actions

**Archivo:** `.github/workflows/deploy.yml`

```yaml
name: Deploy to Production

on:
  push:
    branches: [main]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v3

      - name: Setup Node.js
        uses: actions/setup-node@v3
        with:
          node-version: "18"
          cache: "npm"

      - name: Install dependencies
        run: npm ci

      - name: Build
        run: npm run build
        env:
          VITE_API_URL: ${{ secrets.VITE_API_URL }}

      - name: Deploy to Vercel
        uses: amondnet/vercel-action@v20
        with:
          vercel-token: ${{ secrets.VERCEL_TOKEN }}
          vercel-org-id: ${{ secrets.VERCEL_ORG_ID }}
          vercel-project-id: ${{ secrets.VERCEL_PROJECT_ID }}
          vercel-args: "--prod"
```

---

## Troubleshooting

### Problema: 404 en rutas

**Solucion:** Configurar rewrites/redirects

**Vercel:**

```json
{
  "rewrites": [{ "source": "/(.*)", "destination": "/index.html" }]
}
```

**Netlify:**

```toml
[[redirects]]
  from = "/*"
  to = "/index.html"
  status = 200
```

### Problema: Imagenes no cargan

**Solucion:** Verificar rutas

- Rutas absolutas: `/images/hero.jpg`
- Rutas en `public/`: Accesibles directamente
- Rutas en `src/`: Importar con `import`

### Problema: Variables de entorno no funcionan

**Solucion:** Prefijo `VITE_`

```env
# Incorrecto
API_URL=https://api.example.com

# Correcto
VITE_API_URL=https://api.example.com
```

### Problema: Build falla

**Solucion:** Verificar errores

```bash
# Limpiar cache
rm -rf node_modules dist
npm install
npm run build
```

---

## Recursos

### Hosting

- [Vercel Docs](https://vercel.com/docs)
- [Netlify Docs](https://docs.netlify.com/)
- [Cloudflare Pages](https://pages.cloudflare.com/)

### Performance

- [Web.dev](https://web.dev/)
- [Lighthouse](https://developers.google.com/web/tools/lighthouse)

### SEO

- [Google Search Console](https://search.google.com/search-console)
- [Open Graph Debugger](https://www.opengraph.xyz/)

---

## Notas

**Recomendacion:** Usar Vercel o Netlify para proyectos frontend-only.

**Para proyectos con backend:** Considerar VPS o servicios como Railway, Render, o Azure.

**Costos estimados:**

- Vercel/Netlify: Gratis (hobby)
- VPS basico: $5-10/mes
- Dominio: $10-15/ano

## Prohibicion de emojis

**NOTA:** Por decision de estilo y compatibilidad, los emojis estan prohibidos en todo el proyecto y documentacion. Utiliza solo texto plano y simbolos ASCII.
