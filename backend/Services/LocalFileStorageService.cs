namespace backend.Services
{
    /// <summary>
    /// Implementación de almacenamiento local en disco.
    /// Las imágenes se guardan en: {webroot}/uploads/{artistId}/{guid}.{ext}
    /// Se exponen como archivos estáticos en: /uploads/...
    /// </summary>
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<LocalFileStorageService> _logger;

        // Extensiones permitidas para imágenes
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".webp", ".gif"
        };

        // Tamaño máximo: 10 MB
        private const long MaxFileSizeBytes = 10 * 1024 * 1024;

        public LocalFileStorageService(IWebHostEnvironment env, ILogger<LocalFileStorageService> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async Task<string> SaveAsync(IFormFile file, int artistId)
        {
            // Validar tamaño
            if (file.Length > MaxFileSizeBytes)
                throw new InvalidOperationException($"El archivo supera el límite de {MaxFileSizeBytes / 1024 / 1024} MB.");

            // Validar extensión
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                throw new InvalidOperationException($"Extensión no permitida: {extension}. Use JPG, PNG, WEBP o GIF.");

            // Generar nombre único para evitar colisiones
            var fileName = $"{Guid.NewGuid()}{extension}";

            // Directorio físico: wwwroot/uploads/{artistId}/
            var uploadsRoot = Path.Combine(_env.WebRootPath, "uploads", artistId.ToString());
            Directory.CreateDirectory(uploadsRoot);

            var physicalPath = Path.Combine(uploadsRoot, fileName);

            using var stream = new FileStream(physicalPath, FileMode.Create);
            await file.CopyToAsync(stream);

            _logger.LogInformation("Imagen guardada: {Path}", physicalPath);

            // Retornar ruta pública relativa
            return $"/uploads/{artistId}/{fileName}";
        }

        public Task DeleteAsync(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Task.CompletedTask;

            // Normalizar la ruta del webroot para comparaciones consistentes
            var webRoot = Path.GetFullPath(_env.WebRootPath);

            // Construir y normalizar la ruta física resultante
            var physicalPath = Path.GetFullPath(
                Path.Combine(webRoot, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)));

            // Prevención de path traversal: la ruta debe quedar DENTRO de wwwroot
            if (!physicalPath.StartsWith(webRoot + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("Intento de path traversal bloqueado. relativePath: {RelPath}", relativePath);
                return Task.CompletedTask;
            }

            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
                _logger.LogInformation("Imagen eliminada: {Path}", physicalPath);
            }

            return Task.CompletedTask;
        }
    }
}
