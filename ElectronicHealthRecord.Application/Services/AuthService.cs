using ElectronicHealthRecord.Application.DTOs.Auth;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Domain.Exceptions;

namespace ElectronicHealthRecord.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaHasher _senhaHasher;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            ISenhaHasher senhaHasher,
            ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _senhaHasher = senhaHasher;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(Usuario.NormalizarEmail(request.Email));

            // Mesma exceção para e-mail inexistente e senha errada, para não revelar quais e-mails existem.
            if (usuario == null || !_senhaHasher.Verificar(request.Senha, usuario.SenhaHash))
            {
                throw new CredenciaisInvalidasException();
            }

            var role = usuario.Perfil.ToString();
            var token = _tokenService.GerarToken(usuario.Id, usuario.Nome, usuario.Email, role);

            return new LoginResponse(token, usuario.Nome, usuario.Email, role);
        }
    }
}
