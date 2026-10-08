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

    [HttpGet]
    [ProducesResponseType(typeof(PacienteResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<PacienteResponse>> ObterPorId(Guid id)
    {
        var paciente = await _pacienteService.ObterPorIdAsync(id);
        return Ok(paciente);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PacienteResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<PacienteResponse>> Criar(CriarPacienteRequest request)
    {
        var pacienteCriado = await _pacienteService.CriarAsync(request);
        return Ok(pacienteCriado);
    }
}
