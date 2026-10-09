using ElectronicHealthRecord.Application.DTOs.Atendimento;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<AtendimentoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AtendimentoResponse>>> ObterTodos()
    {
        var atendimentos = await _atendimentoService.ObterTodosAsync();
        return Ok(atendimentos);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AtendimentoResponse>> ObterPorId(Guid id)
    {
        var atendimento = await _atendimentoService.ObterPorIdAsync(id);
        return Ok(atendimento);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AtendimentoResponse>> Criar([FromBody] CriarAtendimentoRequest request)
    {
        var atendimentoNovo = await _atendimentoService.CriarAsync(request);
        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = atendimentoNovo.Id },
            atendimentoNovo
        );
    }

    [HttpPut("{id:guid}/realizar")]
    [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<AtendimentoResponse>> Realizar(Guid id)
    {
        var atendimento = await _atendimentoService.RealizarAsync(id);
        return Ok(atendimento);
    }

    [HttpPut("{id:guid}/cancelar")]
    [ProducesResponseType(typeof(AtendimentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<AtendimentoResponse>> Cancelar(Guid id)
    {
        var atendimento = await _atendimentoService.CancelarAsync(id);
        return Ok(atendimento);
    }
}
