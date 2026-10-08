using ElectronicHealthRecord.Application.DTOs.RegistroClinico;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers
{
    [ApiController]
    [Route("api/registrosclinicos")]
    public class RegistroClinicoController : ControllerBase
    {
        private readonly IRegistroClinicoService _registroClinicoService;

        public RegistroClinicoController(IRegistroClinicoService registroClinicoService)
        {
            _registroClinicoService = registroClinicoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RegistroClinicoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RegistroClinicoResponse>>> ObterTodos()
        {
            var registrosClinicos = await _registroClinicoService.ObterTodosAsync();
            return Ok(registrosClinicos);
        }

        [HttpGet("id:guid")]
        [ProducesResponseType(typeof(RegistroClinicoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegistroClinicoResponse>> ObterPorId(Guid id)
        {
            try
            {
                var registroClinico = await _registroClinicoService.ObterPorIdAsync(id);
                return Ok(registroClinico);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegistroClinicoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegistroClinicoResponse>> Criar([FromBody] CriarRegistroClinicoRequest request)
        {
            try
            {
                var novoRegistroClinico = await _registroClinicoService.CriarAsync(request);
                return CreatedAtAction(
                    nameof(ObterPorId),
                    new { id = novoRegistroClinico.Id },
                    novoRegistroClinico
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
