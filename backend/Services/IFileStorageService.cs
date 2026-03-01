namespace backend.Services
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Guarda un archivo subido y retorna la ruta relativa pública, ej: /uploads/3/imagen.jpg
        /// </summary>
        Task<string> SaveAsync(IFormFile file, int artistId);

        /// <summary>
        /// Elimina un archivo dada su ruta relativa pública.
        /// </summary>
        Task DeleteAsync(string relativePath);
    }
}
