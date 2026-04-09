using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace backend.Exceptions
{
    public class CustomValidationException : ApiException
    {
        public CustomValidationException(object errors, string message = "Unprocessable Entity") 
            : base(StatusCodes.Status422UnprocessableEntity, message, errors)
        {
        }
    }
}