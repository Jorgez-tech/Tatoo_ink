using backend.Models;

namespace backend.Services
{
    public interface IContactService
    {
        Task<ContactMessage> ProcessContactMessageAsync(ContactRequestDto request);
        Task<IEnumerable<ContactMessageDto>> GetAllMessagesAsync();
        Task<ContactMessageDto?> GetMessageByIdAsync(int id);
    }
}
