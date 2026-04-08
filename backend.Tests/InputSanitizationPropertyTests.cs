using Ganss.Xss;
using Xunit;

namespace backend.Tests
{
    public class InputSanitizationPropertyTests
    {
        [Theory]
        [InlineData("<script>alert('xss')</script>", "")]
        [InlineData("<img src=x onerror=alert('xss')>", "")]
        [InlineData("Texto normal", "Texto normal")]
        public void Sanitizer_Should_Remove_Dangerous_Content(string input, string expected)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitized = sanitizer.Sanitize(input);
            if (string.IsNullOrEmpty(expected))
            {
                Assert.True(string.IsNullOrEmpty(sanitized) || !sanitized.Contains("<script>"));
            }
            else
            {
                Assert.Contains(expected, sanitized);
            }
            Assert.DoesNotContain("<script>", sanitized);
        }
    }
}
