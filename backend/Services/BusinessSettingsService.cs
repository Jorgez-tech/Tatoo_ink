using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class BusinessSettingsService : IBusinessSettingsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BusinessSettingsService> _logger;

        public BusinessSettingsService(ApplicationDbContext context, ILogger<BusinessSettingsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BusinessSettingsResponseDto> GetAsync()
        {
            var settings = await GetOrCreateSingletonAsync();
            return ToResponse(settings);
        }

        public async Task<BusinessSettingsResponseDto> GetInternalAsync()
        {
            var settings = await GetOrCreateSingletonAsync();
            return ToResponse(settings);
        }

        public async Task<BusinessSettingsResponseDto> UpdateAsync(BusinessSettingsUpdateDto request, int? updatedByUserId)
        {
            var settings = await GetOrCreateSingletonAsync();

            settings.BusinessName = request.BusinessName.Trim();
            settings.BusinessTagline = request.BusinessTagline.Trim();
            settings.BusinessDescription = request.BusinessDescription.Trim();
            settings.PhoneNumber = request.PhoneNumber.Trim();
            settings.EmailAddress = request.EmailAddress.Trim().ToLowerInvariant();
            settings.Address = request.Address.Trim();
            settings.InstagramUrl = NormalizeOptional(request.InstagramUrl);
            settings.FacebookUrl = NormalizeOptional(request.FacebookUrl);
            settings.TwitterUrl = NormalizeOptional(request.TwitterUrl);
            settings.Schedule = request.Schedule.Trim();
            settings.UpdatedAt = DateTime.UtcNow;
            settings.UpdatedByUserId = updatedByUserId;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Business settings updated by user id: {UserId}", updatedByUserId);

            return ToResponse(settings);
        }

        private async Task<BusinessSettings> GetOrCreateSingletonAsync()
        {
            var settings = await _context.BusinessSettings.FirstOrDefaultAsync();
            if (settings != null)
            {
                return settings;
            }

            settings = new BusinessSettings
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

            _context.BusinessSettings.Add(settings);
            await _context.SaveChangesAsync();
            return settings;
        }

        private static string? NormalizeOptional(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return value.Trim();
        }

        private static BusinessSettingsResponseDto ToResponse(BusinessSettings settings)
        {
            return new BusinessSettingsResponseDto
            {
                Id = settings.Id,
                BusinessName = settings.BusinessName,
                BusinessTagline = settings.BusinessTagline,
                BusinessDescription = settings.BusinessDescription,
                PhoneNumber = settings.PhoneNumber,
                EmailAddress = settings.EmailAddress,
                Address = settings.Address,
                InstagramUrl = settings.InstagramUrl,
                FacebookUrl = settings.FacebookUrl,
                TwitterUrl = settings.TwitterUrl,
                Schedule = settings.Schedule,
                UpdatedAt = settings.UpdatedAt
            };
        }
    }
}
