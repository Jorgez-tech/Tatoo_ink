using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace backend.Tests.Integration;

public class BusinessSettingsEndpointIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public BusinessSettingsEndpointIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPublic_ReturnsBusinessSettings()
    {
        var response = await _client.GetAsync("/api/v1/business-settings");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var settings = await response.Content.ReadFromJsonAsync<BusinessSettingsResponseDto>();
        Assert.NotNull(settings);
        Assert.False(string.IsNullOrWhiteSpace(settings!.BusinessName));
        Assert.False(string.IsNullOrWhiteSpace(settings.Schedule));
    }

    [Fact]
    public async Task GetInternal_WithoutToken_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync("/api/v1/internal/business-settings");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetInternal_WithAdminToken_ReturnsBusinessSettings()
    {
        var token = await GetAccessTokenAsync();
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/internal/business-settings");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var settings = await response.Content.ReadFromJsonAsync<BusinessSettingsResponseDto>();
        Assert.NotNull(settings);
        Assert.False(string.IsNullOrWhiteSpace(settings!.BusinessName));
    }

    [Fact]
    public async Task UpdateInternal_WithoutToken_ReturnsUnauthorized()
    {
        var payload = new BusinessSettingsUpdateDto
        {
            BusinessName = "Ink Studio Updated",
            BusinessTagline = "Arte total",
            BusinessDescription = "Descripción",
            PhoneNumber = "+34 999 999 999",
            EmailAddress = "test@ink.com",
            Address = "Nueva dirección",
            Schedule = "Lun-Vie 10:00-19:00"
        };

        var response = await _client.PatchAsJsonAsync("/api/v1/internal/business-settings", payload);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateInternal_WithAdminToken_UpdatesSettings()
    {
        var token = await GetAccessTokenAsync();

        var payload = new BusinessSettingsUpdateDto
        {
            BusinessName = "Ink Studio Madrid",
            BusinessTagline = "Arte premium en piel",
            BusinessDescription = "Nuevo texto de descripción para pruebas de integración.",
            PhoneNumber = "+34 654 321 000",
            EmailAddress = "hello@inkstudio.com",
            Address = "Avenida Principal 456",
            InstagramUrl = "https://instagram.com/inkstudio",
            FacebookUrl = "https://facebook.com/inkstudio",
            TwitterUrl = "https://x.com/inkstudio",
            Schedule = "Lun-Sab 11:00-20:00"
        };

        var request = new HttpRequestMessage(HttpMethod.Patch, "/api/v1/internal/business-settings")
        {
            Content = JsonContent.Create(payload)
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updated = await response.Content.ReadFromJsonAsync<BusinessSettingsResponseDto>();
        Assert.NotNull(updated);
        Assert.Equal(payload.BusinessName, updated!.BusinessName);
        Assert.Equal(payload.EmailAddress.ToLowerInvariant(), updated.EmailAddress);
        Assert.Equal(payload.Schedule, updated.Schedule);
    }

    private async Task<string> GetAccessTokenAsync()
    {
        const string email = "admin.settings@test.com";
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

        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth/login", new LoginRequestDto
        {
            Email = email,
            Password = password
        });

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        var body = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();
        Assert.NotNull(body);
        return body!.Token;
    }
}
