import { chromium } from '@playwright/test';

async function testLandingPage() {
    console.log('🚀 Iniciando pruebas de la landing page...\n');

    const browser = await chromium.launch({ headless: false });
    const context = await browser.newContext({
        viewport: { width: 1920, height: 1080 }
    });
    const page = await context.newPage();

    try {
        // 1. Navegar a la página
        console.log('📍 Navegando a http://localhost:5173...');
        await page.goto('http://localhost:5173', { waitUntil: 'networkidle' });
        console.log('✅ Página cargada\n');

        // 2. Tomar screenshot inicial
        console.log('📸 Tomando screenshot...');
        await page.screenshot({ path: 'test-results/01-landing-page.png', fullPage: true });
        console.log('✅ Screenshot guardado: test-results/01-landing-page.png\n');

        // 3. Verificar título
        const title = await page.title();
        console.log(`📄 Título: "${title}"`);

        // 4. Revisar consola
        console.log('\n🔍 Errores en consola:');
        const consoleErrors = [];
        page.on('console', msg => {
            if (msg.type() === 'error') {
                consoleErrors.push(msg.text());
                console.log('❌', msg.text());
            }
        });

        // 5. Verificar secciones principales
        console.log('\n📦 Verificando secciones...');
        const sections = ['nav', 'header', 'main', 'footer'];
        for (const section of sections) {
            const exists = await page.locator(section).count() > 0;
            console.log(`${exists ? '✅' : '❌'} <${section}>`);
        }

        // 6. Probar scroll
        console.log('\n⬇️ Probando scroll...');
        await page.evaluate(() => window.scrollTo(0, 500));
        await page.waitForTimeout(1000);
        await page.screenshot({ path: 'test-results/02-scrolled.png' });
        console.log('✅ Screenshot con scroll: test-results/02-scrolled.png');

        // 7. Buscar botones y enlaces
        console.log('\n🔘 Elementos interactivos encontrados:');
        const buttons = await page.locator('button').count();
        const links = await page.locator('a').count();
        console.log(`   Botones: ${buttons}`);
        console.log(`   Enlaces: ${links}`);

        // 8. Probar responsive
        console.log('\n📱 Probando viewport móvil...');
        await page.setViewportSize({ width: 375, height: 667 });
        await page.evaluate(() => window.scrollTo(0, 0));
        await page.waitForTimeout(500);
        await page.screenshot({ path: 'test-results/03-mobile.png', fullPage: true });
        console.log('✅ Screenshot móvil: test-results/03-mobile.png');

        // 9. Probar viewport tablet
        console.log('\n📱 Probando viewport tablet...');
        await page.setViewportSize({ width: 768, height: 1024 });
        await page.evaluate(() => window.scrollTo(0, 0));
        await page.waitForTimeout(500);
        await page.screenshot({ path: 'test-results/04-tablet.png', fullPage: true });
        console.log('✅ Screenshot tablet: test-results/04-tablet.png');

        // 10. Volver a desktop
        await page.setViewportSize({ width: 1920, height: 1080 });
        await page.evaluate(() => window.scrollTo(0, 0));

        console.log('\n✨ Pruebas completadas exitosamente!\n');
        console.log('📂 Screenshots guardados en: test-results/');

    } catch (error) {
        console.error('❌ Error durante las pruebas:', error);
    } finally {
        await browser.close();
    }
}

testLandingPage();
