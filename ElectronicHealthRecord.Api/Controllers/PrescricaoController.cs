using ElectronicHealthRecord.Application.DTOs.Prescricao;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers
{
    [ApiController]
    [Route("api/prescricoes")]
    public class PrescricaoController : ControllerBase
    {
        private readonly IPrescricaoService _prescricaoService;

        public PrescricaoController(IPrescricaoService prescricaoService)
        {
            _prescricaoService = prescricaoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PrescricaoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PrescricaoResponse>>> ObterTodos()
        {
            var prescricoes = await _prescricaoService.ObterTodosAsync();
            return Ok(prescricoes);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PrescricaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrescricaoResponse>> ObterPorId(Guid id)
        {
            try
            {
                var prescricao = await _prescricaoService.ObterPorIdAsync(id);
                return Ok(prescricao);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrescricaoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PrescricaoResponse>> Criar([FromBody] CriarPrescricaoRequest request)
        {
            try
            {
                var novaPrescricao = await _prescricaoService.CriarAsync(request);
                return CreatedAtAction(
                    nameof(ObterPorId),
                    new { id = novaPrescricao.Id },
                    novaPrescricao
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
