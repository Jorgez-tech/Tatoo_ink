using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace backend.Tests.Integration;

public class AuthEndpointIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public AuthEndpointIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsTokenAndUser()
    {
        const string email = "admin.login@test.com";
        const string password = "SecurePass123!";

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var passwordService = new PasswordService();

            if (!db.Users.Any(u => u.Email == email))
            {
                db.Users.Add(new User
                {
                    Email = email,
                    PasswordHash = passwordService.HashPassword(password),
                    Role = "admin",
                    IsActive = true
                });

                await db.SaveChangesAsync();
            }
        }

        var request = new LoginRequestDto
        {
            Email = email,
            Password = password
        };

        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        Assert.NotNull(body);
        Assert.False(string.IsNullOrWhiteSpace(body!.Token));
        Assert.False(string.IsNullOrWhiteSpace(body.RefreshToken));
        Assert.NotNull(body.User);
        Assert.Equal(email, body.User.Email);
        Assert.Equal("admin", body.User.Role);

        var jwtParts = body.Token.Split('.');
        Assert.Equal(3, jwtParts.Length);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        var request = new LoginRequestDto
        {
            Email = "missing.user@test.com",
            Password = "WrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<AuthErrorResponseDto>();
        Assert.NotNull(body);
        Assert.Equal("INVALID_CREDENTIALS", body!.Error.Code);
    }

    [Fact]
    public async Task Refresh_WithValidToken_ReturnsNewTokens()
    {
        const string email = "refresh.user@test.com";
        const string password = "SecurePass123!";

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var passwordService = new PasswordService();

            if (!db.Users.Any(u => u.Email == email))
            {
                db.Users.Add(new User
                {
                    Email = email,
                    PasswordHash = passwordService.HashPassword(password),
                    Role = "artist",
                    IsActive = true
                });

                await db.SaveChangesAsync();
            }
        }

        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth/login", new LoginRequestDto
        {
            Email = email,
            Password = password
        });

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        var loginBody = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();
        Assert.NotNull(loginBody);

        var refreshResponse = await _client.PostAsJsonAsync("/api/v1/auth/refresh", new RefreshRequestDto
        {
            RefreshToken = loginBody!.RefreshToken
        });

        Assert.Equal(HttpStatusCode.OK, refreshResponse.StatusCode);
        var refreshBody = await refreshResponse.Content.ReadFromJsonAsync<RefreshResponseDto>();
        Assert.NotNull(refreshBody);
        Assert.False(string.IsNullOrWhiteSpace(refreshBody!.Token));
        Assert.False(string.IsNullOrWhiteSpace(refreshBody.RefreshToken));
        Assert.NotEqual(loginBody.RefreshToken, refreshBody.RefreshToken);
    }

    [Fact]
    public async Task Logout_WithValidRefreshToken_ReturnsNoContent()
    {
        const string email = "logout.user@test.com";
        const string password = "SecurePass123!";

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var passwordService = new PasswordService();

            if (!db.Users.Any(u => u.Email == email))
            {
                db.Users.Add(new User
                {
                    Email = email,
                    PasswordHash = passwordService.HashPassword(password),
                    Role = "artist",
                    IsActive = true
                });

                await db.SaveChangesAsync();
            }
        }

        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth/login", new LoginRequestDto
        {
            Email = email,
            Password = password
        });

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        var loginBody = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();
        Assert.NotNull(loginBody);

        var logoutResponse = await _client.PostAsJsonAsync("/api/v1/auth/logout", new LogoutRequestDto
        {
            RefreshToken = loginBody!.RefreshToken
        });

        Assert.Equal(HttpStatusCode.NoContent, logoutResponse.StatusCode);
    }
}