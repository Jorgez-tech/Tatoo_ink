using backend.Models;

namespace backend.Services
{
    public interface IBusinessSettingsService
    {
        Task<BusinessSettingsResponseDto> GetAsync();
        Task<BusinessSettingsResponseDto> GetInternalAsync();
        Task<BusinessSettingsResponseDto> UpdateAsync(BusinessSettingsUpdateDto request, int? updatedByUserId);
    }
}
