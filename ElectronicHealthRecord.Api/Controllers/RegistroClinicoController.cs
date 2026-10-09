using ElectronicHealthRecord.Application.DTOs.RegistroClinico;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<RegistroClinicoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RegistroClinicoResponse>>> ObterTodos()
    {
        var registrosClinicos = await _registroClinicoService.ObterTodosAsync();
        return Ok(registrosClinicos);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RegistroClinicoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegistroClinicoResponse>> ObterPorId(Guid id)
    {
        var registroClinico = await _registroClinicoService.ObterPorIdAsync(id);
        return Ok(registroClinico);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegistroClinicoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<RegistroClinicoResponse>> Criar([FromBody] CriarRegistroClinicoRequest request)
    {
        var novoRegistroClinico = await _registroClinicoService.CriarAsync(request);
        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = novoRegistroClinico.Id },
            novoRegistroClinico
        );
    }
}
