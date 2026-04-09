using Microsoft.AspNetCore.Http;

namespace backend.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message = "Recurso no encontrado") 
            : base(StatusCodes.Status404NotFound, message)
        {
        }
    }
}