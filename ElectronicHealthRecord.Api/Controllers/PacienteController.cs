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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PacienteResponse>> ObterPorId(Guid id)
    {
        var paciente = await _pacienteService.ObterPorIdAsync(id);
        return Ok(paciente);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PacienteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PacienteResponse>> Criar([FromBody] CriarPacienteRequest request)
    {
        var novoPaciente = await _pacienteService.CriarAsync(request);
        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = novoPaciente.Id },
            novoPaciente
        );
    }

    [HttpGet("{id:guid}/historico")]
    [ProducesResponseType(typeof(PaginacaoResponse<RegistroClinicoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaginacaoResponse<RegistroClinicoResponse>>> ObterHistorico(
        Guid id,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 10)
    {
        var request = new PaginacaoRequest(pagina, tamanhoPagina);
        var historico = await _registroClinicoService.ObterHistoricoPorPacienteAsync(id, request);
        return Ok(historico);
    }
}
