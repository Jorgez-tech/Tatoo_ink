using backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace backend.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, IConfiguration? configuration, ILogger<Program>? logger = null)
        {
            logger?.LogInformation("Iniciando DbInitializer: creando BD si es necesaria");
            context.Database.EnsureCreated();
            logger?.LogInformation("Base de datos verificada/creada exitosamente");

            // Definir las imágenes de galería con URLs locales optimizadas
            var seedImages = new[]
            {
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-1.webp",
                    Fallback = "/images/gallery/tattoo-art-1.jpg",
                    Alt = "Diseño de tatuaje artístico estilo tradicional",
                    Category = "Art",
                    Photographer = "Unsplash",
                    Description = "Trabajo artístico detallado con estilo neotradicional."
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-2.webp",
                    Fallback = "/images/gallery/tattoo-art-2.jpg",
                    Alt = "Tatuaje en tinta negra con detalles finos",
                    Category = "Black Ink",
                    Photographer = "Unsplash",
                    Description = "Líneas finas y sombreado suave en tinta negra."
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-3.webp",
                    Fallback = "/images/gallery/tattoo-art-3.jpg",
                    Alt = "Máquina de tatuar en primer plano",
                    Category = "Machine",
                    Photographer = "Unsplash",
                    Description = "Primer plano de máquina profesional de alta precisión."
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-4.webp",
                    Fallback = "/images/gallery/tattoo-art-4.jpg",
                    Alt = "Tatuaje geométrico con líneas precisas",
                    Category = "Geometric",
                    Photographer = "Unsplash",
                    Description = "Composición geométrica simétrica con alto contraste."
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-5.webp",
                    Fallback = "/images/gallery/tattoo-art-5.jpg",
                    Alt = "Interior del estudio de tatuajes profesional",
                    Category = "Studio",
                    Photographer = "Unsplash",
                    Description = "Nuestras instalaciones modernas y seguras."
                },
                new GalleryImage
                {
                    Src = "/images/gallery/tattoo-art-6.webp",
                    Fallback = "/images/gallery/tattoo-art-6.jpg",
                    Alt = "Artista tatuador trabajando en cliente",
                    Category = "Artist",
                    Photographer = "Unsplash",
                    Description = "Nuestros artistas brindan una experiencia personalizada y profesional."
                }
            };

            // Obtener imágenes existentes en la BD
            var existingImages = context.GalleryImages.ToList();
            logger?.LogInformation("Galería actual contiene {ImageCount} imágenes", existingImages.Count);

            if (existingImages.Count == 0)
            {
                logger?.LogInformation("BD vacía: insertando {SeedCount} imágenes iniciales", seedImages.Length);
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
                    existing.Description = seed.Description;
                    existing.IsPublic = seed.IsPublic;
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

            logger?.LogInformation("Guardando cambios de galería en BD");
            context.SaveChanges();
            logger?.LogInformation("Imágenes de galería sincronizadas exitosamente");

            SeedBootstrapAdmin(context, configuration, logger);
            SeedBusinessSettings(context, logger);
        }

        private static void SeedBootstrapAdmin(ApplicationDbContext context, IConfiguration? configuration, ILogger<Program>? logger = null)
        {
            var enabled = configuration?.GetValue<bool>("Security:SeedDefaultAdmin") ?? false;
            if (!enabled)
            {
                logger?.LogInformation("SeedDefaultAdmin deshabilitado - omitiendo creación de usuario admin bootstrap");
                return;
            }

            logger?.LogInformation("SeedDefaultAdmin habilitado - verificando credenciales en configuración");
            var email = configuration?["Security:DefaultAdminEmail"]?.Trim().ToLowerInvariant();
            var passwordHash = configuration?["Security:DefaultAdminPasswordHash"]?.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passwordHash))
            {
                logger?.LogError("SeedDefaultAdmin habilitado pero faltan credenciales en configuración");
                throw new InvalidOperationException(
                    "SeedDefaultAdmin esta habilitado, pero faltan Security:DefaultAdminEmail o Security:DefaultAdminPasswordHash.");
            }

            var existingUser = context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                logger?.LogInformation("Usuario admin {Email} ya existe", email);
                if (!string.Equals(existingUser.Role, "admin", StringComparison.OrdinalIgnoreCase) || !existingUser.IsActive)
                {
                    logger?.LogInformation("Actualizando usuario {Email}: role=admin, isActive=true", email);
                    existingUser.Role = "admin";
                    existingUser.IsActive = true;
                    existingUser.UpdatedAt = DateTime.UtcNow;
                    context.SaveChanges();
                }

                return;
            }

            logger?.LogInformation("Creando usuario admin bootstrap con email {Email}", email);
            var adminUser = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                Role = "admin",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Users.Add(adminUser);
            logger?.LogInformation("Guardando usuario admin en BD");
            context.SaveChanges();
            logger?.LogInformation("Usuario admin {Email} creado exitosamente", email);
        }

        private static void SeedBusinessSettings(ApplicationDbContext context, ILogger<Program>? logger = null)
        {
            var existing = context.BusinessSettings.FirstOrDefault();
            if (existing != null)
            {
                logger?.LogInformation("BusinessSettings ya existen - omitiendo seed");
                return;
            }

            logger?.LogInformation("Creando BusinessSettings iniciales");
            var settings = new BusinessSettings
            {
                BusinessName = "Ink Studio",
                BusinessTagline = "Arte en tu Piel",
                BusinessDescription = "Transformamos tus ideas en obras de arte permanentes. Más de 10 años de experiencia creando tatuajes únicos y personalizados.",
                PhoneNumber = "+34 123 456 789",
                EmailAddress = "info@tattoostudio.com",
                Address = "Calle Principal 123, Ciudad",
                InstagramUrl = "#",
                FacebookUrl = "#",
                TwitterUrl = "#",
                Schedule = "Lun - Sáb: 10:00 - 20:00",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.BusinessSettings.Add(settings);
            logger?.LogInformation("Guardando BusinessSettings en BD");
            context.SaveChanges();
            logger?.LogInformation("BusinessSettings creadas exitosamente");
        }
    }
}
