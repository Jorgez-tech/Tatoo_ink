import { chromium } from '@playwright/test';

async function testInteractions() {
    console.log('🎯 Iniciando pruebas de interacción...\n');

    const browser = await chromium.launch({ headless: false, slowMo: 500 });
    const context = await browser.newContext({
        viewport: { width: 1920, height: 1080 }
    });
    const page = await context.newPage();

    try {
        await page.goto('http://localhost:5173', { waitUntil: 'networkidle' });

        // 1. Probar navegación del Navbar
        console.log('🧭 Probando navegación del Navbar...');
        const navLinks = await page.locator('nav a').all();
        console.log(`   Encontrados ${navLinks.length} enlaces en navbar`);

        for (let i = 0; i < Math.min(navLinks.length, 5); i++) {
            const link = navLinks[i];
            const text = await link.textContent();
            const href = await link.getAttribute('href');
            console.log(`   ${i + 1}. "${text}" → ${href}`);

            if (href?.startsWith('#')) {
                await link.click();
                await page.waitForTimeout(800);
                console.log(`      ✅ Click en "${text}" - navegado`);
            }
        }

        // 2. Probar botón CTA del Hero
        console.log('\n🎯 Probando botón CTA del Hero...');
        await page.evaluate(() => window.scrollTo(0, 0));
        await page.waitForTimeout(500);

        const heroButton = page.locator('button, a').filter({ hasText: /contacto|agendar|reserva/i }).first();
        const heroButtonExists = await heroButton.count() > 0;

        if (heroButtonExists) {
            const buttonText = await heroButton.textContent();
            console.log(`   Botón encontrado: "${buttonText}"`);
            await heroButton.click();
            await page.waitForTimeout(1000);
            console.log('   ✅ Click en botón CTA');
            await page.screenshot({ path: 'test-results/05-cta-clicked.png' });
        } else {
            console.log('   ⚠️ No se encontró botón CTA');
        }

        // 3. Buscar y probar galería
        console.log('\n🖼️ Buscando galería de imágenes...');

        // Navegar a la sección de galería
        const galleryLink = page.locator('a[href="#galeria"]').first();
        if (await galleryLink.count() > 0) {
            await galleryLink.click();
            await page.waitForTimeout(1000);
        }

        const galleryImages = await page.locator('#galeria img, [id*="gallery"] img').all();
        console.log(`   Encontradas ${galleryImages.length} imágenes en galería`);

        if (galleryImages.length > 0) {
            console.log('   Intentando click en primera imagen de galería...');
            try {
                await galleryImages[0].click({ timeout: 5000 });
                await page.waitForTimeout(1000);
                await page.screenshot({ path: 'test-results/06-gallery-opened.png' });
                console.log('   ✅ Imagen clickeada');

                // Intentar cerrar lightbox con ESC
                await page.keyboard.press('Escape');
                await page.waitForTimeout(500);
                console.log('   ✅ Presionado ESC para cerrar');
            } catch (error) {
                console.log('   ⚠️ No se pudo hacer click en imagen (puede no tener lightbox)');
                await page.screenshot({ path: 'test-results/06-gallery-view.png' });
            }
        }

        // 4. Probar formulario de contacto
        console.log('\n📝 Probando formulario de contacto...');
        await page.evaluate(() => window.scrollTo(0, document.body.scrollHeight));
        await page.waitForTimeout(500);

        const nameInput = page.locator('input[name="name"], input[placeholder*="nombre" i]').first();
        const emailInput = page.locator('input[type="email"], input[name="email"]').first();
        const messageInput = page.locator('textarea, input[name="message"]').first();

        const formExists = await nameInput.count() > 0 && await emailInput.count() > 0;

        if (formExists) {
            console.log('   Llenando formulario...');
            await nameInput.fill('Usuario de Prueba');
            await emailInput.fill('test@example.com');
            if (await messageInput.count() > 0) {
                await messageInput.fill('Este es un mensaje de prueba desde Playwright');
            }
            await page.screenshot({ path: 'test-results/07-form-filled.png' });
            console.log('   ✅ Formulario llenado (no enviado)');
        } else {
            console.log('   ⚠️ No se encontró formulario de contacto');
        }

        // 5. Probar scroll al inicio
        console.log('\n⬆️ Volviendo al inicio...');
        await page.evaluate(() => window.scrollTo(0, 0));
        await page.waitForTimeout(500);
        await page.screenshot({ path: 'test-results/08-back-to-top.png' });
        console.log('   ✅ Scroll al inicio');

        // 6. Probar navegación por teclado
        console.log('\n⌨️ Probando navegación por teclado...');
        for (let i = 0; i < 5; i++) {
            await page.keyboard.press('Tab');
            await page.waitForTimeout(200);
        }
        await page.screenshot({ path: 'test-results/09-keyboard-nav.png' });
        console.log('   ✅ Navegación con Tab completada');

        // 7. Métricas de performance
        console.log('\n⚡ Recolectando métricas de performance...');
        const metrics = await page.evaluate(() => {
            const perf = performance.getEntriesByType('navigation')[0];
            return {
                domContentLoaded: Math.round(perf.domContentLoadedEventEnd - perf.domContentLoadedEventStart),
                loadComplete: Math.round(perf.loadEventEnd - perf.loadEventStart),
                domInteractive: Math.round(perf.domInteractive - perf.fetchStart),
            };
        });
        console.log('   DOMContentLoaded:', metrics.domContentLoaded, 'ms');
        console.log('   Load Complete:', metrics.loadComplete, 'ms');
        console.log('   DOM Interactive:', metrics.domInteractive, 'ms');

        console.log('\n✨ Pruebas de interacción completadas!\n');

    } catch (error) {
        console.error('❌ Error durante las pruebas:', error.message);
    } finally {
        await browser.close();
    }
}

testInteractions();
