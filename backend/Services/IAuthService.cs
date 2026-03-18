using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}