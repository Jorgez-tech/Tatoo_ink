using backend.Models;

namespace backend.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Definir las imágenes de galería con URLs locales optimizadas
            var seedImages = new[]
            {
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-1.webp",
                    Fallback = "/images/gallery/tattoo-art-1.jpg",
                    Alt = "Diseño de tatuaje artístico estilo tradicional",
                    Category = "Art",
                    Photographer = "Unsplash"
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-2.webp",
                    Fallback = "/images/gallery/tattoo-art-2.jpg",
                    Alt = "Tatuaje en tinta negra con detalles finos",
                    Category = "Black Ink",
                    Photographer = "Unsplash"
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-3.webp",
                    Fallback = "/images/gallery/tattoo-art-3.jpg",
                    Alt = "Máquina de tatuar en primer plano",
                    Category = "Machine",
                    Photographer = "Unsplash"
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-4.webp",
                    Fallback = "/images/gallery/tattoo-art-4.jpg",
                    Alt = "Tatuaje geométrico con líneas precisas",
                    Category = "Geometric",
                    Photographer = "Unsplash"
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-5.webp",
                    Fallback = "/images/gallery/tattoo-art-5.jpg",
                    Alt = "Interior del estudio de tatuajes profesional",
                    Category = "Studio",
                    Photographer = "Unsplash"
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-6.webp",
                    Fallback = "/images/gallery/tattoo-art-6.jpg",
                    Alt = "Artista tatuador trabajando en cliente",
                    Category = "Artist",
                    Photographer = "Unsplash"
                }
            };

            // Obtener imágenes existentes en la BD
            var existingImages = context.GalleryImages.ToList();

            if (existingImages.Count == 0)
            {
                // BD vacía: insertar nuevas imágenes
                foreach (var img in seedImages)
                {
                    context.GalleryImages.Add(img);
                }
            }
            else
            {
                // BD tiene datos: actualizar con URLs locales
                for (int i = 0; i < Math.Min(existingImages.Count, seedImages.Length); i++)
                {
                    var existing = existingImages[i];
                    var seed = seedImages[i];

                    // Actualizar con URLs locales optimizadas
                    existing.Src = seed.Src;
                    existing.Fallback = seed.Fallback;
                    existing.Alt = seed.Alt;
                    existing.Category = seed.Category;
                    existing.Photographer = seed.Photographer;
                }

                // Si hay menos imágenes en BD que en seed, agregar las faltantes
                if (seedImages.Length > existingImages.Count)
                {
                    for (int i = existingImages.Count; i < seedImages.Length; i++)
                    {
                        context.GalleryImages.Add(seedImages[i]);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
