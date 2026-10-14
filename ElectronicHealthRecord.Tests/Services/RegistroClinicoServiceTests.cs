using ElectronicHealthRecord.Application.DTOs.Comum;
using ElectronicHealthRecord.Application.DTOs.RegistroClinico;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Application.Services;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace ElectronicHealthRecord.Tests.Services;

public class RegistroClinicoServiceTests
{
    private readonly Mock<IRegistroClinicoRepository> _registroRepoMock;
    private readonly Mock<IAtendimentoRepository> _atendimentoRepoMock;
    private readonly Mock<IPacienteRepository> _pacienteRepoMock;
    private readonly RegistroClinicoService _service;

    public RegistroClinicoServiceTests()
    {
        _registroRepoMock = new Mock<IRegistroClinicoRepository>();
        _atendimentoRepoMock = new Mock<IAtendimentoRepository>();
        _pacienteRepoMock = new Mock<IPacienteRepository>();

        _service = new RegistroClinicoService(
            _registroRepoMock.Object,
            _atendimentoRepoMock.Object,
            _pacienteRepoMock.Object
        );
    }

    [Fact]
    public async Task CriarAsync_QuandoAtendimentoEstaRealizado_DeveCriarComSucesso()
    {
        var atendimentoId = Guid.NewGuid();
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        atendimento.Realizar();

        _atendimentoRepoMock.Setup(r => r.ObterPorIdAsync(atendimentoId)).ReturnsAsync(atendimento);
        _registroRepoMock.Setup(r => r.ObterPorAtendimentoIdAsync(atendimentoId)).ReturnsAsync((RegistroClinico?)null);

        var request = new CriarRegistroClinicoRequest(
            atendimentoId,
            "Tosse seca",
            "Gripe",
            "Repouso e hidratação"
        );

        var response = await _service.CriarAsync(request);

        response.Should().NotBeNull();
        response.Diagnostico.Should().Be("Gripe");
        _registroRepoMock.Verify(r => r.CriarAsync(It.IsAny<RegistroClinico>()), Times.Once);
    }

    [Fact]
    public async Task CriarAsync_QuandoAtendimentoAindaEstaAgendado_DeveLancarBusinessRuleException()
    {
        var atendimentoId = Guid.NewGuid();
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);

        _atendimentoRepoMock.Setup(r => r.ObterPorIdAsync(atendimentoId)).ReturnsAsync(atendimento);

        var request = new CriarRegistroClinicoRequest(
            atendimentoId,
            "Tosse seca",
            "Gripe",
            "Repouso"
        );

        Func<Task> acao = async () => await _service.CriarAsync(request);

        await acao.Should().ThrowAsync<BusinessRuleException>()
            .WithMessage("*status 'Realizado'*");
        _registroRepoMock.Verify(r => r.CriarAsync(It.IsAny<RegistroClinico>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_QuandoAtendimentoJaPossuiRegistro_DeveLancarConflictException()
    {
        var atendimentoId = Guid.NewGuid();
        var atendimento = new Atendimento(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        atendimento.Realizar();

        var registroExistente = new RegistroClinico(atendimentoId, "Queixa", "Diagnóstico", "Obs");

        _atendimentoRepoMock.Setup(r => r.ObterPorIdAsync(atendimentoId)).ReturnsAsync(atendimento);
        _registroRepoMock.Setup(r => r.ObterPorAtendimentoIdAsync(atendimentoId)).ReturnsAsync(registroExistente);

        var request = new CriarRegistroClinicoRequest(atendimentoId, "Nova queixa", "Novo diag", "Nova obs");

        Func<Task> acao = async () => await _service.CriarAsync(request);

        await acao.Should().ThrowAsync<ConflictException>()
            .WithMessage("*já possui um registro clínico*");
    }

    [Fact]
    public async Task ObterHistoricoPorPacienteAsync_QuandoPacienteNaoExiste_DeveLancarNotFoundException()
    {
        var pacienteId = Guid.NewGuid();
        _pacienteRepoMock.Setup(r => r.ObterPorIdAsync(pacienteId)).ReturnsAsync((Paciente?)null);

        var request = new PaginacaoRequest(1, 10);

        Func<Task> acao = async () => await _service.ObterHistoricoPorPacienteAsync(pacienteId, request);

        await acao.Should().ThrowAsync<NotFoundException>();
    }
}