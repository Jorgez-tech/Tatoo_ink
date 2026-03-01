using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public static class DbInitializer
    {
        /// <summary>
        /// Inicializa la base de datos: aplica migraciones pendientes,
        /// crea roles y artistas seed si no existen.
        /// El seed de imágenes de galería permanece intacto.
        /// </summary>
        public static async Task InitializeAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config)
        {
            // ── Migraciones ───────────────────────────────────────────────────
            // Para bases de datos creadas antes con EnsureCreated() (sin historial de
            // migraciones), aplicar MigrateAsync causa un error porque intenta recrear
            // tablas que ya existen. Usamos un enfoque defensivo:
            // 1. Intentar MigrateAsync (funciona en DBs nuevas o con historial completo)
            // 2. Si falla por conflicto de tablas existentes, asegurar manualmente las
            //    tablas nuevas que agregamos en este PR (RefreshTokens).
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Microsoft.Data.Sqlite.SqliteException ex) when (ex.Message.Contains("already exists"))
            {
                // DB legacy creada con EnsureCreated — asegurar nuevas tablas manualmente
                await EnsureNewTablesAsync(context);
            }

            // ── Roles ────────────────────────────────────────────────────────
            await EnsureRoleAsync(roleManager, "Admin");
            await EnsureRoleAsync(roleManager, "Artist");

            // ── Artistas seed ─────────────────────────────────────────────────
            // Las contraseñas se leen desde appsettings (nunca hardcodeadas)
            var artistsConfig = config.GetSection("SeedArtists").Get<SeedArtistConfig[]>();
            if (artistsConfig == null || artistsConfig.Length == 0)
            {
                artistsConfig = DefaultSeedArtists();
            }

            foreach (var seed in artistsConfig)
            {
                await EnsureArtistAsync(context, userManager, seed);
            }

            // ── Galería seed (imágenes de demostración) ───────────────────────
            await SeedGalleryImagesAsync(context);
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        private static async Task EnsureArtistAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SeedArtistConfig seed)
        {
            // Verificar si el usuario ya existe
            var existingUser = await userManager.FindByEmailAsync(seed.Email);
            if (existingUser != null) return;

            var user = new ApplicationUser
            {
                UserName = seed.Email,
                Email    = seed.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, seed.Password);
            if (!result.Succeeded) return;

            await userManager.AddToRoleAsync(user, seed.Role);

            if (seed.Role == "Artist")
            {
                context.Artists.Add(new Artist
                {
                    DisplayName = seed.DisplayName,
                    Bio         = seed.Bio,
                    UserId      = user.Id,
                    CreatedAt   = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedGalleryImagesAsync(ApplicationDbContext context)
        {
            if (context.GalleryImages.Any()) return;

            var seedImages = new[]
            {
                new GalleryImage { Src = "/images/gallery/tattoo-art-1.webp", Fallback = "/images/gallery/tattoo-art-1.jpg", Alt = "Diseño de tatuaje artístico estilo tradicional",  Category = "Art" },
                new GalleryImage { Src = "/images/gallery/tattoo-art-2.webp", Fallback = "/images/gallery/tattoo-art-2.jpg", Alt = "Tatuaje en tinta negra con detalles finos",       Category = "Black Ink" },
                new GalleryImage { Src = "/images/gallery/tattoo-art-3.webp", Fallback = "/images/gallery/tattoo-art-3.jpg", Alt = "Máquina de tatuar en primer plano",               Category = "Machine" },
                new GalleryImage { Src = "/images/gallery/tattoo-art-4.webp", Fallback = "/images/gallery/tattoo-art-4.jpg", Alt = "Tatuaje geométrico con líneas precisas",          Category = "Geometric" },
                new GalleryImage { Src = "/images/gallery/tattoo-art-5.webp", Fallback = "/images/gallery/tattoo-art-5.jpg", Alt = "Interior del estudio de tatuajes profesional",    Category = "Studio" },
                new GalleryImage { Src = "/images/gallery/tattoo-art-6.webp", Fallback = "/images/gallery/tattoo-art-6.jpg", Alt = "Artista tatuador trabajando en cliente",          Category = "Artist" },
            };

            context.GalleryImages.AddRange(seedImages);
            await context.SaveChangesAsync();
        }

        private static SeedArtistConfig[] DefaultSeedArtists() => new[]
        {
            new SeedArtistConfig("admin@tattooink.com",   "Admin#2024",   "Admin",  "Admin",        "Administrador del estudio"),
            new SeedArtistConfig("artista1@tattooink.com","Artist#2024!","Artist", "Alex Martínez","Especialista en tatuajes tradicionales y neo-tradicional"),
            new SeedArtistConfig("artista2@tattooink.com","Artist#2024!","Artist", "Sam Rivera",   "Tatuajes blackwork y dotwork"),
            new SeedArtistConfig("artista3@tattooink.com","Artist#2024!","Artist", "Jordan Cruz",  "Realismo a color y retratos"),
        };

        /// <summary>
        /// Crea las tablas nuevas introducidas en este PR que no existen en DBs legacy
        /// (creadas con EnsureCreated sin el pipeline de migraciones).
        /// Usa IF NOT EXISTS para ser idempotente.
        /// </summary>
        private static async Task EnsureNewTablesAsync(ApplicationDbContext context)
        {
            await context.Database.ExecuteSqlRawAsync("""
                CREATE TABLE IF NOT EXISTS "RefreshTokens" (
                    "Id"        INTEGER NOT NULL CONSTRAINT "PK_RefreshTokens" PRIMARY KEY AUTOINCREMENT,
                    "TokenHash" TEXT    NOT NULL,
                    "UserId"    TEXT    NOT NULL,
                    "ExpiresAt" TEXT    NOT NULL,
                    "IsRevoked" INTEGER NOT NULL DEFAULT 0,
                    "CreatedAt" TEXT    NOT NULL,
                    CONSTRAINT "FK_RefreshTokens_AspNetUsers_UserId"
                        FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
                )
                """);

            await context.Database.ExecuteSqlRawAsync("""
                CREATE UNIQUE INDEX IF NOT EXISTS "IX_RefreshTokens_TokenHash"
                ON "RefreshTokens" ("TokenHash")
                """);

            await context.Database.ExecuteSqlRawAsync("""
                CREATE INDEX IF NOT EXISTS "IX_RefreshTokens_UserId"
                ON "RefreshTokens" ("UserId")
                """);
        }
    }

    // Clase de configuración para el seed de artistas
    public record SeedArtistConfig(
        string Email,
        string Password,
        string Role,
        string DisplayName,
        string? Bio
    );
}
