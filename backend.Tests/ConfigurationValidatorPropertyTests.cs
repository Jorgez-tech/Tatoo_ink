using Microsoft.Extensions.Configuration;
using Xunit;
using backend.Utils;
using System;
using System.Collections.Generic;

namespace backend.Tests
{
    public class ConfigurationValidatorPropertyTests
    {
        [Fact]
        public void Should_Throw_If_ConnectionString_Missing()
        {
            var dict = new Dictionary<string, string>
            {
                {"EmailSettings:Provider", "sendgrid"},
                {"EmailSettings:SendGridApiKey", "key"},
                {"EmailSettings:StudioEmail", "studio@email.com"},
                {"CorsSettings:AllowedOrigins:0", "http://localhost"}
            };
            var config = new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
            Assert.Throws<Exception>(() => ConfigurationValidator.Validate(config));
        }

        [Fact]
        public void Should_Throw_If_EmailSettings_Missing()
        {
            var dict = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "conn"},
                {"EmailSettings:Provider", "sendgrid"},
                {"EmailSettings:StudioEmail", "studio@email.com"},
                {"CorsSettings:AllowedOrigins:0", "http://localhost"}
            };
            var config = new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
            Assert.Throws<Exception>(() => ConfigurationValidator.Validate(config));
        }

        [Fact]
        public void Should_Throw_If_Cors_Missing()
        {
            var dict = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "conn"},
                {"EmailSettings:Provider", "sendgrid"},
                {"EmailSettings:SendGridApiKey", "key"},
                {"EmailSettings:StudioEmail", "studio@email.com"}
            };
            var config = new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
            Assert.Throws<Exception>(() => ConfigurationValidator.Validate(config));
        }

        [Fact]
        public void Should_Not_Throw_If_Config_Valid()
        {
            var dict = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "conn"},
                {"EmailSettings:Provider", "sendgrid"},
                {"EmailSettings:SendGridApiKey", "key"},
                {"EmailSettings:StudioEmail", "studio@email.com"},
                {"CorsSettings:AllowedOrigins:0", "http://localhost"}
            };
            var config = new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
            ConfigurationValidator.Validate(config);
        }
    }
}
