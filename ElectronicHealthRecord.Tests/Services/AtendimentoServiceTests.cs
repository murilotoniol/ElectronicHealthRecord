using ElectronicHealthRecord.Application.DTOs.Atendimento;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Application.Services;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Domain.Enums;
using ElectronicHealthRecord.Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace ElectronicHealthRecord.Tests.Services;

public class AtendimentoServiceTests
{
    private readonly Mock<IAtendimentoRepository> _atendimentoRepoMock;
    private readonly Mock<IPacienteRepository> _pacienteRepoMock;
    private readonly Mock<IProfissionalRepository> _profissionalRepoMock;
    private readonly AtendimentoService _service;

    public AtendimentoServiceTests()
    {
        _atendimentoRepoMock = new Mock<IAtendimentoRepository>();
        _pacienteRepoMock = new Mock<IPacienteRepository>();
        _profissionalRepoMock = new Mock<IProfissionalRepository>();

        _service = new AtendimentoService(
            _atendimentoRepoMock.Object,
            _pacienteRepoMock.Object,
            _profissionalRepoMock.Object
        );
    }

    [Fact]
    public async Task CriarAsync_QuandoNaoHaConflito_DeveCriarComSucesso()
    {
        var pacienteId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var dataHora = DateTime.UtcNow.AddDays(1);

        var paciente = new Paciente("João Silva", "12345678901", new DateOnly(1990, 1, 1), "11999999999");
        var profissional = new Profissional("Dra. Paula", "CRM/SP 123456", "Cardiologia");

        _pacienteRepoMock.Setup(r => r.ObterPorIdAsync(pacienteId)).ReturnsAsync(paciente);
        _profissionalRepoMock.Setup(r => r.ObterPorIdAsync(profissionalId)).ReturnsAsync(profissional);
        _atendimentoRepoMock.Setup(r => r.ExisteConflitoHorarioAsync(profissionalId, dataHora)).ReturnsAsync(false);

        var request = new CriarAtendimentoRequest(pacienteId, profissionalId, dataHora);

        var response = await _service.CriarAsync(request);

        response.Should().NotBeNull();
        response.Status.Should().Be(StatusAtendimento.Agendado);
        _atendimentoRepoMock.Verify(r => r.CriarAsync(It.IsAny<Atendimento>()), Times.Once);
    }

    [Fact]
    public async Task CriarAsync_QuandoMedicoTemConflitoDeHorario_DeveLancarConflictException()
    {
        var pacienteId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();
        var dataHora = DateTime.UtcNow.AddDays(1);

        var paciente = new Paciente("João Silva", "12345678901", new DateOnly(1990, 1, 1), "11999999999");
        var profissional = new Profissional("Dra. Paula", "CRM/SP 123456", "Cardiologia");

        _pacienteRepoMock.Setup(r => r.ObterPorIdAsync(pacienteId)).ReturnsAsync(paciente);
        _profissionalRepoMock.Setup(r => r.ObterPorIdAsync(profissionalId)).ReturnsAsync(profissional);
        _atendimentoRepoMock.Setup(r => r.ExisteConflitoHorarioAsync(profissionalId, dataHora)).ReturnsAsync(true);

        var request = new CriarAtendimentoRequest(pacienteId, profissionalId, dataHora);

        Func<Task> acao = async () => await _service.CriarAsync(request);

        await acao.Should().ThrowAsync<ConflictException>()
            .WithMessage("*mesmo horário*");
        _atendimentoRepoMock.Verify(r => r.CriarAsync(It.IsAny<Atendimento>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_QuandoPacienteNaoExiste_DeveLancarNotFoundException()
    {
        var pacienteId = Guid.NewGuid();
        _pacienteRepoMock.Setup(r => r.ObterPorIdAsync(pacienteId)).ReturnsAsync((Paciente?)null);

        var request = new CriarAtendimentoRequest(pacienteId, Guid.NewGuid(), DateTime.UtcNow);

        Func<Task> acao = async () => await _service.CriarAsync(request);

        await acao.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{pacienteId}*");
    }

    [Fact]
    public async Task RealizarAsync_QuandoAtendimentoExiste_DeveAtualizarParaRealizado()
    {
        var id = Guid.NewGuid();
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        _atendimentoRepoMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync(atendimento);

        var response = await _service.RealizarAsync(id);

        response.Status.Should().Be(StatusAtendimento.Realizado);
        _atendimentoRepoMock.Verify(r => r.AtualizarAsync(atendimento), Times.Once);
    }
}