using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElectronicHealthRecord.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ElectronicHealthRecord.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Guid id, string nome, string email, string role)
        {
            var chaveSecreta = _configuration["Jwt:ChaveSecreta"]
                ?? throw new InvalidOperationException("Chave JWT não configurada");
            var emissor = _configuration["Jwt:Emissor"];
            var publico = _configuration["Jwt:Publico"];
            var expiracaoHoras = int.Parse(_configuration["Jwt:ExpiracaoHoras"] ?? "8");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new JwtSecurityToken(
               issuer: emissor,
               audience: publico,
               claims: claims,
               expires: DateTime.UtcNow.AddHours(expiracaoHoras),
               signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
