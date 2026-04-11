using backend.Models;

namespace backend.Services
{
    public interface IContactService
    {
        Task<ServiceResult> ProcessContactMessageAsync(ContactRequestDto request);
        Task<IEnumerable<ContactMessageDto>> GetAllMessagesAsync();
        Task<ContactMessageDto?> GetMessageByIdAsync(int id);
    }
}
