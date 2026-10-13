using ElectronicHealthRecord.Application.DTOs.Auth;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicHealthRecord.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            if (request.Email == "medico@hospital.com" && request.Senha == "123456")
            {
                var token = _tokenService.GerarToken(Guid.NewGuid(), "Dr. Roberto Silva", request.Email, "Medico");
                return Ok(new LoginResponse(token, "Dr. Roberto Silva", request.Email, "Medico"));
            }
            if (request.Email == "recepcao@hospital.com" && request.Senha == "123456")
            {
                var token = _tokenService.GerarToken(Guid.NewGuid(), "Ana Souza", request.Email, "Recepcionista");
                return Ok(new LoginResponse(token, "Ana Souza", request.Email, "Recepcionista"));
            }
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });
        }
    }
}
