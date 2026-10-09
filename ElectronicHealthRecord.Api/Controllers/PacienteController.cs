using ElectronicHealthRecord.Application.DTOs.Comum;
using ElectronicHealthRecord.Application.DTOs.Pacientes;
using ElectronicHealthRecord.Application.DTOs.RegistroClinico;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers;

[ApiController]
[Route("api/pacientes")]
public class PacienteController : ControllerBase
{
    private readonly IPacienteService _pacienteService;
    private readonly IRegistroClinicoService _registroClinicoService;

    public PacienteController(
        IPacienteService pacienteService,
        IRegistroClinicoService registroClinicoService)
    {
        _pacienteService = pacienteService;
        _registroClinicoService = registroClinicoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PacienteResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PacienteResponse>>> ObterTodos()
    {
        var pacientes = await _pacienteService.ObterTodosAsync();
        return Ok(pacientes);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PacienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PacienteResponse>> ObterPorId(Guid id)
    {
        try
        {
            var paciente = await _pacienteService.ObterPorIdAsync(id);
            return Ok(paciente);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(PacienteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PacienteResponse>> Criar([FromBody] CriarPacienteRequest request)
    {
        try
        {
            var novoPaciente = await _pacienteService.CriarAsync(request);
            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = novoPaciente.Id },
                novoPaciente
            );
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet("{id:guid}/historico")]
    [ProducesResponseType(typeof(PaginacaoResponse<RegistroClinicoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaginacaoResponse<RegistroClinicoResponse>>> ObterHistorico(
        Guid id,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 10)
    {
        try
        {
            var request = new PaginacaoRequest(pagina, tamanhoPagina);
            var historico = await _registroClinicoService.ObterHistoricoPorPacienteAsync(id, request);
            return Ok(historico);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
