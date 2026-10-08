using ElectronicHealthRecord.Application.DTOs.Atendimento;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers
{
    [ApiController]
    [Route("api/atendimentos")]
    public class AtendimentoController : ControllerBase
    {
        private readonly IAtendimentoService _atendimentoService;

        public AtendimentoController(IAtendimentoService atendimentoService)
        {
            _atendimentoService = atendimentoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AtendimentoResponse>>> ObterTodos()
        {
            var atendimentos = await _atendimentoService.ObterTodosAsync();
            return Ok(atendimentos);
        }

        [HttpGet("id:guid")]
        [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AtendimentoResponse>> ObterPorId(Guid id)
        {
            try
            {
                var atendimento = await _atendimentoService.ObterPorIdAsync(id);
                return atendimento;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AtendimentoResponse>> Criar(CriarAtendimentoRequest request)
        {
            try
            {
                var atendimento = await _atendimentoService.CriarAsync(request);
                return atendimento;
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("id:guid/realizar")]
        [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AtendimentoResponse>> Realizar(Guid id)
        {
            try
            {
                var atendimento = await _atendimentoService.RealizarAsync(id);
                return atendimento;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPut("id:guid/cancelar")]
        [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AtendimentoResponse>> Cancelar(Guid id)
        {
            try
            {
                var atendimento = await _atendimentoService.CancelarAsync(id);
                return atendimento;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

    }
}
