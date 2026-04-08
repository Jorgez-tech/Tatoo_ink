using System;

namespace backend.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; }
        public object Errors { get; }

        protected ApiException(int statusCode, string message, object errors = null) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
