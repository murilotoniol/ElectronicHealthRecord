using ElectronicHealthRecord.Application.DTOs.Auth;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Application.Services;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Domain.Enums;
using ElectronicHealthRecord.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ElectronicHealthRecord.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
    private readonly Mock<ISenhaHasher> _senhaHasherMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly AuthService _service;

    public AuthServiceTests()
    {
        _usuarioRepoMock = new Mock<IUsuarioRepository>();
        _senhaHasherMock = new Mock<ISenhaHasher>();
        _tokenServiceMock = new Mock<ITokenService>();

        _service = new AuthService(
            _usuarioRepoMock.Object,
            _senhaHasherMock.Object,
            _tokenServiceMock.Object
        );
    }

    [Fact]
    public async Task LoginAsync_QuandoCredenciaisForemValidas_DeveRetornarLoginResponseComToken()
    {
        var email = "medico@hospital.com";
        var senha = "Password123";
        var hash = "hash_criptografado";
        var tokenEsperado = "jwt.token.valido";

        var usuario = new Usuario("Dr. Roberto", email, hash, PerfilUsuario.Medico);

        _usuarioRepoMock.Setup(r => r.ObterPorEmailAsync(email)).ReturnsAsync(usuario);
        _senhaHasherMock.Setup(h => h.Verificar(senha, hash)).Returns(true);
        _tokenServiceMock.Setup(t => t.GerarToken(usuario.Id, usuario.Nome, usuario.Email, "Medico"))
            .Returns(tokenEsperado);

        var request = new LoginRequest(email, senha);

        var response = await _service.LoginAsync(request);

        response.Should().NotBeNull();
        response.Token.Should().Be(tokenEsperado);
        response.Email.Should().Be(email);
        response.Role.Should().Be("Medico");
    }

    [Fact]
    public async Task LoginAsync_QuandoUsuarioNaoExiste_DeveLancarCredenciaisInvalidasException()
    {
        _usuarioRepoMock.Setup(r => r.ObterPorEmailAsync(It.IsAny<string>())).ReturnsAsync((Usuario?)null);

        var request = new LoginRequest("inexistente@hospital.com", "qualquerSenha");

        Func<Task> acao = async () => await _service.LoginAsync(request);

        await acao.Should().ThrowAsync<CredenciaisInvalidasException>();
    }

    [Fact]
    public async Task LoginAsync_QuandoSenhaForIncorreta_DeveLancarCredenciaisInvalidasException()
    {
        var email = "medico@hospital.com";
        var usuario = new Usuario("Dr. Roberto", email, "hash_correto", PerfilUsuario.Medico);

        _usuarioRepoMock.Setup(r => r.ObterPorEmailAsync(email)).ReturnsAsync(usuario);
        _senhaHasherMock.Setup(h => h.Verificar("senha_errada", "hash_correto")).Returns(false);

        var request = new LoginRequest(email, "senha_errada");

        Func<Task> acao = async () => await _service.LoginAsync(request);

        await acao.Should().ThrowAsync<CredenciaisInvalidasException>();
        _tokenServiceMock.Verify(t => t.GerarToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}