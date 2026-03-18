using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
        Task<RefreshResponseDto?> RefreshAsync(RefreshRequestDto request);
        Task<bool> LogoutAsync(LogoutRequestDto request);
    }
}