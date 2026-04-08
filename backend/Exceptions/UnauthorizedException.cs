using Microsoft.AspNetCore.Http;

namespace backend.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message = "No autorizado") 
            : base(StatusCodes.Status401Unauthorized, message)
        {
        }
    }
}