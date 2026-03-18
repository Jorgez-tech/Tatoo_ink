using backend.Models;

namespace backend.Services
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken();
    }
}