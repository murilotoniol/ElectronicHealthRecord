using ElectronicHealthRecord.Application.DTOs.Prescricao;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<PrescricaoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PrescricaoResponse>>> ObterTodos()
    {
        var prescricoes = await _prescricaoService.ObterTodosAsync();
        return Ok(prescricoes);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PrescricaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PrescricaoResponse>> ObterPorId(Guid id)
    {
        var prescricao = await _prescricaoService.ObterPorIdAsync(id);
        return Ok(prescricao);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PrescricaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PrescricaoResponse>> Criar([FromBody] CriarPrescricaoRequest request)
    {
        var novaPrescricao = await _prescricaoService.CriarAsync(request);
        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = novaPrescricao.Id },
            novaPrescricao
        );
    }
}
