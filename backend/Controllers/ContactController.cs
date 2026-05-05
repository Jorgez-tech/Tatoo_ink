using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
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
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ContactRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogWarning("Post: Validación fallida. Errores: {Errors}", string.Join(", ", errors));
                return BadRequest();
            }

            _logger.LogInformation("Post: Procesando mensaje de contacto de {Email}", request.Email);

            var contactMessage = await _contactService.ProcessContactMessageAsync(request);

            _logger.LogInformation("Post: Mensaje procesado exitosamente. ID: {ContactId}", contactMessage.Id);
            return Ok(new ContactResponseDto
            {
                Success = true,
                Message = "Mensaje recibido correctamente. Nos pondremos en contacto contigo pronto.",
                Id = contactMessage.Id
            });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(IEnumerable<ContactMessageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll: Admin consultando todos los mensajes de contacto");
            var messages = await _contactService.GetAllMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(ContactMessageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("GetById: Admin consultando mensaje ID {MessageId}", id);
            var message = await _contactService.GetMessageByIdAsync(id);
            return Ok(message);
        }
    }
}
