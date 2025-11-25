using backend.Models;

namespace backend.Services
{
    public interface IContactService
    {
        Task<ServiceResult> ProcessContactMessageAsync(ContactRequestDto request);
    }
}
