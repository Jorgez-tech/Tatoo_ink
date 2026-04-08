using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        /// <summary>
        /// Recibe un mensaje de contacto desde el formulario del frontend
        /// </summary>
        /// <param name="request">Datos del mensaje de contacto</param>
        /// <returns>Respuesta indicando si el mensaje fue procesado exitosamente</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ContactRequestDto request)
        {
            // Validar modelo (FluentValidation se ejecuta automáticamente)
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogWarning("Validación fallida para mensaje de contacto. Errores: {Errors}", string.Join(", ", errors));

                return BadRequest(new ContactResponseDto
                {
                    Success = false,
                    Message = "Datos de entrada inválidos"
                });
            }

            _logger.LogInformation("Procesando mensaje de contacto de {Email}", request.Email);

            // Procesar mensaje
            var result = await _contactService.ProcessContactMessageAsync(request);

            if (result.Success)
            {
                _logger.LogInformation("Mensaje de contacto procesado exitosamente. ID: {ContactId}", result.Id);

                return Ok(new ContactResponseDto
                {
                    Success = true,
                    Message = "Mensaje recibido correctamente. Nos pondremos en contacto contigo pronto.",
                    Id = result.Id
                });
            }
            else
            {
                _logger.LogError("Error al procesar mensaje de contacto: {Error}", result.Error);

                return StatusCode(500, new ContactResponseDto
                {
                    Success = false,
                    Message = "Ocurrió un error al procesar tu mensaje. Por favor, intenta nuevamente."
                });
            }
        }
    }
}
