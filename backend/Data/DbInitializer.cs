using backend.Models;

namespace backend.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any gallery images.
            if (context.GalleryImages.Any())
            {
                return;   // DB has been seeded
            }

            var images = new GalleryImage[]
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

            foreach (var i in images)
            {
                context.GalleryImages.Add(i);
            }
            context.SaveChanges();
        }
    }
}
