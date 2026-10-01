using ElectronicHealthRecord.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Paciente>> ObterTodos()
    {
        return Ok(new List<Paciente>
        {
            new Paciente("João da Silva", "123.456.789-00", new DateOnly(1990, 1, 1), "(11) 91234-5678"),
            new Paciente("Maria de Souza", "987.654.321-00", new DateOnly(1985, 5, 15), "(21) 99876-5432")
        });
    }
}
