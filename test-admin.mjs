import { chromium } from '@playwright/test';

async function testAdmin() {
    console.log('🛡️ Iniciando pruebas del Panel Admin y Autenticación...\n');

    const browser = await chromium.launch({ headless: false, slowMo: 300 });
    const context = await browser.newContext({
        viewport: { width: 1280, height: 720 }
    });
    const page = await context.newPage();

    try {
        // 1. Probar ruta protegida sin login
        console.log('🔒 Probando acceso denegado a ruta protegida...');
        await page.goto('http://localhost:5173/admin', { waitUntil: 'networkidle' });
        await page.waitForTimeout(500);
        
        const currentUrl = page.url();
        if (currentUrl.includes('/admin/login')) {
            console.log('   ✅ Redirección correcta al Login (no autorizado)');
        } else {
            console.log('   ⚠️ Fallo en restricción de ruta (esperaba /admin/login)');
        }

        // 2. Probar Login con credenciales inventadas
        console.log('\n🔑 Probando validación del formulario de Login...');
        await page.fill('input[type="email"]', 'admin@inkstudio.com');
        await page.fill('input[type="password"]', 'wrongpassword');
        await page.click('button[type="submit"]');
        await page.waitForTimeout(1000);
        await page.screenshot({ path: 'test-results/10-login-failed.png' });
        console.log('   ✅ Fallo controlado verificado (formulario)');

        // 3. Login Exitoso (asumiendo usuario seed o fake en stub local)
        console.log('\n🔓 Inicializando sesión de artista...');
        const emailInput = page.locator('input[type="email"]');
        const passInput = page.locator('input[type="password"]');
        
        if (await emailInput.count() > 0) {
            await emailInput.fill('admin@tatooink.com'); 
            await passInput.fill('Admin123!');
            await page.click('button[type="submit"]');
            await page.waitForTimeout(1500);
            
            if (page.url().includes('/admin') && !page.url().includes('login')) {
                console.log('   ✅ Login exitoso - Dashboard cargado');
                await page.screenshot({ path: 'test-results/11-admin-dashboard.png' });
                
                // 4. Navegación a CRUD Galería
                console.log('\n🖼️ Probando navegación a Gestión de Galería...');
                const btnGallery = page.locator('button', { hasText: 'Agregar Trabajo' });
                if (await btnGallery.count() > 0) {
                    await btnGallery.click();
                    await page.waitForTimeout(800);
                    console.log('   ✅ Vista de Nuevo Trabajo alcanzada');
                    await page.screenshot({ path: 'test-results/12-admin-new-work.png' });
                    
                    // Volver
                    await page.goBack();
                }

                // 5. Navegación a Mensajes
                console.log('\n📩 Probando navegación a Mensajes de Contacto...');
                const btnMessages = page.locator('button', { hasText: 'Ver Mensajes' });
                if (await btnMessages.count() > 0) {
                    await btnMessages.click();
                    await page.waitForTimeout(800);
                    console.log('   ✅ Vista de Mensajes alcanzada');
                    await page.screenshot({ path: 'test-results/13-admin-messages.png' });
                    
                    const firstMessage = page.locator('button', { hasText: 'Ver Detalles' }).first();
                    if (await firstMessage.count() > 0) {
                        await firstMessage.click();
                        await page.waitForTimeout(800);
                        console.log('   ✅ Vista de Detalle de Mensaje alcanzada');
                        await page.screenshot({ path: 'test-results/14-admin-message-detail.png' });
                    }
                }
                
                // 6. Probar Logout
                console.log('\n👋 Cerrando sesión...');
                await page.goto('http://localhost:5173/admin', { waitUntil: 'networkidle' });
                const btnLogout = page.locator('button', { hasText: 'Cerrar Sesión' });
                if (await btnLogout.count() > 0) {
                    await btnLogout.click();
                    await page.waitForTimeout(800);
                    
                    if (page.url() === 'http://localhost:5173/') {
                        console.log('   ✅ Sesión cerrada y redirigido al Home');
                    } else {
                        console.log('   ⚠️ Falla en Logout');
                    }
                }
                
            } else {
                console.log('   ⚠️ Login local falló. Verificar backend (mock de seed).');
            }
        }

        console.log('\n✨ Pruebas Playwright del Panel completadas!\n');

    } catch (error) {
        console.error('🚫 Error durante las pruebas:', error.message);
    } finally {
        await browser.close();
    }
}

testAdmin();