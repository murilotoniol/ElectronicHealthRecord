using ElectronicHealthRecord.Application.DTOs.Pacientes;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers;

[ApiController]
[Route("api/pacientes")]
public class PacienteController : ControllerBase
{
    private readonly IPacienteService _pacienteService;

    public PacienteController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
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
            return BadRequest(new { mensagem =  ex.Message });
        }
    }
}
