using ElectronicHealthRecord.Application.DTOs.Profissional;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<ProfissionalResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProfissionalResponse>>> ObterTodos()
    {
        var profissionais = await _profissionalService.ObterTodosAsync();
        return Ok(profissionais);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProfissionalResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProfissionalResponse>> ObterPorId(Guid id)
    {
        var profissional = await _profissionalService.ObterPorIdAsync(id);
        return Ok(profissional);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProfissionalResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProfissionalResponse>> Criar([FromBody] CriarProfissionalRequest request)
    {
        var novoProfissional = await _profissionalService.CriarAsync(request);
        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = novoProfissional.Id },
            novoProfissional
        );
    }
}
