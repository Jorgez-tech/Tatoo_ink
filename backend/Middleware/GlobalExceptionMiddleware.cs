using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using backend.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace backend.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == StatusCodes.Status500InternalServerError) 
                {
                   _logger.LogError(ex, "Error de negocio procesado. Path: {Path}", context.Request.Path);
                }
                await HandleExceptionAsync(context, ex.StatusCode, ex.Message, ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepci�n global capturada no controlada. Path: {Path}", context.Request.Path);
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Ocurri� un error interno. Por favor, intenta nuevamente.", null);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string title, object? errors)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
            };

            if (errors != null)
            {
                problemDetails.Extensions["errors"] = errors;
            }

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };

            var payload = JsonSerializer.Serialize(problemDetails, jsonOptions);
            return context.Response.WriteAsync(payload);
        }
    }
}
