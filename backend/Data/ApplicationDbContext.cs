using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.WantsAppointment).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.EmailSent).HasDefaultValue(false);
                entity.Property(e => e.EmailSentAt).IsRequired(false);
                entity.HasIndex(e => e.Email);
            });

            modelBuilder.Entity<GalleryImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Src).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Alt).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
