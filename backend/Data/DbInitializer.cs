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
                    Src = "https://images.unsplash.com/photo-1665085326630-b01fea9a613d?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBhcnQlMjBkZXNpZ258ZW58MXx8fHwxNzYyMjU2NTg5fDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral",
                    Alt = "Tattoo Art Design 1",
                    Category = "Art"
                },
                new GalleryImage
                {
                    Src = "https://images.unsplash.com/photo-1721160223584-b3a19f2e0e6a?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBibGFjayUyMGlua3xlbnwxfHx8fDE3NjIzMTcyMjh8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral",
                    Alt = "Tattoo Art Design 2",
                    Category = "Black Ink"
                },
                new GalleryImage
                {
                    Src = "https://images.unsplash.com/photo-1753260724749-25110c0ce91c?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBtYWNoaW5lJTIwY2xvc2UlMjB1cHxlbnwxfHx8fDE3NjIzMTcyMjh8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral",
                    Alt = "Tattoo Art Design 3",
                    Category = "Machine"
                },
                new GalleryImage
                {
                    Src = "https://images.unsplash.com/photo-1604374376934-2df6fad6519b?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBnZW9tZXRyaWMlMjB0YXR0b298ZW58MXx8fHwxNzYyMjUwNzAyfDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral",
                    Alt = "Tattoo Art Design 4",
                    Category = "Geometric"
                },
                new GalleryImage
                {
                    Src = "https://images.unsplash.com/photo-1760877611905-0f885a3ce551?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBzdHVkaW8lMjBpbnRlcmlvcnxlbnwxfHx8fDE3NjIyNDQ1OTl8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral",
                    Alt = "Tattoo Art Design 5",
                    Category = "Studio"
                },
                new GalleryImage
                {
                    Src = "https://images.unsplash.com/photo-1761276297550-27567ed50a1e?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBhcnRpc3QlMjB3b3JraW5nfGVufDF8fHx8MTc2MjI1MDcwMHww&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral",
                    Alt = "Tattoo Art Design 6",
                    Category = "Artist"
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
