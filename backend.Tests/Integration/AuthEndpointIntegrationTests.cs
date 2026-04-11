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
    public async Task Login_WithValidCredentials_ReturnsTokenAndCookie()
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
        
        // El RefreshToken ya no viene en el body
        Assert.True(string.IsNullOrWhiteSpace(body.RefreshToken));
        
        Assert.NotNull(body.User);
        Assert.Equal(email, body.User.Email);
        Assert.Equal("admin", body.User.Role);

        // Validar cookie de refresh token
        Assert.True(response.Headers.Contains("Set-Cookie"));
        var cookies = response.Headers.GetValues("Set-Cookie").ToList();
        var cookieStr = string.Join(" | ", cookies);
        Assert.True(cookies.Any(c => c.StartsWith("refreshToken=") && c.ToLower().Contains("httponly")), $"Cookies was: {cookieStr}");
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

        var body = await response.Content.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>();
        Assert.NotNull(body);
        Assert.Equal(401, body!.Status);
    }

    [Fact]
    public async Task Refresh_WithValidCookie_ReturnsNewTokenAndCookie()
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
        var cookies = loginResponse.Headers.GetValues("Set-Cookie");
        var refreshTokenCookieParts = cookies.First(c => c.StartsWith("refreshToken=")).Split(';');
        var refreshTokenCookieEntry = refreshTokenCookieParts[0]; // refreshToken=XYZ..

        var refreshReq = new HttpRequestMessage(HttpMethod.Post, "/api/v1/auth/refresh");
        refreshReq.Headers.Add("Cookie", refreshTokenCookieEntry);

        var refreshResponse = await _client.SendAsync(refreshReq);

        Assert.Equal(HttpStatusCode.OK, refreshResponse.StatusCode);
        var refreshBody = await refreshResponse.Content.ReadFromJsonAsync<RefreshResponseDto>();
        Assert.NotNull(refreshBody);
        Assert.False(string.IsNullOrWhiteSpace(refreshBody!.Token));
        Assert.True(string.IsNullOrWhiteSpace(refreshBody.RefreshToken)); // empty in body
        
        Assert.True(refreshResponse.Headers.Contains("Set-Cookie"));
        Assert.Contains(refreshResponse.Headers.GetValues("Set-Cookie"), c => c.StartsWith("refreshToken="));
    }

    [Fact]
    public async Task Logout_WithValidCookie_ReturnsNoContentAndClearsCookie()
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
        var cookies = loginResponse.Headers.GetValues("Set-Cookie");
        var refreshTokenCookieEntry = cookies.First(c => c.StartsWith("refreshToken=")).Split(';')[0];

        var logoutReq = new HttpRequestMessage(HttpMethod.Post, "/api/v1/auth/logout");
        logoutReq.Headers.Add("Cookie", refreshTokenCookieEntry);

        var logoutResponse = await _client.SendAsync(logoutReq);

        Assert.Equal(HttpStatusCode.NoContent, logoutResponse.StatusCode);      
        
        // Verifica que la cookie es borrada/expirada
        Assert.True(logoutResponse.Headers.Contains("Set-Cookie"));
        var logoutCookies = logoutResponse.Headers.GetValues("Set-Cookie");
        Assert.Contains(logoutCookies, c => c.StartsWith("refreshToken=") && c.Contains("expires="));
    }
}
