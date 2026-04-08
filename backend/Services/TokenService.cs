using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using backend.Models;
using Microsoft.Extensions.Configuration;

namespace backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(User user)
        {
            var secret = _configuration["Security:AccessTokenSecret"];
            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException("Falta Security:AccessTokenSecret para generar tokens.");
            }

            var expiresInMinutes = _configuration.GetValue<int>("Security:AccessTokenMinutes", 15);
            var expUnix = DateTimeOffset.UtcNow.AddMinutes(expiresInMinutes).ToUnixTimeSeconds();

            var header = Base64UrlEncode(JsonSerializer.Serialize(new
            {
                alg = "HS256",
                typ = "JWT"
            }));

            var payload = Base64UrlEncode(JsonSerializer.Serialize(new
            {
                sub = user.Id,
                email = user.Email,
                role = user.Role,
                exp = expUnix
            }));

            var unsignedToken = $"{header}.{payload}";
            var signature = Sign(unsignedToken, secret);

            return $"{unsignedToken}.{signature}";
        }

        public string CreateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
        }

        private static string Sign(string value, string secret)
        {
            var key = Encoding.UTF8.GetBytes(secret);
            using var hmac = new HMACSHA256(key);
            var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));

            return Base64UrlEncode(signature);
        }

        private static string Base64UrlEncode(string value)
        {
            return Base64UrlEncode(Encoding.UTF8.GetBytes(value));
        }

        private static string Base64UrlEncode(byte[] value)
        {
            return Convert.ToBase64String(value)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}