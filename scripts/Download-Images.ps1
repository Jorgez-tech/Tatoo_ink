# Download-Images.ps1
# Script para descargar imágenes de Unsplash a directorio local
# Uso: .\scripts\Download-Images.ps1

Write-Host "=== Descarga de Imágenes de Unsplash ===" -ForegroundColor Cyan
Write-Host ""

# Verificar que existen los directorios
$dirs = @(
    "public\images\hero",
    "public\images\about",
    "public\images\gallery"
)

foreach ($dir in $dirs) {
    if (-not (Test-Path $dir)) {
        Write-Host "Creando directorio: $dir" -ForegroundColor Yellow
        New-Item -ItemType Directory -Path $dir -Force | Out-Null
    }
}

# Definir imágenes a descargar (versiones de alta calidad)
$images = @(
    @{
        name = "Hero - Tattoo Artist Working"
        url = "https://images.unsplash.com/photo-1761276297550-27567ed50a1e?w=1920&q=85&fit=crop&crop=entropy"
        dest = "public\images\hero\tattoo-artist-original.jpg"
    },
    @{
        name = "About - Tattoo Studio Interior"
        url = "https://images.unsplash.com/photo-1760877611905-0f885a3ce551?w=1200&q=85&fit=crop&crop=entropy"
        dest = "public\images\about\studio-interior-original.jpg"
    },
    @{
        name = "Gallery 1 - Tattoo Art Design"
        url = "https://images.unsplash.com/photo-1665085326630-b01fea9a613d?w=800&q=85&fit=crop&crop=entropy"
        dest = "public\images\gallery\tattoo-art-1-original.jpg"
    },
    @{
        name = "Gallery 2 - Tattoo Black Ink"
        url = "https://images.unsplash.com/photo-1721160223584-b3a19f2e0e6a?w=800&q=85&fit=crop&crop=entropy"
        dest = "public\images\gallery\tattoo-art-2-original.jpg"
    },
    @{
        name = "Gallery 3 - Tattoo Machine Close Up"
        url = "https://images.unsplash.com/photo-1753260724749-25110c0ce91c?w=800&q=85&fit=crop&crop=entropy"
        dest = "public\images\gallery\tattoo-art-3-original.jpg"
    },
    @{
        name = "Gallery 4 - Geometric Tattoo"
        url = "https://images.unsplash.com/photo-1604374376934-2df6fad6519b?w=800&q=85&fit=crop&crop=entropy"
        dest = "public\images\gallery\tattoo-art-4-original.jpg"
    },
    @{
        name = "Gallery 5 - Studio Setup"
        url = "https://images.unsplash.com/photo-1760877611905-0f885a3ce551?w=800&q=85&fit=crop&crop=entropy"
        dest = "public\images\gallery\tattoo-art-5-original.jpg"
    },
    @{
        name = "Gallery 6 - Artist at Work"
        url = "https://images.unsplash.com/photo-1761276297550-27567ed50a1e?w=800&q=85&fit=crop&crop=entropy"
        dest = "public\images\gallery\tattoo-art-6-original.jpg"
    }
)

$total = $images.Count
$current = 0
$errors = @()

foreach ($img in $images) {
    $current++
    Write-Host "[$current/$total] Descargando: $($img.name)" -ForegroundColor Green
    Write-Host "    URL: $($img.url)" -ForegroundColor Gray
    Write-Host "    Destino: $($img.dest)" -ForegroundColor Gray
    
    try {
        # Descargar con Invoke-WebRequest
        Invoke-WebRequest -Uri $img.url -OutFile $img.dest -ErrorAction Stop
        
        # Verificar que el archivo existe y tiene tamaño > 0
        $file = Get-Item $img.dest
        if ($file.Length -gt 0) {
            $sizeMB = [math]::Round($file.Length / 1MB, 2)
            Write-Host "    OK - Descargado: $sizeMB MB" -ForegroundColor Cyan
        } else {
            throw "Archivo vacío"
        }
        
        Write-Host ""
    }
    catch {
        $errorMsg = "Error descargando '$($img.name)': $_"
        Write-Host "    ERROR: $_" -ForegroundColor Red
        $errors += $errorMsg
        Write-Host ""
    }
}

Write-Host "=== Resumen ===" -ForegroundColor Cyan
Write-Host "Total: $total imágenes" -ForegroundColor White
Write-Host "Exitosas: $($total - $errors.Count)" -ForegroundColor Green
Write-Host "Errores: $($errors.Count)" -ForegroundColor $(if ($errors.Count -eq 0) { "Green" } else { "Red" })
Write-Host ""

if ($errors.Count -gt 0) {
    Write-Host "Errores encontrados:" -ForegroundColor Red
    foreach ($err in $errors) {
        Write-Host "  - $err" -ForegroundColor Yellow
    }
    Write-Host ""
}

Write-Host "=== Próximos pasos ===" -ForegroundColor Cyan
Write-Host "1. Optimizar imágenes con Squoosh (https://squoosh.app/)" -ForegroundColor White
Write-Host "   - Formato: WebP (calidad 80)" -ForegroundColor Gray
Write-Host "   - Fallback: JPG (calidad 85)" -ForegroundColor Gray
Write-Host ""
Write-Host "2. Renombrar archivos optimizados:" -ForegroundColor White
Write-Host "   - tattoo-artist.webp / tattoo-artist.jpg" -ForegroundColor Gray
Write-Host "   - studio-interior.webp / studio-interior.jpg" -ForegroundColor Gray
Write-Host "   - tattoo-art-1.webp / tattoo-art-1.jpg" -ForegroundColor Gray
Write-Host "   - etc..." -ForegroundColor Gray
Write-Host ""
Write-Host "3. Actualizar config/images.ts con rutas locales" -ForegroundColor White
Write-Host ""
Write-Host "Ver guía completa en: docs/IMAGE-OPTIMIZATION-GUIDE.md" -ForegroundColor Cyan
