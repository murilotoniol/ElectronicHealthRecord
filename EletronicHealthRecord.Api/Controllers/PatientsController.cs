using ElectronicHealthRecord.Models;
using Microsoft.AspNetCore.Mvc;

namespace EletronicHealthRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAllPatients()
        {
            return Ok(new List<Patient>
            {
                new Patient("John Doe", "123.456.789-00", new DateOnly(1990, 1, 1), "(11) 91234-5678"),
                new Patient("Jane Smith", "987.654.321-00", new DateOnly(1985, 5, 15), "(21) 99876-5432")
            });
        }
    }
}
