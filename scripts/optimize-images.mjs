// optimize-images.mjs
// Script para optimizar imágenes usando Sharp
// Convierte JPG originales a WebP y JPG optimizados

import sharp from 'sharp';
import { readdir, stat } from 'fs/promises';
import { join, dirname, basename, extname } from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

// Configuración
const CONFIG = {
    hero: {
        width: 1920,
        height: 1080,
        webpQuality: 80,
        jpgQuality: 85
    },
    about: {
        width: 1200,
        height: 800,
        webpQuality: 80,
        jpgQuality: 85
    },
    gallery: {
        width: 800,
        height: 600,
        webpQuality: 80,
        jpgQuality: 85
    }
};

// Función principal
async function optimizeImages() {
    console.log('=== Optimización de Imágenes con Sharp ===\n');

    const folders = [
        { path: 'public/images/hero', config: CONFIG.hero },
        { path: 'public/images/about', config: CONFIG.about },
        { path: 'public/images/gallery', config: CONFIG.gallery }
    ];

    let totalOptimized = 0;
    let totalErrors = 0;

    for (const folder of folders) {
        console.log(`Procesando: ${folder.path}`);

        try {
            const files = await readdir(folder.path);
            const originals = files.filter(f => f.includes('-original.jpg'));

            for (const file of originals) {
                const inputPath = join(folder.path, file);
                const baseName = file.replace('-original.jpg', '');

                console.log(`  Optimizando: ${baseName}`);

                try {
                    // Verificar tamaño original
                    const stats = await stat(inputPath);
                    const originalSizeMB = (stats.size / 1024 / 1024).toFixed(2);
                    console.log(`    Original: ${originalSizeMB} MB`);

                    // Generar WebP optimizado
                    const webpPath = join(folder.path, `${baseName}.webp`);
                    await sharp(inputPath)
                        .resize(folder.config.width, folder.config.height, {
                            fit: 'cover',
                            position: 'center'
                        })
                        .webp({
                            quality: folder.config.webpQuality,
                            effort: 6 // Mayor esfuerzo de compresión
                        })
                        .toFile(webpPath);

                    const webpStats = await stat(webpPath);
                    const webpSizeMB = (webpStats.size / 1024 / 1024).toFixed(2);
                    const webpReduction = ((1 - webpStats.size / stats.size) * 100).toFixed(1);
                    console.log(`    WebP: ${webpSizeMB} MB (reducción: ${webpReduction}%)`);

                    // Generar JPG optimizado
                    const jpgPath = join(folder.path, `${baseName}.jpg`);
                    await sharp(inputPath)
                        .resize(folder.config.width, folder.config.height, {
                            fit: 'cover',
                            position: 'center'
                        })
                        .jpeg({
                            quality: folder.config.jpgQuality,
                            mozjpeg: true // Usar mozjpeg para mejor compresión
                        })
                        .toFile(jpgPath);

                    const jpgStats = await stat(jpgPath);
                    const jpgSizeMB = (jpgStats.size / 1024 / 1024).toFixed(2);
                    const jpgReduction = ((1 - jpgStats.size / stats.size) * 100).toFixed(1);
                    console.log(`    JPG: ${jpgSizeMB} MB (reducción: ${jpgReduction}%)`);

                    totalOptimized += 2; // WebP + JPG
                    console.log(`    ✓ Completado\n`);

                } catch (err) {
                    console.error(`    ✗ Error: ${err.message}\n`);
                    totalErrors++;
                }
            }

        } catch (err) {
            console.error(`  Error leyendo carpeta: ${err.message}\n`);
            totalErrors++;
        }
    }

    // Resumen
    console.log('=== Resumen ===');
    console.log(`Archivos optimizados: ${totalOptimized}`);
    console.log(`Errores: ${totalErrors}`);
    console.log('');

    if (totalErrors === 0) {
        console.log('✓ Optimización completada exitosamente');
        console.log('');
        console.log('Próximos pasos:');
        console.log('1. Actualizar src/config/images.ts con rutas locales');
        console.log('2. Verificar que ImageWithFallback soporte <picture>');
        console.log('3. Probar en dev server (npm run dev)');
        console.log('4. Ejecutar build y verificar bundle size');
    }
}

// Ejecutar
optimizeImages().catch(err => {
    console.error('Error fatal:', err);
    process.exit(1);
});
