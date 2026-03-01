using backend.Models;

namespace backend.Services
{
    public interface IEmailService
    {
        Task<bool> SendContactNotificationAsync(ContactMessage message);
    }
}
