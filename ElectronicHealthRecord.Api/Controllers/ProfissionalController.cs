using ElectronicHealthRecord.Application.DTOs.Profissional;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace ElectronicHealthRecord.Api.Controllers
{
    [ApiController]
    [Route("api/profissionais")]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalService _profissionalService;

        public ProfissionalController(IProfissionalService profissionalService)
        {
            _profissionalService = profissionalService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProfissionalResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProfissionalResponse>>> ObterTodos()
        {
            var profissionais = await _profissionalService.ObterTodosAsync();
            return Ok(profissionais);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProfissionalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProfissionalResponse>> ObterPorId(Guid id)
        {
            try
            {
                var profissional = await _profissionalService.ObterPorIdAsync(id);
                return Ok(profissional);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProfissionalResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProfissionalResponse>> Criar([FromBody] CriarProfissionalRequest request)
        {
            try
            {
                var novoProfissional = await _profissionalService.CriarAsync(request);
                return CreatedAtAction(
                    nameof(ObterPorId),
                    new { id = novoProfissional.Id },
                    novoProfissional
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
